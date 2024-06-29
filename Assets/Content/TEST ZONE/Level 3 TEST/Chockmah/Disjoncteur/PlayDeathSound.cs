using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDeathSound : MonoBehaviour, IEventTriggerable
{
    public AudioSource chokmahDeathAudio;
    // Start is called before the first frame update


    public void TriggerEvent()
    {
       
        chokmahDeathAudio.Play();
    }

}
