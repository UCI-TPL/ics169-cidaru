using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossCheatCode : MonoBehaviour
{
    private bool cheating = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("X Button") && Input.GetButton("Y Button") &&  Input.GetButton("Left Bumper") &&  Input.GetButton("Right Bumper") && !cheating)
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Boss Scene"))
            {
                cheating = true;
                PlayerPrefs.SetInt("Level", 6);
                SceneManager.LoadScene(6);
            }
            else
            {
                /*
                cheating = true;
                FindObjectOfType<BossHealthUI>().gameObject.GetComponent<Health>().currentHealth = 0;
                */
            }
        }
        if (Input.GetKey("o") && Input.GetKey("p"))
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Boss Scene"))
            {
                cheating = true;
                PlayerPrefs.SetInt("Level", 6);
                SceneManager.LoadScene(6);
            }
            else
            {
                cheating = true;
                FindObjectOfType<BossHealthUI>().gameObject.GetComponent<Health>().currentHealth = 0;
            }
        }

    }
}
