using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ladder : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent agent;


    private void OnTriggerEnter(Collider other)
    {
        if (!agent)
        {
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != agent.gameObject)
        {
            return;
        }
    }




    //Enter Collider
    // Collider is Reference -> Start Coroutine
    // Collider Not Refernce -> Set Reference -> Start Coroutine

    //Exit Collider
    // Collider is Reference -> Stop Coroutine





    IEnumerator CheckState()
    {
        while (agent.isOnOffMeshLink)
        {
            StartCoroutine(CheckState());
            yield return null;
        }





    }




    private void UpdatePosition()
    {
        print("Update Position");
    }
}
