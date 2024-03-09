using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventPlayerSound : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private AudioClip sound;

    public void TriggerEvent()
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }


}
