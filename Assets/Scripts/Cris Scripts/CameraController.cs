using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public MoveType moveType;

    private Vector3 offset;
    public float smoothTime = 0.125f;

    private Vector3 velocity;

    public enum MoveType
    {
        basicFollow, smoothDampFollow, LerpFollow
    }

    void Start () {
        offset = transform.position - player.transform.position;
	}
	
	void FixedUpdate () {
        switch (moveType)
        {
            case MoveType.smoothDampFollow:
                smoothDampFollow();
                break;
            case MoveType.LerpFollow:
                LerpFollow();
                break;
            default:
                basicFollow();
                break;
        }
	}

    void basicFollow()
    {
        transform.position = player.transform.position + offset;
    }

    void smoothDampFollow()
    {
        Vector3 newPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void LerpFollow()
    {
        Vector3 newPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothTime);
    }
}
