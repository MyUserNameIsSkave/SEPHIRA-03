using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disjoncteur : AII_EventTrigger, IEventTriggerable
{
    [SerializeField]
    private MeshRenderer _light;

    [SerializeField]
    private Door door;


    private bool done = false;

    public void TriggerEvent()      // NE PAS TOUCHER
    {
        if (done == false)
        {
            GetComponent<BoxCollider>().enabled = true;
            done = true;
        }
        
    }

    public override void InteractionSucceed()
    {
        door.StartCoroutine("DisjoncteurActivated");

        _light.material = new Material(_light.material);
        _light.material.color = Color.green;

        GetComponent<BoxCollider>().enabled = false;
    }
}
