using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private bool startAutomaticaly;

    [SerializeField]
    private float initialDelay;

    [Space(20)]

    [SerializeField]
    private AudioClip[] musics;


    private AudioSource source;
    private int i = 0;

    public void TriggerEvent()
    {
        StartNextMusic();
    }



    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();


        if (startAutomaticaly)
        {
            Invoke("StartMusic", initialDelay);
        }
    }


    private void StartMusic()
    {
        StartCoroutine(StartNextMusic());
    }


    IEnumerator StartNextMusic()
    {
        source.clip = musics[i];
        source.Play();
        i++;

        yield return null;
    }
    
}
