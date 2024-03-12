using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EventIntermediaire : MonoBehaviour, IEventTriggerable
{

    [SerializeField]
    private MonoBehaviour eventTarget;

    public void TriggerEvent()
    {
        if (eventTarget is IEventTriggerable myInterface)
        {
            myInterface.TriggerEvent();
        }
    }
}
