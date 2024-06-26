using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSound : MonoBehaviour
{
    public AudioClip audioClip; // Le clip audio à lire
    private float fadeDuration = 1f; // Durée de la réduction du volume en secondes
    private float waitTime = 10f; // Temps d'attente entre chaque lecture du son en secondes

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundWithFade());
    }

    IEnumerator PlaySoundWithFade()
    {
        while (true)
        {
            // Lire le son
            audioSource.clip = audioClip;
            audioSource.volume = 0.1f;
            audioSource.Play();

            // Attendre jusqu'à la fin du son
            yield return new WaitForSeconds(audioClip.length);

            // Réduire le volume progressivement
            float startTime = Time.time;
            float endTime = startTime + fadeDuration;
            while (Time.time < endTime)
            {
                float t = (Time.time - startTime) / fadeDuration;
                audioSource.volume = Mathf.Lerp(1f, 0f, t);
                yield return null;
            }

            // Réinitialiser le volume et attendre avant de relire le son
            audioSource.volume = 0.1f;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
