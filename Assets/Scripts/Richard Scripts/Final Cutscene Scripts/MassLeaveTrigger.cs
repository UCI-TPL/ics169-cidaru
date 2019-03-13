using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassLeaveTrigger : MonoBehaviour
{
    public List<Animator> anims;

    public void EnableAllLeaves()
    {
        foreach (Animator anim in anims)
            anim.SetBool("leave", true);
    }
}
