using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimShadow : MonoBehaviour
{
    public SpriteRenderer parentRend;
    public  SpriteRenderer thisRend;

    private Health parentHp;


    void Start()
    {
        //parentRend = GetComponentInParent<SpriteRenderer>();
        //thisRend = GetComponent<SpriteRenderer>();
        parentHp = GetComponentInParent<Health>();
    }

    private void Awake()
    {
        thisRend.sprite = parentRend.sprite;
        thisRend.flipX = parentRend.flipX;
    }


    void Update()
    {
        thisRend.sprite = parentRend.sprite;
        thisRend.flipX = parentRend.flipX;

        if (parentHp && parentHp.dead() && GetComponentInParent<Enemy>())
        {
            Destroy(gameObject);
        }
    }
}
