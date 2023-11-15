using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventTriggerable
{

    /// <summary>
    /// To put in an script containing an Event that can be Triggered from the exterior.
    /// </summary>
    public void TriggerEvent();

}
