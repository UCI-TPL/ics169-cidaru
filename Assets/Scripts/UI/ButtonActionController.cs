using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonActionController : MonoBehaviour {
    public Transition transition;
    //private Fader fade;
    private GameObject eventSystem;

    void Awake()
    {
        //fade = GameObject.Find("GameManager").GetComponent<Fader>();
        eventSystem = GameObject.Find("EventSystem");
    }

    private void Start()
    {
        eventSystem.SetActive(true);
    }

    public void EnableMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        eventSystem.SetActive(false);
        StartCoroutine(FadeWait(sceneIndex));
    }

    public void LoadContinue()
    {
        eventSystem.SetActive(false);

        int currentLevel = PlayerPrefs.GetInt("Level");

        if (currentLevel != 1)
            StartCoroutine(FadeWait(currentLevel));

        /*
        if (currentLevel == 2 || currentLevel == 3)
            StartCoroutine(FadeWait(2));
        else if (currentLevel == 4)
            StartCoroutine(FadeWait(3));
        */
    }

    public void LoadNewGame()
    {
        PlayerPrefs.SetInt("Level", 1);
        eventSystem.SetActive(false);
        StartCoroutine(FadeWait(1));
    }

    IEnumerator FadeWait(int sceneIndex)
    {
        //float fadeTime = fade.BeginSceneFade(1);
        //fade.BeginAudioFade(1);

        transition.fadeToBlack();

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;

        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        eventSystem.SetActive(false);
        StartCoroutine(FadeQuitWait());
    }

    IEnumerator FadeQuitWait()
    {
        //float fadeTime = fade.BeginSceneFade(1);
        //fade.BeginAudioFade(1);

        transition.fadeToBlack();

        yield return new WaitForSecondsRealtime(2f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
