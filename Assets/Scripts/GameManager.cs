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
        ShootRoomStart,
        ShootRoom,
        ShootRoomEnd,
        ShootRoomPost,
        SlowRoomStart,
        SlowRoom,
        SlowRoomPost,
        VortexRoomStart,
        VortexRoom,
        VortexRoomEnd,
        VortexRoomPost,
        BabyRoomStart,
        BabyRoomPart1,
        BabyRoomPart1End,
        BabyRoomPart2,
        BabyRoomPart2End,
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

    [Header("Initial Dialogue of Levels")]
    public TextAsset level1Text;
    public TextAsset level2Text;
    public TextAsset level3Text;
    public TextAsset preBossText;
    public TextAsset postBossText;

    [Header("Respawn Dialogue")]
    public List<TextAsset> respawnDialogues;

    [Header("Tutorial Fields (DO NOT FILL BELOW IF NOT TUTORIAL)")]
    public bool isTutorial = false;
    
    [Header("Room Conditions")]
    public List<GameObject> introDestroyableVases = new List<GameObject>();
    public List<GameObject> vortexTrappedTrojans;
    public List<GameObject> babyHogTiedTrojan;
    public GameObject babyYaYeetTrojan;
    public List<GameObject> babyPortals;

    [Header("Room Doors")]
    public List<GameObject> shootRoomDoors;
    public GameObject shootRoomPortalDoor;
    public GameObject vortexRoomDoor;
    public GameObject babyRoomDoor;
    public GameObject portalRoomDoor;

    [Header("Cutscene Text Files")]
    public TextAsset[] shootTextFiles;
    public TextAsset[] slowTextFiles;
    public TextAsset[] vortexTextFiles;
    public TextAsset[] babyTextFiles;
    public TextAsset[] portalTextFiles;

    [Header("Individual Skill CD")]
    public GameObject dashCD;
    public GameObject slowCD;
    public GameObject babyCD;
    public GameObject vortexCD;

    private bool textActive;
    
    //[HideInInspector]
    public TutorialStates currentState;

    [HideInInspector]
    public TutorialStates nextState;
    
    private GameObject player;
    private DialogTextBox playerDialogueBubble;
    private PlayerHealth playerHp;

    private Transform minimapPos;
    private Transform cameraColPos;

    private Fader fade;

    private bool initialDialogue;

    private bool playerTalking;

    private ControlsUIText controlsUIText;

    private GlitchEffect glitchEff;

    private RoomTemplates templates;

    private bool respawning;

    [HideInInspector]
    public bool babyRoomComplete;
    [HideInInspector]
    public bool vortexRoomComplete;
    [HideInInspector]
    public bool slowRoomComplete;

    private void Awake()
    {
        gm = this;

        player = GameObject.Find("Player");
        playerHp = player.GetComponent<PlayerHealth>();

        minimapPos = GameObject.Find("Minimap Objects").transform;
        cameraColPos = GameObject.Find("Camera Collider").transform;

        initialDialogue = false;

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

            currentState = TutorialStates.ShootRoomStart;

            babyRoomComplete = false;
            slowRoomComplete = false;
            vortexRoomComplete = false;
        }
        else
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
        if (!initialDialogue)
        {
            PerformInitialDialogue();
            initialDialogue = true;
        }

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
            if (Input.GetKeyDown(KeyCode.Tab) && !map.activeSelf)
                map.SetActive(true);
            else if (Input.GetKeyDown(KeyCode.Tab) && map.activeSelf)
                map.SetActive(false);

            if (Input.GetButtonDown("Back") && !map.activeSelf)
                map.SetActive(true);
            else if (Input.GetButtonDown("Back") && map.activeSelf)
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

        //startDialogue(respawnDialogues[Random.Range(0, respawnDialogues.Count)]);
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

        if (isTutorial && (currentState != TutorialStates.ShootRoom || currentState != TutorialStates.ShootRoomPost))
            currentState = nextState;
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
            if (Input.GetKeyDown(KeyCode.Tab) && !map.activeSelf)
                map.SetActive(true);
            else if (Input.GetKeyDown(KeyCode.Tab) && map.activeSelf)
                map.SetActive(false);

            if (Input.GetButtonDown("Back") && !map.activeSelf)
                map.SetActive(true);
            else if (Input.GetButtonDown("Back") && map.activeSelf)
                map.SetActive(false);

            if (currentState == TutorialStates.ShootRoomStart && !textActive)
            {
                startTutorialDialogue(shootTextFiles[0]);
            }
            else if (currentState == TutorialStates.ShootRoom)
            {
                dashCD.SetActive(false);
                slowCD.SetActive(false);
                babyCD.SetActive(false);
                vortexCD.SetActive(false);

                checkVaseRoomCleared();
            }
            else if (currentState == TutorialStates.ShootRoomEnd && !textActive)
            {
                startTutorialDialogue(shootTextFiles[1]);
            }
            else if (currentState == TutorialStates.ShootRoomPost)
            {
                dashCD.SetActive(false);
                slowCD.SetActive(false);
                babyCD.SetActive(false);
                vortexCD.SetActive(false);
            }
            else if (currentState == TutorialStates.SlowRoomStart && !textActive)
            {
                startTutorialDialogue(slowTextFiles[0]);
            }
            else if (currentState == TutorialStates.SlowRoom)
            {
                slowCD.SetActive(true);
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
                dashCD.SetActive(false);
                babyCD.SetActive(false);
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
            else if (currentState == TutorialStates.BabyRoomPart1)
            {
                dashCD.SetActive(false);
                slowCD.SetActive(false);
                vortexCD.SetActive(false);
                babyCD.SetActive(true);

                checkBabyRoomPart1Cleared();
            }
            else if (currentState == TutorialStates.BabyRoomPart1End && !textActive)
            {
                startTutorialDialogue(babyTextFiles[1]);

                babyYaYeetTrojan.SetActive(true);

                foreach (GameObject babyPortal in babyPortals)
                    babyPortal.SetActive(true);
            }
            else if (currentState == TutorialStates.BabyRoomPart2)
            {
                checkBabyRoomPart2Cleared();
            }
            else if (currentState == TutorialStates.BabyRoomPart2End && !textActive)
            {
                startTutorialDialogue(babyTextFiles[2]);
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
                dashCD.SetActive(true);
                vortexCD.SetActive(true);
                babyCD.SetActive(true);
                slowCD.SetActive(true);
            }
        }
    }
    
    // Check if the vases have been cleared
    public void checkVaseRoomCleared()
    {
        foreach (GameObject destroyableVase in introDestroyableVases)
        {
            // If not cleared, return
            if (destroyableVase != null)
                return;
        }

        // Disable all doors after clearing
        foreach (GameObject shootRoomDoor in shootRoomDoors)
            shootRoomDoor.SetActive(false);

        NextState();
    }

    public void checkVortexRoomCleared()
    {
        foreach (GameObject vortexTrappedTrojan in vortexTrappedTrojans)
        {
            // If not cleared, return
            if (vortexTrappedTrojan != null)
                return;
        }

        vortexRoomDoor.SetActive(false);

        vortexRoomComplete = true;

        NextState();
    }

    public void checkBabyRoomPart1Cleared()
    {
        foreach (GameObject babyHogTiedTrojanObject in babyHogTiedTrojan)
        {
            // If not cleared, return
            if (babyHogTiedTrojanObject != null)
                return;
        }

        NextState();
    }

    public void checkBabyRoomPart2Cleared()
    {
        if (babyYaYeetTrojan != null)
            return;

        babyRoomDoor.SetActive(false);

        babyRoomComplete = true;

        NextState();
    }

    public void PerformInitialDialogue()
    {
        if (PlayerPrefs.GetInt("Level") == 2)
            startDialogue(level1Text);
        else if (PlayerPrefs.GetInt("Level") == 3)
            startDialogue(level2Text);
        else if (PlayerPrefs.GetInt("Level") == 4)
            startDialogue(level3Text);
    }

    public void startDialogue(TextAsset text)
    {
        Time.timeScale = 0;

        map.SetActive(false);

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

        map.SetActive(false);

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
        // Shoot Room State Change Condition
        if (currentState == TutorialStates.ShootRoomStart)
            currentState = TutorialStates.ShootRoom;
        else if (currentState == TutorialStates.ShootRoom)
            currentState = TutorialStates.ShootRoomEnd;
        else if (currentState == TutorialStates.ShootRoomEnd)
            currentState = TutorialStates.ShootRoomPost;

        // Slow Room State Change Condition
        if (currentState == TutorialStates.SlowRoomStart)
            currentState = TutorialStates.SlowRoom;
        else if (currentState == TutorialStates.SlowRoom)
            currentState = TutorialStates.SlowRoomPost;

        // Vortex Room Change Condition
        if (currentState == TutorialStates.VortexRoomStart)
            currentState = TutorialStates.VortexRoom;
        else if (currentState == TutorialStates.VortexRoom)
            currentState = TutorialStates.VortexRoomEnd;
        else if (currentState == TutorialStates.VortexRoomEnd)
            currentState = TutorialStates.VortexRoomPost;

        // Baby Room Change Condition
        if (currentState == TutorialStates.BabyRoomStart)
            currentState = TutorialStates.BabyRoomPart1;
        else if (currentState == TutorialStates.BabyRoomPart1)
            currentState = TutorialStates.BabyRoomPart1End;
        else if (currentState == TutorialStates.BabyRoomPart1End)
            currentState = TutorialStates.BabyRoomPart2;
        else if (currentState == TutorialStates.BabyRoomPart2)
            currentState = TutorialStates.BabyRoomPart2End;
        else if (currentState == TutorialStates.BabyRoomPart2End)
            currentState = TutorialStates.BabyRoomPost;

        // Portal Room Change Condition
        if (currentState == TutorialStates.PortalRoom)
            currentState = TutorialStates.PortalRoomPost;
    }

    public void checkIntroDoorOpen()
    {
        if (vortexRoomComplete)
            shootRoomDoors[0].SetActive(true);

        if (babyRoomComplete)
            shootRoomDoors[1].SetActive(true);

        if (slowRoomComplete)
            shootRoomDoors[2].SetActive(true);

        if (vortexRoomComplete && babyRoomComplete && slowRoomComplete)
            shootRoomPortalDoor.SetActive(false);
    }
    #endregion Tutorial Functions
}
