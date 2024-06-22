using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBinahToPosition : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        GameManager.Instance.BinahManager.SendBinahToLocation(transform.position);
        StartCoroutine(LockInput());
    }


    IEnumerator LockInput()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.playerInputLocked = true;
    }


}
