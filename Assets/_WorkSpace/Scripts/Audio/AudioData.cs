using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/Data")]
public class AudioData : ScriptableObject
{
    public string ClipName;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume = 1.0f;
}
