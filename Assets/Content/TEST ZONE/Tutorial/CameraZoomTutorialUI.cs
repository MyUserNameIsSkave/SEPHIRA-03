using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTutorialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject introLevelManager;

    [SerializeField]
    private MonoBehaviour dialogueToTrigger;



    private void FixedUpdate()
    {
        if (GameManager.Instance.CameraController.currentFOV < 30)
        {
            IEventTriggerable eventInterface = dialogueToTrigger as IEventTriggerable;
            eventInterface.TriggerEvent();

            Destroy(transform.parent.gameObject);
            Destroy(introLevelManager);
        }
    }
}
