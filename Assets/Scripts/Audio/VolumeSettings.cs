using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    void Start()
    {
        // Load current volume from AudioMixer
        float currentVolume;
        if (audioMixer.GetFloat("MusicVolume", out currentVolume))
        {
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            volumeSlider.value = 1f; // Default to max volume
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
}
