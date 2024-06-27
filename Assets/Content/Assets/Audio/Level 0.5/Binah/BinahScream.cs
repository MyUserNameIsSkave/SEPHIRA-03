using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BinahScream : MonoBehaviour, IEventTriggerable
{
    public AudioClip audioClip; // Le clip audio à lire
    private float fadeDuration = 1f; // Durée de la réduction du volume en secondes
    private float waitTime = 4f; // Temps d'attente entre chaque lecture du son en secondes
    private bool BinahStopScreaming;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundWithFade());
    }

    public void TriggerEvent()
    {
        BinahStopScreaming = true;  
        StartCoroutine(StopSound());
    }

    IEnumerator PlaySoundWithFade()
    {
        while (true)
        {
            if (BinahStopScreaming == false && audioSource != null)
            {
                Debug.Log("Hurle");
                // Lire le son
                audioSource.clip = audioClip;
                audioSource.volume = 0.1f;
                audioSource.Play();

                // Attendre jusqu'à la fin du son
                yield return new WaitForSeconds(audioClip.length);

                // Réduire le volume progressivement
                float startTime = Time.time;
                float endTime = startTime + fadeDuration;
                while (Time.time < endTime && audioSource != null)
                {
                    float t = (Time.time - startTime) / fadeDuration;
                    audioSource.volume = Mathf.Lerp(0.1f, 0f, t);
                    yield return null;
                }

                // Réinitialiser le volume et attendre avant de relire le son
                audioSource.volume = 0.1f;
                yield return new WaitForSeconds(waitTime);
            }

        }
    }
    IEnumerator StopSound()
    {
        while (true && audioSource != null)
        {
            yield return new WaitForSeconds(1f);
            float startTime = Time.time;
            float endTime = startTime + fadeDuration;
            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / fadeDuration;
                audioSource.volume = Mathf.Lerp(0.1f, 0f, t);
                yield return null;
            }

            StopCoroutine(PlaySoundWithFade());
            Destroy(audioSource);
            break;
        }
    }
}


