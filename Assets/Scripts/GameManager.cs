using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour {

    public enum TutorialStates
    {
        ShootMoveRoomStart,
        ShootMoveRoom,
        ShootMoveRoomEnd,
        ShootMoveRoomPost,
        SlowRoomStart,
        SlowRoom,
        SlowRoomEnd,
        SlowRoomPost,
        VortexRoomStart,
        VortexRoom,
        VortexRoomEnd,
        VortexRoomPost,
        BabyRoomStart,
        BabyRoom,
        BabyRoomEnd,
        BabyRoomPost,
        PortalRoom,
        PortalRoomPost
    }

    public static GameManager gm;

    public enum Opening
    {
        Bottom,
        Top,
        Left,
        Right
    }

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

    [Header("Dialogue Box Objects")]
    public GameObject dialogBox;
    public GameObject avatarImage;
    public DialogTextBox dialogText;

    [Header("UI Objects (To Disable)")]
    public GameObject uiIcons;
    public GameObject hpUI;

    [Header("Tutorial Fields (DO NOT FILL BELOW IF NOT TUTORIAL)")]
    public bool isTutorial = false;
    
    [Header("Room Conditions")]
    public List<GameObject> destroyableVases = new List<GameObject>();
    public GameObject trappedTrojan;
    public GameObject hogTiedTrojan;

    [Header("Room Doors")]
    public GameObject shootMoveRoomDoor;
    public List<GameObject> vortexRoomDoors;
    public List<GameObject> babyRoomDoors;

    [Header("Cutscene Text Files")]
    public TextAsset[] moveShootTextFiles;
    public TextAsset[] slowTextFiles;
    public TextAsset[] vortexTextFiles;
    public TextAsset[] babyTextFiles;
    public TextAsset[] portalTextFiles;

    [Header("Individual Skill CD")]
    public GameObject slowCD;
    public GameObject babyCD;
    public GameObject vortexCD;

    private bool textActive;
    
    [HideInInspector]
    public TutorialStates currentState;
    
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

        minimapPos = GameObject.Find("Minimap Objects").transform;
        cameraColPos = GameObject.Find("Camera Collider").transform;

        fade = GetComponent<Fader>();

        if (PlayerPrefs.GetInt("Mouse") != 0)
            Cursor.visible = true;
        else
            Cursor.visible = false;

        /*
        controlsUIText = GetComponent<ControlsUIText>();

        if (PlayerPrefs.GetInt("Mouse") != 0)
            controlsUIText.keyboardText();
        else
            controlsUIText.controllerText();
        */

        playerTalking = false;

        map.SetActive(false);

        glitchEff = Camera.main.GetComponent<GlitchEffect>();
        glitchEff.enabled = false;
        
        respawning = false;

        dialogBox.SetActive(false);
        avatarImage.SetActive(false);

        if (isTutorial)
        {
            spawningRooms = false;

            textActive = false;

            currentState = TutorialStates.ShootMoveRoomStart;            
        } else
        {
            spawningRooms = true;
            proceduralUI.SetActive(true);

            templates = GetComponent<RoomTemplates>();
        }
    }

    void Update() {
        if (isTutorial)
        {
            tutorialLoop();
            return;
        }

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
            
            GetComponent<PauseController>().enabled = false;

            if (SceneManager.GetActiveScene().buildIndex == 2)
                StartCoroutine(RespawnMap());
            else if (SceneManager.GetActiveScene().buildIndex == 3)
                StartCoroutine(DeathReloadLevel());
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
        //player.GetComponent<GunController>().respawnReload(); Pre Reload Removal
        player.GetComponent<CooldownController>().resetCooldowns();

        // Reset Map
        DestroyOnRestart[] restartObjects = GameObject.FindObjectsOfType<DestroyOnRestart>();

        foreach (DestroyOnRestart restartObject in restartObjects)
            restartObject.DestroyObject();

        glitchEff.enabled = false;

        Time.timeScale = 1;

        GetComponent<PauseController>().enabled = true;

        respawning = false;
    }

    public void updateMinimapPosition(Vector3 newPos)
    {
        if (minimapPos.position != newPos || isTutorial)
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
        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator DeathReloadLevel()
    {
        respawning = true;

        glitchEff.enabled = true;

        yield return new WaitForSecondsRealtime(respawnTime);

        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReloadLevel()
    {
        Time.timeScale = 0;

        StartCoroutine(FadeWait(SceneManager.GetActiveScene().buildIndex));
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
            t += Time.unscaledDeltaTime / cameraPanTime;

            cameraColPos.position = Vector3.Lerp(currentPos, nextPos, t);
            confiner.InvalidatePathCache();

            yield return null;
        }

        cameraPanning = false;
        
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(roomStartDelay);

        Time.timeScale = 1;

        if (isTutorial)
            NextState();
    }

    #region Tutorial Functions
    private void tutorialLoop()
    {
        if (playerHp.dead() && !respawning)
        {
            Time.timeScale = 0f;

            Cursor.visible = true;

            GetComponent<PauseController>().enabled = false;

            if (SceneManager.GetActiveScene().buildIndex == 1)
                StartCoroutine(DeathReloadLevel());
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

            if (currentState == TutorialStates.ShootMoveRoomStart && !textActive)
            {
                startTutorialDialogue(moveShootTextFiles[0]);
            }
            else if (currentState == TutorialStates.ShootMoveRoom)
            {
                slowCD.SetActive(false);
                babyCD.SetActive(false);
                vortexCD.SetActive(false);

                checkVaseRoomCleared();
            }
            else if (currentState == TutorialStates.ShootMoveRoomEnd && !textActive)
            {
                startTutorialDialogue(moveShootTextFiles[1]);
            }
            else if (currentState == TutorialStates.ShootMoveRoomPost)
            {
                // SKIP ACTION
            }
            else if (currentState == TutorialStates.SlowRoomStart && !textActive)
            {
                startTutorialDialogue(slowTextFiles[0]);
            }
            else if (currentState == TutorialStates.SlowRoom)
            {
                slowCD.SetActive(true);
            }
            else if (currentState == TutorialStates.SlowRoomEnd && !textActive)
            {
                startTutorialDialogue(slowTextFiles[1]);
            }
            else if (currentState == TutorialStates.SlowRoomPost)
            {
                // SKIP ACTION
            }
            else if (currentState == TutorialStates.VortexRoomStart && !textActive)
            {
                startTutorialDialogue(vortexTextFiles[0]);
            }
            else if (currentState == TutorialStates.VortexRoom)
            {
                slowCD.SetActive(false);
                vortexCD.SetActive(true);

                checkVortexRoomCleared();
            }
            else if (currentState == TutorialStates.VortexRoomEnd && !textActive)
            {
                startTutorialDialogue(vortexTextFiles[1]);
            }
            else if (currentState == TutorialStates.VortexRoomPost)
            {
                // SKIP ACTION
            }
            else if (currentState == TutorialStates.BabyRoomStart && !textActive)
            {
                startTutorialDialogue(babyTextFiles[0]);
            }
            else if (currentState == TutorialStates.BabyRoom)
            {
                vortexCD.SetActive(false);
                babyCD.SetActive(true);

                checkBabyRoomCleared();
            }
            else if (currentState == TutorialStates.BabyRoomEnd && !textActive)
            {
                startTutorialDialogue(babyTextFiles[1]);
            }
            else if (currentState == TutorialStates.BabyRoomPost)
            {
                // SKIP ACTION
            }
            else if (currentState == TutorialStates.PortalRoom && !textActive)
            {
                startTutorialDialogue(portalTextFiles[0]);
            }
            else if (currentState == TutorialStates.PortalRoomPost)
            {
                vortexCD.SetActive(true);
                babyCD.SetActive(true);
                slowCD.SetActive(true);
            }
        }
        else
        {
            map.SetActive(false);
        }
    }
    
    // Check if the vases have been cleared
    public void checkVaseRoomCleared()
    {
        foreach (GameObject destroyableVase in destroyableVases)
        {
            // If not cleared, return
            if (destroyableVase != null)
                return;
        }

        // Disable all doors after clearing
        shootMoveRoomDoor.SetActive(false);

        NextState();
    }

    public void checkVortexRoomCleared()
    {
        if (trappedTrojan != null)
            return;

        foreach (GameObject dc in vortexRoomDoors)
            dc.SetActive(false);

        NextState();
    }

    public void checkBabyRoomCleared()
    {
        if (hogTiedTrojan != null)
            return;

        foreach (GameObject dc in babyRoomDoors)
            dc.SetActive(false);

        NextState();
    }

    public void startDialogue(TextAsset text)
    {
        Time.timeScale = 0;

        dialogBox.SetActive(true);
        avatarImage.SetActive(true);

        dialogText.startText(text);

        uiIcons.SetActive(false);
        hpUI.SetActive(false);
    }

    public void endDialogue()
    {
        Time.timeScale = 1;

        dialogBox.SetActive(false);
        avatarImage.SetActive(false);

        uiIcons.SetActive(true);
        hpUI.SetActive(true);
    }

    public void startTutorialDialogue(TextAsset text)
    {
        Time.timeScale = 0;

        dialogBox.SetActive(true);
        avatarImage.SetActive(true);

        dialogText.startText(text);

        uiIcons.SetActive(false);
        hpUI.SetActive(false);

        textActive = true;
    }

    public void endTutorialDialogue()
    {
        Time.timeScale = 1;

        dialogBox.SetActive(false);
        avatarImage.SetActive(false);

        uiIcons.SetActive(true);
        hpUI.SetActive(true);

        NextState();
        textActive = false;
    }

    public void NextState()
    {
        if (currentState == TutorialStates.ShootMoveRoomStart)
            currentState = TutorialStates.ShootMoveRoom;
        else if (currentState == TutorialStates.ShootMoveRoom)
            currentState = TutorialStates.ShootMoveRoomEnd;
        else if (currentState == TutorialStates.ShootMoveRoomEnd)
            currentState = TutorialStates.ShootMoveRoomPost;
        else if (currentState == TutorialStates.ShootMoveRoomPost)
            currentState = TutorialStates.SlowRoomStart;
        else if (currentState == TutorialStates.SlowRoomStart)
            currentState = TutorialStates.SlowRoom;
        else if (currentState == TutorialStates.SlowRoom)
            currentState = TutorialStates.SlowRoomEnd;
        else if (currentState == TutorialStates.SlowRoomEnd)
            currentState = TutorialStates.SlowRoomPost;
        else if (currentState == TutorialStates.SlowRoomPost)
            currentState = TutorialStates.VortexRoomStart;
        else if (currentState == TutorialStates.VortexRoomStart)
            currentState = TutorialStates.VortexRoom;
        else if (currentState == TutorialStates.VortexRoom)
            currentState = TutorialStates.VortexRoomEnd;
        else if (currentState == TutorialStates.VortexRoomEnd)
            currentState = TutorialStates.VortexRoomPost;
        else if (currentState == TutorialStates.VortexRoomPost)
            currentState = TutorialStates.BabyRoomStart;
        else if (currentState == TutorialStates.BabyRoomStart)
            currentState = TutorialStates.BabyRoom;
        else if (currentState == TutorialStates.BabyRoom)
            currentState = TutorialStates.BabyRoomEnd;
        else if (currentState == TutorialStates.BabyRoomEnd)
            currentState = TutorialStates.BabyRoomPost;
        else if (currentState == TutorialStates.BabyRoomPost)
            currentState = TutorialStates.PortalRoom;
        else
            currentState = TutorialStates.PortalRoomPost;
    }
    #endregion Tutorial Functions
}
