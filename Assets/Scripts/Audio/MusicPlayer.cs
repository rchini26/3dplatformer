using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;
    
    private MusicSetup _currentMusicSetup;

    private void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        _currentMusicSetup = AudioManager.Instance.GetMusicSetupByType(musicType);
        audioSource.clip = _currentMusicSetup.audioClip;
        audioSource.Play();
    }
}
