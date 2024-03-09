using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;



public class LoopSoundCrossFade : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private AudioClip loopSound;

    [SerializeField]
    private AudioClip transitionSound;

    [SerializeField]
    private AudioClip endLoopSound;


    [Space(20)]


    [SerializeField]
    private AudioSource loopSource;

    [SerializeField]
    private AudioSource transitionSource;

    [SerializeField]
    private AudioSource endLoopSource;


    [Space (20)]


    [SerializeField]
    private float crossfadeSpeed = 0.5f;




    private bool firstLoopEnded = false;



    bool alreadyTriggered = false;


    // Start is called before the first frame update
    void Start()
    {
        loopSource.clip = loopSound;
        loopSource.loop = true;
        loopSource.Play();
    }




    public void TriggerEvent()
    {
        firstLoopEnded = true;
        StartCoroutine(FadeIt());
    }


    private void Update()
    {
        

        if (firstLoopEnded && !transitionSource.isPlaying && !alreadyTriggered)
        {
            alreadyTriggered = true;
            endLoopSource.clip = endLoopSound;
            endLoopSource.Play();
        }
    }





    IEnumerator FadeIt()
    {
        //set original audiosource volume and clip
        float t = 0;
        float lbv = loopSource.volume;
        float ebv = transitionSource.volume;



        transitionSource.clip = transitionSound;
        transitionSource.Play();




        while (t < 0.98f)
        {
            t = Mathf.Lerp(t, 1f, Time.deltaTime * crossfadeSpeed);
            loopSource.volume = Mathf.Lerp(lbv, 0f, t);
            transitionSource.volume = Mathf.Lerp(0f, ebv, t);
            yield return null;

        }

        loopSource.Stop();

        yield break;
    }



}
