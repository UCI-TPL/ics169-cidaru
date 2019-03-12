using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPostAnim : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
            Destroy(this.gameObject);
    }
}
