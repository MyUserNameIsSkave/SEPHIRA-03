using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrouchTutorialManager : MonoBehaviour, IEventTriggerable
{

    public static bool alreadyTriggered = false;

    public void TriggerEvent()
    {
        if (alreadyTriggered == true)
        {
            return;
        }

        alreadyTriggered = true;
        GetComponent<Image>().enabled = true;
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.C))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
