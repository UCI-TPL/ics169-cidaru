using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimLeave : MonoBehaviour
{
    public Animator dogAnim;

    public void DisableLeave()
    {
        dogAnim.SetBool("leave", false);
    }
}
