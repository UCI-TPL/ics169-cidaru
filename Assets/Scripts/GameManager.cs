using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    public enum Opening
    {
        Bottom,
        Top,
        Left,
        Right
    }
    
    public GameObject gameOverMenu;

    public Texture2D cursor;

    public GameObject map;

    private GameObject player;
    private DialogTextBox playerDialogueBubble;
    private Health playerHp;
    private GunController gun;
    private Transform minimapPos;

    private Fader fade;

    private bool playerDialogue;

    private void Awake()
    {
        gm = this;

        player = GameObject.Find("Player");
        playerHp = player.GetComponent<Health>();
        gun = player.GetComponent<GunController>();

        playerDialogueBubble = GameObject.Find("Player Text Bubble").GetComponent<DialogTextBox>();

        minimapPos = GameObject.Find("Minimap Objects").transform;

        fade = GetComponent<Fader>();

        Cursor.SetCursor(cursor, new Vector2(512 / 2, 512 / 2), CursorMode.Auto);

        playerDialogue = false;

        map.SetActive(false);
    }

    void Start () {
    }

    void Update() {
        if (playerHp.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }

        if (Time.timeScale != 0f)
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                map.SetActive(true);
                print("yo");
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                map.SetActive(false);
            }
        }
    }

    public void updateMinimapPosition(Vector3 newPos)
    {
        minimapPos.position = newPos;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 0;
        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex)); // ADD ONE FOR NEXT LEVEL CURRENTLY LOOPING
    }

    IEnumerator FadeWait(int sceneIndex)
    {
        float fadeTime = fade.BeginSceneFade(1);
        fade.BeginAudioFade(1);

        yield return new WaitForSecondsRealtime(fadeTime);

        Time.timeScale = 1;

        SceneManager.LoadScene(sceneIndex);
    }

    public bool getDialogue()
    {
        return playerDialogue;
    }

    public void startDialogue(TextAsset txt)
    {
        playerDialogue = true;

        playerDialogueBubble.startText(txt);
    }

    public void endDialogue()
    {
        playerDialogue = false;
    }
}
