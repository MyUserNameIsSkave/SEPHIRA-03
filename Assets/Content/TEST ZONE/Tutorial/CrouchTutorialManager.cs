using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrouchTutorialManager : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        if (GameManager.CurrentIndex != -1)
        {
            return;
        }

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
