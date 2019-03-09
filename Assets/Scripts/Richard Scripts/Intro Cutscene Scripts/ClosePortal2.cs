using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePortal2 : MonoBehaviour
{
    public Animator anim;

    public void SetPortalClose2()
    {
        anim.SetBool("close", true);
    }
}
