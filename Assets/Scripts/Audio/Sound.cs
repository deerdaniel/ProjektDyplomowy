using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string AudioName;
    public AudioClip AudioClip;
    public float AudioVolume;
    public float AudioPitch;
    public bool Loop;
    [HideInInspector]
    public AudioSource AudioSource;
}
