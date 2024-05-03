using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChokmahLogic : MonoBehaviour
{
    public ChokmahPath patrolRoute;


    private NavMeshAgent agent;

    private 


    int currentIndex = 0;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(PatrolCoroutine());
    }



    IEnumerator PatrolCoroutine()
    {
        agent.SetDestination(patrolRoute.PatrolPoints[currentIndex].transform.position);
        yield return new WaitForSeconds(0.1f);



        while (true)
        {

            // No Patrol Route
            if (patrolRoute == null)
            {
                Debug.Log("No Patrol Route");

                //Loop on FixedUpdate
                yield return new WaitForFixedUpdate();
                continue;
            }


            // Should Not be Moving to Pass
            if (agent.velocity.magnitude != 0)
            {
                //Loop on FixedUpdate
                yield return new WaitForFixedUpdate();
                continue;
            }


            int previousIndex = currentIndex;

            currentIndex += 1;
            if (currentIndex >= patrolRoute.PatrolPoints.Length)  // CurrentIndex comapred to MaxIndex
            {
                if (patrolRoute.IsLoop)
                {
                    currentIndex = 0;
                }
                else if (patrolRoute.DestroyAutomaticaly)
                {
                    Destroy(gameObject);
                }
                else
                {
                    yield break;
                }
            }


            //Delay frome the Waiting Time of the Current Point
            yield return new WaitForSeconds(patrolRoute.PatrolPoints[previousIndex].GetComponent<PatrolPoint>().WaitTime);         // Devrait pas etre current mais past


            //Move Agent
            agent.SetDestination(patrolRoute.PatrolPoints[currentIndex].transform.position);


            //Loop with delay top prevent the random skipping of points
            yield return new WaitForSeconds(0.1f);

        }
    }
}
