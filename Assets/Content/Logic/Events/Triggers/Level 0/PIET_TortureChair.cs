using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIET_TortureChair : PI_EventTrigger, IEventTriggerable
{

    public GameObject clickIndicator;


    public override void TriggerEvent()
    {
       GetComponent<MeshCollider>().enabled = false;
       CheckForCameraChange();
       CheckForEventsToDo();
       clickIndicator.GetComponent<Canvas>().enabled = false;
    }
}
