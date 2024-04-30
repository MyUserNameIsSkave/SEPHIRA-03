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
    private bool isOn = true;

    [SerializeField]
    private AudioClip[] _audioClips;

    [SerializeField]
    private int musicTrack;

    [SerializeField]
    private float onVolume= 1f;










    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (_audioClips.Count() <= musicTrack - 1)
        {
            _audioSource.clip = _audioClips[musicTrack - 1];
            Apply();
        }

        Apply();
    }




    public override void Interaction()
    {
        Switch();
    }

    public override void TriggerEvent()
    {
        Switch();
    }



    private void Switch()
    {
        isOn = !isOn;
        Apply();
    }

    private void Apply()
    {
        if (isOn)
        {
            _audioSource.volume = onVolume;
        }
        else
        {
            _audioSource.volume = 0.0f;

        }
    }

}
