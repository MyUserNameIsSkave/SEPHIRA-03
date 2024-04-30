using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;




public class PI_Radio : Player_Interaction
{
    
    private AudioSource _audioSource;



    [Header("    SETTINGS")]
    [Space(15)]

    [SerializeField]
    private bool defaultState = true;

    [Space(10)]

    [SerializeField]
    private AudioClip[] _audioClips;

    [SerializeField]
    private int musicTrack;










    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClips[musicTrack - 1];

        _audioSource.Play();


        if (defaultState == true)
        {
            _audioSource.mute = false;
        }
        else
        {
            _audioSource.mute = true;
        }
    }




    public override void Interaction()
    {
        //Switch();
    }

    public override void TriggerEvent()
    {
        Switch();
    }



    private void Switch()
    {
        print(_audioSource.mute);
        _audioSource.mute = !_audioSource.mute;

    }


}
