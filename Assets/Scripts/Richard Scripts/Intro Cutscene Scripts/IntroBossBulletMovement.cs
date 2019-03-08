using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBossBulletMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    public GameObject bulletEff;

    public GameObject mom;

    public void Update()
    {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, mom.transform.position, step);

        if (transform.position.x - mom.transform.position.x > -0.001f)
        {
            Instantiate(bulletEff, transform.position, Quaternion.identity);

            mom.GetComponent<Animator>().SetBool("hit", true);
            GetComponent<NextIntroState>().ChangeToNextState();

            gameObject.SetActive(false);
        }
    }
}
