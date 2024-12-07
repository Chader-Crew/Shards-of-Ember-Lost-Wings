using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DubPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    [SerializeField] private float[] delays;
    
    private AudioSource source;
    private int index;
    public void StartSequence()
    {
        index = 0;
        source = GetComponent<AudioSource>();
        source.clip = audios[index];
        source.Play();
        this.CallWithDelay(PlayNext, delays[index]);
        index++;
    }
    private void PlayNext()
    {
        if(index < audios.Length)
        {
            source.clip = audios[index];
            source.Play();
            this.CallWithDelay(PlayNext, delays[index]);
            index++;
        }
    }
}
