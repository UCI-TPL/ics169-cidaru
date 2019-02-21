using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public GameObject pauseMenu;
    public List<GameObject> menus;

    public GameObject map;

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
	    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (!paused)
                Pause();
            else
                Resume();
        }
	}

    public void Pause()
    {
        Cursor.visible = true;

        paused = true;

        map.SetActive(false);
        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (PlayerPrefs.GetInt("Mouse") == 0)
            Cursor.visible = false;

        paused = false;

        pauseMenu.SetActive(false);
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        Time.timeScale = 1;
    }
}
