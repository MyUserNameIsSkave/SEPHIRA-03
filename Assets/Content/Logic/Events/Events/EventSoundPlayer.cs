using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventSoundPlayer : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private AudioClip sound;

    public AudioSource precedentAudioSource;
    public float fadeTime = 0.5f; // temps de fade out en secondes

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TriggerEvent()
    {
        if (precedentAudioSource == null || !precedentAudioSource.isPlaying)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
        else if (precedentAudioSource.isPlaying)
        {
            StartCoroutine(FadeOutAndPlay(precedentAudioSource, sound));
        }
    }

    private IEnumerator FadeOutAndPlay(AudioSource audioToFade, AudioClip clipToPlay)
    {
        float startVolume = audioToFade.volume;

        while (audioToFade.volume > 0)
        {
            audioToFade.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioToFade.Stop();
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
