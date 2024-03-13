using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RencontreBinahSalleTorture : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    GameObject SourceAudio;
    // Start is called before the first frame update
    public void TriggerEvent()
    {
        SourceAudio.SetActive(true);
    }
}
