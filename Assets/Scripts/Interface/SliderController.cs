using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderController : MonoBehaviour
{
   AudioSource audioSource;
   public GameObject audioManager;
   public Slider volumeSlider;

   void Awake()
   {
        if (audioManager != null)
        {
            audioSource = audioManager.GetComponent<AudioSource>();
        }

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(MenuVolume); 
        }
   }

   public void MenuVolume(float value)
   {
         if (audioSource != null)
        {
            audioSource.volume = value;
        }
   }
}
