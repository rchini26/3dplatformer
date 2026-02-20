using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class AudioManager : Singleton<AudioManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicSetupByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicSetupByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }
    
    public void PlaySFXByType(SFXType sfxType)
    {
        var sfx = GetSFXSetupByType(sfxType);
        sfxSource.PlayOneShot(sfx.audioClip);
    }
    
    public SFXSetup GetSFXSetupByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }
}

public enum MusicType
{
    Type01,
    Type02
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    Type01,
    Type02
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}