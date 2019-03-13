using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {
    /* Defines behavior for melee weapons.
     * Namely delivering damage and deflecting bullets
     */

    public int dmg = 1;
    public bool deflection;
    public GameObject weaponEcho;

    private bool startDeflection;
    private Quaternion startRotation;
    private bool attackAnim;

    public enum WeaponState
    {
        Normal,
        Succing,
        Rotato
    }

    public float rotationSpeed = 200f;

    private WeaponState currentState;
    private Vector3 center;
    private float radius = 0f;

    public virtual void Awake()
    {
        currentState = WeaponState.Normal;
        radius = 0f;
        startDeflection = deflection;
        startRotation = transform.rotation;
    }

    public void Update()
    {
        if (currentState == WeaponState.Normal)
        {
            //Vector2 move = transform.up * movementSpeed * Time.deltaTime;
            //rb2d.MovePosition(rb2d.position + move);
        }
        else if (currentState == WeaponState.Succing)
        {
            Vector2 difference = transform.position - center;

            float angle = Vector2.Angle(Vector2.right, difference);

            if (transform.position.y - center.y < 0)
            {
                angle = -angle;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);

            currentState = WeaponState.Rotato;
        }
        else if (currentState == WeaponState.Rotato)
        {
            radius = Vector3.Distance(transform.position, center);
            if (radius >= 0.03f)
            {
                radius -= 0.01f;
            }

            transform.Rotate(new Vector3(0,0,0) * rotationSpeed * Time.deltaTime);
            Vector3.MoveTowards(transform.position, center, 100);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && tag.Contains("Enemy")) // Weapon hits player
        {
            collision.collider.GetComponent<Health>().TakeDamage(dmg);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player Bullet" && tag == "Enemy Weapon" && deflection)
        {
            collision.GetComponent<Bullet>().reflect();
            GetComponent<AudioSource>().Play();
        }
    }

    public void startVortex(Vector3 c)
    {
        center = c;
        currentState = WeaponState.Succing;
        transform.rotation = Quaternion.identity;

        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().isTrigger = true;
        if (GetComponent<CircleCollider2D>())
            GetComponent<CircleCollider2D>().isTrigger = true;
        gameObject.layer = 11;

        deflection = false;
    }

    public void endVortex()
    {
        currentState = WeaponState.Normal;

        transform.rotation = Quaternion.identity;

        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().isTrigger = false;
        if (GetComponent<CircleCollider2D>())
            GetComponent<CircleCollider2D>().isTrigger = false;
        gameObject.layer = 12;

        deflection = startDeflection;
    }

    public void resetRotations()
    {
        if (GetComponentInParent<Enemy>().transform.localScale.x == -1)
            transform.rotation = Quaternion.Inverse(startRotation);
        else
            transform.rotation = startRotation;
        attackAnim = false;
    }

    public void swooshDown(int numEcho, Vector3 endRotation)
    {
        if (!attackAnim && weaponEcho)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            for (int i = 0; i < numEcho; i++)
            {
                GameObject echo = Instantiate(weaponEcho, transform.position, transform.rotation);

                echo.GetComponent<SpriteRenderer>().color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
                float rotateZBy = (endRotation.z/i) * Mathf.Sign(transform.localScale.x);
                echo.transform.Rotate(new Vector3(0, 0, rotateZBy));
                echo.GetComponent<SpriteRenderer>().flipX = Mathf.Sign(transform.localScale.x) == -1;
                
                Destroy(echo, 0.25f);
            }
        }
        attackAnim = true;
        transform.Rotate(endRotation);
    }

}
