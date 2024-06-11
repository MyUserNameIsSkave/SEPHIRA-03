using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        GetComponent<Image>().enabled = true;
    }


}
