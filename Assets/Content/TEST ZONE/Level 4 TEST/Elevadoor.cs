using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Elevadoor : MonoBehaviour, IEventTriggerable
{
    public float delay = 0;
    public Animator panoramaAnimator;

    public void TriggerEvent()
    {
        Invoke("OpenDoor", delay);
    }

    private void OpenDoor()
    {
        GetComponent<Animator>().SetTrigger("ouverture");
        panoramaAnimator.speed = 0;
    }
}
