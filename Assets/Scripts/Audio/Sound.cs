using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string audioName;
    public AudioClip audioClip;
    public float audioVolume;
    public float audioPitch;
    public bool loop;
    [HideInInspector]
    public AudioSource audioSource;
}
