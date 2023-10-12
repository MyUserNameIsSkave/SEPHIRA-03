using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Unity.VisualScripting;

public class Ladder : MonoBehaviour
{
    //Settings
    [SerializeField]
    float climbingSpeed;



    //References
    private NavMeshAgent agent;
    private Coroutine checkState;
    private NavMeshLink link;


    //Working Variables
    private Vector3 startPoint;
    private Vector3 endPoint;

    private float startTime;
    private float baseAgentSpeed;



    // ---------- LOGIC ----------



    private void Start()
    {
        //Self reference
        link = GetComponent<NavMeshLink>();

        //World position of the points
        startPoint = link.startPoint + transform.position;
        endPoint = link.endPoint + transform.position;
    }





    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        NavMeshAgent  _agent = other.GetComponent<NavMeshAgent>();

        if (_agent)
        {
            agent = _agent;
            baseAgentSpeed = agent.speed;
            checkState = StartCoroutine(CheckState());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject == agent.gameObject)
        {
            startTime = 0;
            //StopCoroutine(checkState);
            StopAllCoroutines();
            return;
        }
    }



    bool used = false;

    IEnumerator CheckState()
    {
        if (agent.isOnOffMeshLink)
        {
            StartUsing();

            //Adjust rotation to face the ladder
            agent.gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180, 0);
        }
        else if (startTime != 0)
        {
            EndUsing();
            yield break;
        }


        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckState());
    }




    /// <summary>
    /// Do what needs to be done when an Agent Start using the Link
    /// </summary>
    private void StartUsing()
    {
        if (startTime == 0)
        {
            startTime = Time.time;
            agent.speed = climbingSpeed;
            agent.GetComponent<Animator>().SetBool("ClimbingLader", true);


            if (Vector3.Distance(agent.transform.position, startPoint) > Vector3.Distance(agent.transform.position, endPoint))
            {
                //Going Up
                agent.GetComponent<Animator>().SetFloat("Ladder Start Position", 0f);

            }
            else
            {
                //Going Up
                agent.GetComponent<Animator>().SetFloat("Ladder Start Position", 1f);
            }

        }
    }


    /// <summary>
    /// What needs to be done when the Agent Ends using the Link
    /// </summary>
    private void EndUsing()
    {
        agent.GetComponent<Animator>().SetBool("ClimbingLader", false);
        agent.speed = baseAgentSpeed;
    }
}
