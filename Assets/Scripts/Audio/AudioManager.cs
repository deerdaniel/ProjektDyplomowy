using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    //public static AudioManager instance;
    public Slider volumeSlider;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //DontDestroyOnLoad(gameObject);
        volumeSlider.value = AudioListener.volume;
        foreach (Sound sound in sounds)
        {
           sound.audioSource = gameObject.AddComponent<AudioSource>();
           sound.audioSource.clip = sound.audioClip;
           sound.audioSource.volume = sound.audioVolume;
           sound.audioSource.pitch = sound.audioPitch;
           sound.audioSource.loop = sound.loop;
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
        AudioListener.volume = volumeSlider.value;
        //ave();
    }
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    public void Play(string name)
    {
       Sound sound = Array.Find(sounds, sound => sound.audioName == name);
        if (sound == null) { return; }
       sound.audioSource.Play();
    }
}
