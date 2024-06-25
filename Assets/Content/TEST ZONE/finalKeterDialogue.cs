using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalKeterDialogue : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.transform.position);
    }

}
