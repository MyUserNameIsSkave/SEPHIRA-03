using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation_EnemyReaction : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        GetComponent<Animator>().SetTrigger("React");
    }
}
