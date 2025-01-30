using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public Slider VolumeSlider;

    private void Awake()
    {
        VolumeSlider.value = AudioListener.volume;
        foreach (Sound sound in Sounds)
        {
           sound.AudioSource = gameObject.AddComponent<AudioSource>();
           sound.AudioSource.clip = sound.AudioClip;
           sound.AudioSource.volume = sound.AudioVolume;
           sound.AudioSource.pitch = sound.AudioPitch;
           sound.AudioSource.loop = sound.Loop;
        }
    }
    private void Start()
    {
        Play("Theme");
    }
    public void PlayClick()
    {
        Play("Click");
    }
    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
    }
    public void Play(string name)
    {
       Sound sound = Array.Find(Sounds, sound => sound.AudioName == name);
        if (sound == null) { return; }
       sound.AudioSource.Play();
    }
}
