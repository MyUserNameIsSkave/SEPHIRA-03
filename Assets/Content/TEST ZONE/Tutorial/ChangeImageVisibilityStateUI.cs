using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageVisibilityStateUI : MonoBehaviour, IEventTriggerable
{

    [SerializeField]
    Image[] images;


    private void Start()
    {
        TriggerEvent();
    }


    public void TriggerEvent()
    {

        foreach (Image image in images)
        {
            image.enabled = !image.enabled;
        }
    }
}
