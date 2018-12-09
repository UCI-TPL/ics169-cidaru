using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class AddToTargetGroup : MonoBehaviour {
    public CinemachineTargetGroup targetGroup;

    HashSet<CinemachineTargetGroup.Target> targets = new HashSet<CinemachineTargetGroup.Target>();

    private void OnTriggerEnter2D(Collider2D col)
    {
       // print("in onTriggerEnter2D");
        //print(GameObject.FindGameObjectsWithTag("Enemy"));
        if(col.tag == "Door Trigger")
        {
            targets.Add(new CinemachineTargetGroup.Target { target = gameObject.transform, radius = 0f, weight = 1f });
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")){
                targets.Add(new CinemachineTargetGroup.Target { target = go.transform, radius = 0f, weight = 1f });
            }
        }
        //print(targets.ToArray());
        targetGroup.m_Targets = targets.ToArray();
    }
}