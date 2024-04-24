using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointCollision : MonoBehaviour
{
     
    public CameraBase checkpointCamera;


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
            if (checkpointCamera.alreadyUsed)
            {
                if (!GameManager.ValidatedCheckpoints.Contains(index))
                {
                    GameManager.ValidatedCheckpoints.Add(index);
                }



                GameManager.CurrentIndex = index;
                yield break;
            }

            yield return 0;
        }
    }
}
