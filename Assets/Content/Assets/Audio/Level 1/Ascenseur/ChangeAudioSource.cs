using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAudioSource : MonoBehaviour, IEventTriggerable
{
    public AudioClip clip; // Clip audio

    private AudioSource audioSource; // R�f�rence � l'AudioSource

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void TriggerEvent()
    {
        audioSource.clip = clip; // D�finit le nouveau clip � jouer
        audioSource.volume = 0.7f;
        audioSource.Play(); // Joue le nouveau clip
    }

}
