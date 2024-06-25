using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Elevadoor : MonoBehaviour
{
    public float delay = 0;
    public Animator panoramaAnimator;

    public MonoBehaviour music;

    public void Start()
    {
        Invoke("OpenDoor", delay);
    }





    private void OpenDoor()
    {
        GetComponent<Animator>().SetTrigger("ouverture");
        panoramaAnimator.speed = 0;

        IEventTriggerable eventInterface = music.GetComponent<IEventTriggerable>();
        eventInterface.TriggerEvent();
    }
}
