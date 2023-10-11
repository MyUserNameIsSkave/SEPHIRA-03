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


    private float ladderLength;
    private float lerpNormalizer;

    private float startTime;


    private void Start()
    {
        link = GetComponent<NavMeshLink>();
        
        ladderLength = Vector3.Distance(link.startPoint, link.endPoint);
        lerpNormalizer = ladderLength / climbingSpeed;


    }





    


    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        NavMeshAgent  _agent = other.GetComponent<NavMeshAgent>();

        if (_agent)
        {
            startTime = Time.time;
            agent = _agent;
            checkState = StartCoroutine(CheckState());
        }
    }



    private void OnTriggerExit(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject == agent.gameObject)
        {
            StopCoroutine(checkState);
            return;
        }
    }







    IEnumerator CheckState()
    {
        while (agent.isOnOffMeshLink)
        {
            yield return new WaitForFixedUpdate();
            UpdatePosition();
        }



        yield return null;
        StartCoroutine(CheckState());
    }




    private void UpdatePosition()
    {
        print("Update Position");

        //Lerp position


        float timeProgression = Time.time - startTime;
        float lerpProgression = timeProgression / lerpNormalizer;

        float heightProgression = Mathf.Lerp(link.startPoint.y, link.endPoint.y, lerpProgression);


        agent.gameObject.transform.position = new Vector3(link.startPoint.x, heightProgression, link.startPoint.z);

    }
}
