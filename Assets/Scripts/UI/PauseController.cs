using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public GameObject pauseMenu;
    public List<GameObject> menus;
    
    private bool paused;

    void Start()
    {
        pauseMenu.SetActive(false);
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        paused = false;
    }

    void Update() {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
	}

    public void Pause()
    {
        paused = true;

        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void Resume()
    {
        paused = false;

        pauseMenu.SetActive(false);
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        Time.timeScale = 1;
    }
}
