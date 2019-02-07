using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject[] doors;

    public bool portalTrigger = false;

    void Awake()
    {
        foreach (GameObject door in doors)
            door.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.gm.updateMinimapPosition(transform.parent.position);

            enabled = false;

            if (portalTrigger)
                GameObject.Find("Portal").GetComponent<NextLevel>().Activate();

            foreach (GameObject door in doors)
            {
                if (gameObject.name == "Entry Trigger")
                    door.SetActive(true);
                else
                    door.SetActive(false);
            }
        }
    }
}
