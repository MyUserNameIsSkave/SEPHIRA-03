using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ChokmahMusic : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private AudioClip musicStart, musicLoop;

    private AudioSource source;

    public void TriggerEvent()
    {
        StartMusic();
    }



    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    private void StartMusic()
    {
        StartCoroutine(StartNextMusic());
    }


    IEnumerator StartNextMusic()
    {
        source.clip = musicStart;
        source.Play();
        source.loop = false;

        yield return new WaitForSeconds(musicStart.length);

        source.clip = musicLoop;
        source.loop = true;
    }
}
