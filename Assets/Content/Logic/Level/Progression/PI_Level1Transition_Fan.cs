using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_Level1Transition_Fan : Player_Interaction
{
    public AudioSource audioSource; // L'AudioSource � modifier
    public AudioSource audioSourceToFadeOut; // L'AudioSource � r�duire
    public AudioSource audioSourceToFadeOut2; // L'AudioSource � r�duire
    public AudioClip newAudioClip; // Le nouveau son � lire
    private float fadeDuration = 1f; // La dur�e de la transition en douceur (smooth)
    private float waitTime = 0.2f; // Le temps � attendre avant de changer le son

    public override void Interaction()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("stop");

        // Lance la coroutine pour changer le son de mani�re fluide apr�s un temps d�fini
        StartCoroutine(SmoothlyChangeAudioClip(waitTime));
    }

    IEnumerator SmoothlyChangeAudioClip(float waitTime)
    {
        // Attend le temps sp�cifi�
        yield return new WaitForSeconds(waitTime);

        // Sauvegarde le volume actuel de la premi�re AudioSource
        float originalVolume = audioSource.volume;

        // Sauvegarde le volume actuel de la deuxi�me AudioSource
        float originalVolumeToFadeOut = audioSourceToFadeOut.volume;
        float originalVolumeToFadeOut2 = audioSourceToFadeOut2.volume;

        // D�finit le volume � 0 pour commencer la transition en douceur
        audioSource.volume = 0f;

        // Change le clip audio
        audioSource.clip = newAudioClip;

        // Lit le nouveau son
        audioSource.Play();

        // Interpole le volume entre 0 et le volume d'origine pendant la dur�e sp�cifi�e
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            // Augmente le volume de la premi�re AudioSource
            audioSource.volume = Mathf.Lerp(0f, originalVolume, t / fadeDuration);

            // R�duit le volume de la deuxi�me AudioSource
            audioSourceToFadeOut.volume = Mathf.Lerp(originalVolumeToFadeOut, 0f, t / fadeDuration);
            audioSourceToFadeOut2.volume = Mathf.Lerp(originalVolumeToFadeOut2, 0f, t / fadeDuration);

            yield return null;
        }

        // S'assure que le volume atteint la valeur d'origine
        audioSource.volume = originalVolume;
        audioSourceToFadeOut.volume = 0.4f;
        audioSourceToFadeOut2.volume = 0.4f;
        // Attend la fin de la lecture du nouveau son
        yield return new WaitForSeconds(2f);

        // Supprime l'ancienne AudioSource
        Destroy(audioSource);
    }


    public override void TriggerEvent()
    {
        //throw new System.NotImplementedException();
    }
}
