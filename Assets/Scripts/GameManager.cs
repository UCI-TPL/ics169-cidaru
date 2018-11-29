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

    public float cameraPanTime = 1f;
    public float roomStartDelay = 1f;

    [HideInInspector]
    public bool cameraPanning = false;

    private GameObject player;
    private DialogTextBox playerDialogueBubble;
    private Health playerHp;

    private Transform minimapPos;
    private Transform cameraColPos;

    private Fader fade;

    private bool playerDialogue;

    private void Awake()
    {
        gm = this;

        player = GameObject.Find("Player");
        playerHp = player.GetComponent<Health>();

        playerDialogueBubble = GameObject.Find("Player Text Bubble").GetComponent<DialogTextBox>();

        minimapPos = GameObject.Find("Minimap Objects").transform;
        cameraColPos = GameObject.Find("Camera Collider").transform;

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
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                map.SetActive(false);
            }
        }
    }

    public void updateMinimapPosition(Vector3 newPos)
    {
        if (minimapPos.position != newPos)
        {
            minimapPos.position = newPos;

            StartCoroutine(PanCamera(newPos));
        }
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

    IEnumerator PanCamera(Vector3 nextPos)
    {
        cameraPanning = true;

        Vector3 currentPos = cameraColPos.position;
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / cameraPanTime;

            cameraColPos.position = Vector3.Lerp(currentPos, nextPos, t);

            yield return null;
        }

        cameraPanning = false;

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(roomStartDelay);

        Time.timeScale = 1;
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
