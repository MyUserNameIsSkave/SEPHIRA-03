using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeterMusic : MonoBehaviour, IEventTriggerable
{
    public AudioClip clip;
    private AudioSource source;
    public float triggerDelay = 1;

    [Header("    AUDIO SETTINGS")]

    public float inElevatorVolume = 0.025f;
    public float outElevatorVolume = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = inElevatorVolume;
        source.clip = clip;
        source.Play();
        source.loop = true;

    }



    public void TriggerEvent()
    {
        //Verifier si trigger une seconde fois et remplacer la musique par celle de Binah

        //Premier trigger
        Invoke("ChangeMusic", triggerDelay);
    }


    private void ChangeMusic()
    {
        source.bypassEffects = true;
        source.volume = outElevatorVolume;
        source.maxDistance = 1000;
        source.minDistance = 1000;
        source.spatialBlend = 0;


    }


}
