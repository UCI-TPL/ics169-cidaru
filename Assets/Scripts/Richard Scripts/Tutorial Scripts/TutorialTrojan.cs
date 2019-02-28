using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrojan : MonoBehaviour
{
    public GameObject tutorialTree;
    public GameObject sprout;

    public void babyTutorialTree()
    {
        Instantiate(sprout, tutorialTree.transform.position, Quaternion.identity);
        Destroy(tutorialTree);
    }
}
