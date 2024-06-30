using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("MusicVolume", -80f); // Volume muito baixo para representar mute
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Usar dB
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            audioMixer.SetFloat("SFXVolume", -80f); // Volume muito baixo para representar mute
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20); // Usar dB
        }
    }
}
