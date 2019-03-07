using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMomControls : MonoBehaviour
{
    public float movementSpeed = 16.8f;

    public GameObject babyBomb;

    private Vector3 targetPosition;
    private Vector3 originalPosition;

    private bool firstPhase;
    private bool secondPhase;

    public void Awake()
    {
        firstPhase = true;
        secondPhase = false;
        targetPosition = transform.position - new Vector3(2.9f, 0, 0);
        originalPosition = transform.position;
    }

    public void Update()
    {
        if (firstPhase)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position.x - targetPosition.x < 0.001f)
            {
                //Instantiate(babyBomb, transform.position, Quaternion.identity);
                babyBomb.SetActive(true);
                
                firstPhase = false;
                secondPhase = true;
            }
        }
        else if (secondPhase)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);

            if (transform.position.x - originalPosition.x > -0.001f)
            {
                GetComponent<IntroEchoController>().enabled = false;
                firstPhase = false;
                secondPhase = false;
            }
        }
        
    }
}
