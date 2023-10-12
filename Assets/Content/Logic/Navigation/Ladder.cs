using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    float climbingSpeed;



    // Variables
    private NavMeshAgent agent;
    private Coroutine checkState;
    private NavMeshLink link;

    [SerializeField]
    private Vector3 startPoint;
    [SerializeField]

    private Vector3 endPoint;

    private float ladderLength;
    [SerializeField]
    private float lerpNormalizer;

    private float startTime;


    private void Start()
    {
        link = GetComponent<NavMeshLink>();

        startPoint = link.startPoint + transform.position;
        endPoint = link.endPoint + transform.position;



        ladderLength = Vector3.Distance(startPoint, endPoint);
        lerpNormalizer = ladderLength / climbingSpeed;


    }





    


    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        NavMeshAgent  _agent = other.GetComponent<NavMeshAgent>();

        if (_agent)
        {
            agent = _agent;
            checkState = StartCoroutine(CheckState());
        }
    }



    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject == agent.gameObject)
        {
            startTime = 0;
            StopCoroutine(checkState);
            return;
        }
    }







    IEnumerator CheckState()
    {
        while (agent.isOnOffMeshLink)
        {
            print("ENTER");

            if (startTime == 0)
            {
                startTime = Time.time;
                agent.speed = climbingSpeed;
                agent.GetComponent<Animator>().SetBool("ClimbingLader", true);
            }

            // Rotate 
            // DOIT ADAPTER AU SENS DE NAVIGATION


            agent.gameObject.transform.rotation = gameObject.transform.rotation; // * Quaternion.Euler(0, 0, 0);

            yield return new WaitForFixedUpdate();
        }

        agent.GetComponent<Animator>().SetBool("ClimbingLader", false);

        agent.speed = 4;

        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckState());
    }


}
