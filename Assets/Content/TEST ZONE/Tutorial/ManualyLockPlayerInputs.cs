using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualyLockPlayerInputs : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        StartCoroutine(LockInputs());
    }


    IEnumerator LockInputs()
    {
        yield return new WaitForEndOfFrame(); 
        GameManager.Instance.playerInputLocked = true;
    }
}
