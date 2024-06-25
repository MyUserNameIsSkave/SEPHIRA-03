using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    [SerializeField]
    private AudioClip[] clips2;

    public AudioSource audioSourceStep;
    public AudioSource audioSourceClimbStep;
    // Start is called before the first frame update

    private void Awake()
    {
        if (audioSourceStep == null && clips2.Length == 0)
        {
            audioSourceStep = GetComponent<AudioSource>();
        }
        
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSourceStep.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }

    private void ClimbStep()
    {
        AudioClip clip = GetRandomClipClimb();
        audioSourceClimbStep.PlayOneShot(clip);
    }
    private AudioClip GetRandomClipClimb()
    {
        return clips2[UnityEngine.Random.Range(0, clips.Length)];
    }
}
