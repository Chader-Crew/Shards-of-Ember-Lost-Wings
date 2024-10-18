using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

//tambem controla os elementos de interface que nao sao sliders
public class SliderController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public TMP_Dropdown graphicsQualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    public Toggle fullScreenToggle;

    private void Start()
    {
        // Popula o dropdown das resolucoes
        resolutions = Screen.resolutions;
        List<string> resolutionTexts = new List<string>();

        foreach(Resolution resolution in resolutions)
        {
            resolutionTexts.Add(resolution.width + " x " + resolution.height);
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionTexts);
         

        // Inicializar sliders com os valores atuais do volume
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        graphicsQualityDropdown.value = PlayerPrefs.GetInt("GraphicsQuality",2);
        graphicsQualityDropdown.RefreshShownValue();
        resolutionDropdown.value = PlayerPrefs.GetInt("ScreenResolution", 4);
        resolutionDropdown.RefreshShownValue();
        fullScreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen",0) == 1? true:false;


        // Adicionar listeners aos sliders
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        graphicsQualityDropdown.onValueChanged.AddListener(SetGraphicsQuality);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);

        // Atualizar o volume inicial
        SetMusicVolume(musicVolumeSlider.value);
        SetSFXVolume(sfxVolumeSlider.value);
        SetGraphicsQuality(graphicsQualityDropdown.value);
        SetResolution(resolutionDropdown.value);
        SetFullScreen(fullScreenToggle);
    }


    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.instance.SetSFXVolume(Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ScreenResolution", resolutionIndex);
    }

    public void SetFullScreen(bool _isFullScreen)
    {
        Screen.fullScreen = _isFullScreen;
        PlayerPrefs.SetInt("Fullscreen", _isFullScreen?1:0);
    }
}
