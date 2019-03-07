using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDogReemergesDisappear : MonoBehaviour
{
    public Animator dogAnim;

    public void EnableDisappear()
    {
        dogAnim.SetBool("disappear", true);
    }
}
