using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXController", menuName = "SFXController")]
public class SFXStorage : ScriptableObject
{
    [Header("List of SFX")]
    public List<AudioClip> soundEffs = new List<AudioClip>();
}
