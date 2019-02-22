using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarMapUpdate : MonoBehaviour
{
    /// <summary>
    /// This is literally just to make sure the graphs update regularly
    /// for the enemies. If this is not included, the penalties for where
    /// the enemies are currently will not be properly updated.
    /// </summary>
    
    private void FixedUpdate()
    {
        GetComponent<GraphUpdateScene>().Apply();
    }
}
