using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource AudioSource;

    public Slider volumeSlider;

    private float musicVolume = 1f;

    void Start()
    {
        AudioListener.pause = false;
        musicVolume = PlayerPrefs.GetFloat("sfxVolume");
        AudioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    public void playSound()
    {
        AudioSource.Play();
    }

    void Update()
    {
        AudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("sfxVolume", musicVolume);
    }

    public void updateVolume (float volume)
    {
        musicVolume = volume;
    }

}
