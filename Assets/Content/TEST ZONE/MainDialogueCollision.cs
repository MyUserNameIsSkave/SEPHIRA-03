using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialogueCollision : MonoBehaviour
{
    [SerializeField]
    private CameraBase linkedCamera;

    [SerializeField]
    private MonoBehaviour dialogueToTrigger;


    private CheckpointCollision[] checkpoints = null;
    private int index;


    private void Awake()
    {
        checkpoints = FindObjectsOfType<CheckpointCollision>();
        index = Array.IndexOf(checkpoints, this);

        if (GameManager.ValidatedCheckpoints.Contains(index))
        {
            GetComponent<Collider>().enabled = false;
        }

        //VERIFIER SI DEJA UITLISE AVANT SI OU DISABLE
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Binah"))
        {
            return;
        }

        GetComponent<Collider>().enabled = false;
        StartCoroutine(CheckForCamera());
    }




    IEnumerator CheckForCamera()
    {
        
            while (true)
            {
                if (linkedCamera.alreadyUsed)
                {
                    IEventTriggerable eventInterface = dialogueToTrigger as IEventTriggerable;
                    eventInterface.TriggerEvent();

                    yield break;
                }

                yield return 0;
            }
        
        
    }
}
