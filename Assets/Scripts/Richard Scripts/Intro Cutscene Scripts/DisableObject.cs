using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject gObject;

    public void DeactivateObject()
    {
        gObject.SetActive(false);
    }
}
