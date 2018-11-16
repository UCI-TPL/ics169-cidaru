using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    private GameObject player;
    private Health playerHp;
    private GunController gun;
    private Transform minimapPos;

    private void Awake()
    {
        gm = this;

        player = GameObject.Find("Player");
        playerHp = player.GetComponent<Health>();
        gun = player.GetComponent<GunController>();

        minimapPos = GameObject.Find("Minimap Objects").transform;

        Cursor.SetCursor(cursor, new Vector2(512 / 2, 512 / 2), CursorMode.Auto);
    }

    void Start () {
    }
	
	void Update () {
        if (playerHp.dead() && !gameOverMenu.activeSelf)
        {
            Time.timeScale = 0f;

            gameOverMenu.SetActive(true);

            GetComponent<PauseController>().enabled = false;
        }
    }

    public void updateMinimapPosition(Vector3 newPos)
    {
        minimapPos.position = newPos;
    }
}
