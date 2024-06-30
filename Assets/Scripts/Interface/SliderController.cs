using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        // Inicializar sliders com os valores atuais do volume
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // Adicionar listeners aos sliders
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        // Atualizar o volume inicial
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
