using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePortal : MonoBehaviour
{
    public Animator anim;

    public void SetPortalClose()
    {
        anim.SetBool("close", true);
    }
}
