using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation_BinahEscape : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    GameObject BinahSupport;

    public void TriggerEvent()
    {
        GetComponent<Animator>().SetTrigger("Freed");
        Destroy(BinahSupport);
    }
}
