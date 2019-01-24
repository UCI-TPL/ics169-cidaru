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

    public float respawnTime = 2f;

    [HideInInspector]
    public bool cameraPanning = false;

    public CinemachineConfiner confiner;

    public GameObject proceduralUI;

    [HideInInspector]
    public bool spawningRooms = true;

    public GameObject cmCamera;

    private GameObject player;
    private DialogTextBox playerDialogueBubble;
    private PlayerHealth playerHp;

    private Transform minimapPos;
    private Transform cameraColPos;

    private Fader fade;

    private bool playerTalking;

    private ControlsUIText controlsUIText;

    private GlitchEffect glitchEff;

    private RoomTemplates templates;

    private bool respawning;

    private void Awake()
    {
        gm = this;

        player = GameObject.Find("Player");
        playerHp = player.GetComponent<PlayerHealth>();

        playerDialogueBubble = GameObject.Find("Player Text Bubble").GetComponent<DialogTextBox>();

        minimapPos = GameObject.Find("Minimap Objects").transform;
        cameraColPos = GameObject.Find("Camera Collider").transform;

        fade = GetComponent<Fader>();

        Cursor.visible = false;

        if (PlayerPrefs.GetInt("Mouse") != 0)
            Cursor.visible = true;

        controlsUIText = GetComponent<ControlsUIText>();

        if (PlayerPrefs.GetInt("Mouse") != 0)
            controlsUIText.keyboardText();
        else
            controlsUIText.controllerText();

        playerTalking = false;

        map.SetActive(false);

        spawningRooms = true;

        proceduralUI.SetActive(true);

        glitchEff = Camera.main.GetComponent<GlitchEffect>();
        glitchEff.enabled = false;

        templates = GameManager.gm.GetComponent<RoomTemplates>();

        respawning = false;
    }

    void Start () {
    }

    void Update() {
        if (!spawningRooms)
            gameplayLoop();
        else
            spawnRoomLoop();
        
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
        if (playerHp.dead() && !respawning)
        {
            Time.timeScale = 0f;

            Cursor.visible = true;
            //gameOverMenu.SetActive(true);
            
            GetComponent<PauseController>().enabled = false;

            StartCoroutine(RespawnMap());
        }

        if (Time.timeScale != 0f)
        {
            if (Input.GetKey(KeyCode.Tab))
                map.SetActive(true);

            if (Input.GetKeyUp(KeyCode.Tab))
                map.SetActive(false);

            if (Input.GetButtonDown("Back") && !map.activeSelf)
                map.SetActive(true);
            else if (Input.GetButtonDown("Back") && map.activeSelf)
                map.SetActive(false);

        } else
        {
            map.SetActive(false);
        }
    }

    IEnumerator RespawnMap()
    {
        respawning = true;

        glitchEff.enabled = true;

        yield return new WaitForSecondsRealtime(respawnTime);

        foreach (RespawnRoom respawner in templates.respawners)
        {
            respawner.Respawn();
        }

        templates.respawners = new List<RespawnRoom>(templates.copyRespawners);
        templates.copyRespawners.Clear();

        templates.copyRespawners = new List<RespawnRoom>(templates.nextInLineRespawners);
        templates.nextInLineRespawners.Clear();

        // Reset Positions
        player.transform.position = new Vector3(0.25f, 0.25f);
        cameraColPos.localScale = Vector3.one;
        cameraColPos.position = Vector3.zero;
        minimapPos.position = Vector3.zero;
        cmCamera.transform.position = new Vector3(0.25f, 0.25f, -10);

        // Reset Values
        playerHp.MaxHeal();
        player.GetComponent<GunController>().respawnReload();
        player.GetComponent<CooldownController>().resetCooldowns();

        // Reset Map
        DestroyOnRestart[] restartObjects = GameObject.FindObjectsOfType<DestroyOnRestart>();

        foreach (DestroyOnRestart restartObject in restartObjects)
            restartObject.DestroyObject();

        glitchEff.enabled = false;

        Time.timeScale = 1;

        respawning = false;
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

    public void updateDoubleLRMinimapPosition(Vector3 newPos)
    {
        if (minimapPos.position != newPos)
        {
            cameraColPos.localScale = new Vector3(2, 1, 1);

            minimapPos.position = newPos;

            StartCoroutine(PanCamera(newPos));
        }
    }

    public void updateDoubleTBMinimapPosition(Vector3 newPos)
    {
        if (minimapPos.position != newPos)
        {
            cameraColPos.localScale = new Vector3(1, 2, 1);

            minimapPos.position = newPos;

            StartCoroutine(PanCamera(newPos));
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 0;
        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex)); // ADD ONE FOR NEXT LEVEL CURRENTLY LOOPING
    }

    public void LoadBossLevel()
    {
        Time.timeScale = 0;
        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex + 1));
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
