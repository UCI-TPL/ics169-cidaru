using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

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

    public GameObject map;

    public float cameraPanTime = 1f;
    public float roomStartDelay = 1f;

    [HideInInspector]
    public bool cameraPanning = false;

    public CinemachineConfiner confiner;

    public GameObject proceduralUI;

    [HideInInspector]
    public bool spawningRooms = true;

    private GameObject player;
    private DialogTextBox playerDialogueBubble;
    private Health playerHp;

    private Transform minimapPos;
    private Transform cameraColPos;

    private Fader fade;

    private bool playerTalking;

    private void Awake()
    {
        gm = this;

        player = GameObject.Find("Player");
        playerHp = player.GetComponent<Health>();

        playerDialogueBubble = GameObject.Find("Player Text Bubble").GetComponent<DialogTextBox>();

        minimapPos = GameObject.Find("Minimap Objects").transform;
        cameraColPos = GameObject.Find("Camera Collider").transform;

        fade = GetComponent<Fader>();

        Cursor.visible = false;

        if (PlayerPrefs.GetInt("Mouse") != 0)
            Cursor.visible = true;

        playerTalking = false;

        map.SetActive(false);

        spawningRooms = true;

        proceduralUI.SetActive(true);
    }

    void Start () {
    }

    void Update() {
        if (!spawningRooms)
        {
            gameplayLoop();
        } else
        {
            spawnRoomLoop();
        }
        
    }

    private void spawnRoomLoop()
    {
        GameObject[] sp = GameObject.FindGameObjectsWithTag("Spawn Point");

        if (sp.Length == 0)
        {
            GameObject[] destroyers = GameObject.FindGameObjectsWithTag("Destroyer");

            foreach (GameObject destroyer in destroyers)
                Destroy(destroyer);

            spawningRooms = false;
            proceduralUI.SetActive(false);
        }
    }

    private void gameplayLoop()
    {
        if (playerHp.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            Cursor.visible = true;
            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }

        if (Time.timeScale != 0f)
        {
            if (Input.GetKey(KeyCode.Tab) || Input.GetButton("Back"))
            {
                map.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.Tab) || Input.GetButtonUp("Back"))
            {
                map.SetActive(false);
            }
        } else
        {
            map.SetActive(false);
        }
    }

    public void updateMinimapPosition(Vector3 newPos)
    {
        if (minimapPos.position != newPos)
        {
            cameraColPos.localScale = Vector3.one;

            minimapPos.position = newPos;

            StartCoroutine(PanCamera(newPos));
        }
    }

    public void updateDoubleMinimapPosition(Vector3 newPos)
    {
        if (minimapPos.position != newPos)
        {
            cameraColPos.localScale = new Vector3(2, 1, 1);

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
            confiner.InvalidatePathCache();

            yield return null;
        }

        cameraPanning = false;
        
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(roomStartDelay);

        Time.timeScale = 1;
    }

    public void startDialogue(TextAsset txt)
    {
        if (!playerTalking)
        {
            playerTalking = true;

            playerDialogueBubble.startText(txt);
        }
    }

    public void endDialogue()
    {
        playerTalking = false;
    }
}
