using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBabies : MonoBehaviour
{
    public void DisableBabies()
    {
        GameObject[] babies = GameObject.FindGameObjectsWithTag("Baby");

        foreach (GameObject baby in babies)
            baby.SetActive(false);
    }
}
