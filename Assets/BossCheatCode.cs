using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossCheatCode : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("X Button") && 
            Input.GetButton("Y Button") && 
            Input.GetButton("Left Bumper") && 
            Input.GetButton("Right Bumper"))
            {
            SceneManager.LoadScene(5);
        }

    }
}
