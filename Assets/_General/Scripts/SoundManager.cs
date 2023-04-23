using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioMixer audioMixer;

    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    //Para llamar a este metodo:
    //SoundManager.Instance.PlaySFX("");

    //Para parar la musica:
    //SoundManager.Instance.musicSource.Stop();
    public void PlayMusic(string name) {
        Sound sound = Array.Find(music, x=> x.name==name);
        musicSource.clip = sound.clip;
        musicSource.Play();
    }

    public void PlaySFX(string name) {
        Sound sound = Array.Find(sfx, x => x.name == name);
        if (sound != null) {
            sfxSource.clip = sound.clip;
            sfxSource.Play();
        }
    }

    public bool ToggleMusic() {
        return musicSource.mute = !musicSource.mute;
    }
    public bool ToggleSFX()
    {
        return sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10 (volume) * 20);
        //musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        //sfxSource.volume = volume;
    }
}
