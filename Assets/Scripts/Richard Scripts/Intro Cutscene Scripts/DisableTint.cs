using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTint : MonoBehaviour
{
    public Animator tint;

    public void RemoveTint()
    {
        tint.SetBool("untint", true);
    }
}
