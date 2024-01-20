using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy_PatrolState : Enemy_NeutralState
{
    private int currentIndex = 0;
    private int maxIndex;









    PatrolRoute chosedRoute;




    public override void AwakeState()
    {

    }




    List<PatrolRoute> partrolRoutes = new List<PatrolRoute>();



    public override void EnterState()
    {
        partrolRoutes.Clear();


        //Récupère
        Collider[] colliders = Physics.OverlapSphere(BaseManager.transform.position, BaseManager.PatrolDetectionRadius, LayerMask.GetMask("Patrol Route"));

        //Récupère les scripts
        foreach (Collider collider in colliders)
        {
            partrolRoutes.Add(collider.GetComponent<PatrolRoute>());
        }

        chosedRoute = partrolRoutes[Random.Range(0, partrolRoutes.Count)];




        float currentDistance = Mathf.Infinity;

        foreach (GameObject partrolPoint in chosedRoute.PatrolPoints)
        {
            float testDistance = Vector3.Distance(BaseManager.transform.position, partrolPoint.transform.position);

            if (testDistance > currentDistance)
            {

                currentDistance = testDistance;


                currentIndex = System.Array.IndexOf(chosedRoute.PatrolPoints, partrolPoint) - 1;
            }
        }



        maxIndex = chosedRoute.PatrolPoints.Length;
    }

    public override void ExitState()
    {
        chosedRoute = null;
    }

    public override void FixedUpdateState()
    {
        if (chosedRoute == null)
        {
            Debug.Log("chosedRoute not defined yet");
            return;
        }



        if (BaseManager.Agent.velocity.magnitude != 0)
        {
            return;
        }

        currentIndex += 1;
        if (currentIndex >= maxIndex)
        {
            currentIndex = 0;
        }


        BaseManager.MoveAgent(chosedRoute.PatrolPoints[currentIndex].transform.position);

    }





    public override void StartState()
    {

    }

    public override void UpdateState()
    {

    }
}
