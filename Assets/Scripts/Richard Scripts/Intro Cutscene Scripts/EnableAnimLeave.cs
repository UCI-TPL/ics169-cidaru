using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAnimLeave : MonoBehaviour
{
    public Animator anim;

    public void EnableLeave()
    {
        anim.SetBool("leave", true);
    }
}
