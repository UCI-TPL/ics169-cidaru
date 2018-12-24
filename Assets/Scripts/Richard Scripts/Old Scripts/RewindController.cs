using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindController : MonoBehaviour
{
    public float numberOfSecs = 2;
    private List<Vector3> pointsInTime = new List<Vector3>();

    private void FixedUpdate()
    {
        Record();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Rewind();
        }
    }

    private void Rewind()
    {
        transform.position = pointsInTime[0];

        pointsInTime.Clear();
    }

    private void Record()
    {
        if (pointsInTime.Count > Mathf.Round(numberOfSecs / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(0);
        }

        pointsInTime.Add(transform.position);
    }
}
