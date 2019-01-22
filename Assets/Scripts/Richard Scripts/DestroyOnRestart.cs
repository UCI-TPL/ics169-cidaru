using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnRestart : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
