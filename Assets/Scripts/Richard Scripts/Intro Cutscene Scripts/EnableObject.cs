using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    public GameObject gObject;

    public void ActivateObject()
    {
        gObject.SetActive(true);
    }
}
