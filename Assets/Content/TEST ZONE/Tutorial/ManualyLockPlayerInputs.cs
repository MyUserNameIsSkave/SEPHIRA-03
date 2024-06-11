using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualyLockPlayerInputs : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        GameManager.Instance.playerInputLocked = true;
    }
}
