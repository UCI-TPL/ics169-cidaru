using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
    // Bomb abilities
    public Ability babyBomb;
    public Ability slowBomb;

    // Initialize all the values of the ability
    private void Awake()
    {
        initAbilityCharges();
    }

    private void Update()
    {
        // If not in pause state, allow player to use ability
        if (Time.timeScale != 0 && !GameManager.gm.cameraPanning && !GameManager.gm.spawningRooms)
        {
            if (GameManager.gm.isTutorial)
            {
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Left Bumper") || Input.GetButtonDown("Y Button")) && babyBomb.isAbilityReady() && CheckBabysTutorialCondition())
                {
                    // Creates baby bomb if avaliable and button is pressed
                    babyBomb.PutOnCooldown();

                    Instantiate(babyBomb.abilityPrefab, transform.position, Quaternion.identity);
                }
                else if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("B Button")) && slowBomb.isAbilityReady() && CheckSlowTutorialCondition())
                {
                    // Creates slow bomb if avaliable and button is pressed
                    slowBomb.PutOnCooldown();

                    Instantiate(slowBomb.abilityPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Left Bumper") || Input.GetButtonDown("Y Button")) && babyBomb.isAbilityReady())
                {
                    // Creates baby bomb if avaliable and button is pressed
                    babyBomb.PutOnCooldown();

                    Instantiate(babyBomb.abilityPrefab, transform.position, Quaternion.identity);
                }
                else if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("B Button")) && slowBomb.isAbilityReady())
                {
                    // Creates slow bomb if avaliable and button is pressed
                    slowBomb.PutOnCooldown();

                    Instantiate(slowBomb.abilityPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    private bool CheckSlowTutorialCondition()
    {
        return (GameManager.gm.currentState == GameManager.TutorialStates.SlowRoomStart || GameManager.gm.currentState == GameManager.TutorialStates.SlowRoom ||
            GameManager.gm.currentState == GameManager.TutorialStates.SlowRoomEnd || GameManager.gm.currentState == GameManager.TutorialStates.SlowRoomPost ||
            GameManager.gm.currentState == GameManager.TutorialStates.PortalRoomPost);
    }

    private bool CheckBabysTutorialCondition()
    {
        return (GameManager.gm.currentState == GameManager.TutorialStates.BabyRoomStart || GameManager.gm.currentState == GameManager.TutorialStates.BabyRoom ||
            GameManager.gm.currentState == GameManager.TutorialStates.BabyRoomEnd || GameManager.gm.currentState == GameManager.TutorialStates.BabyRoomPost ||
            GameManager.gm.currentState == GameManager.TutorialStates.PortalRoomPost);
    }

    // Initialize all the values of the ability
    private void initAbilityCharges()
    {
        babyBomb.initAbility();
        slowBomb.initAbility();
    }
}
