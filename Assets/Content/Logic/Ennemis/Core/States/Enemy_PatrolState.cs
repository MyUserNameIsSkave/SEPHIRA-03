using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy_PatrolState : Enemy_NeutralState
{
    private int currentIndex = 0;




    public override void AwakeState()
    {

    }




    List<PatrolRoute> partrolRoutes = new List<PatrolRoute>();



    public override void EnterState()
    {
        //// Search for a Patrol Route
        //#region
        //if (BaseManager.ChosedPatrolRoute == null)
        //{
        //    partrolRoutes.Clear();



        //    //Récupère
        //    Collider[] colliders = Physics.OverlapSphere(BaseManager.transform.position, BaseManager.PatrolDetectionRadius, LayerMask.GetMask("Patrol Route"));

        //    //Récupère les scripts
        //    foreach (Collider collider in colliders)
        //    {
        //        partrolRoutes.Add(collider.GetComponent<PatrolRoute>());
        //    }

        //    BaseManager.ChosedPatrolRoute = partrolRoutes[Random.Range(0, partrolRoutes.Count)];
        //}
        //#endregion





        //// Start from Closest Point
        //#region

        //float currentDistance = Mathf.Infinity;

        //foreach (GameObject partrolPoint in BaseManager.ChosedPatrolRoute.PatrolPoints)
        //{
        //    float testDistance = Vector3.Distance(BaseManager.transform.position, partrolPoint.transform.position);

        //    if (testDistance > currentDistance)
        //    {

        //        currentDistance = testDistance;


        //        currentIndex = System.Array.IndexOf(BaseManager.ChosedPatrolRoute.PatrolPoints, partrolPoint) - 1;
        //    }
        //}

        //#endregion




        BaseManager.StartPatrolling();
    }




    public override void ExitState()
    {
        BaseManager.StopPatrolling();
        BaseManager.ChosedPatrolRoute = null;
    }












    public override void FixedUpdateState()
    {
        //if (BaseManager.PatrolRoute == null)
        //{
        //    Debug.Log("chosedRoute not defined yet");
        //    return;
        //}






        ////Return a en mouvement
        //if (BaseManager.Agent.velocity.magnitude != 0)
        //{
        //    return;
        //}


        //Debug.Log("patrol side");
        //isWaiting = true;


        //int previousIndex = currentIndex;

        //currentIndex += 1;
        //if (currentIndex >= maxIndex)
        //{
        //    currentIndex = 0;
        //}


        //if (!doneOnce)
        //{
        //    BaseManager.TakeNextPatrolPoint(BaseManager.PatrolRoute.PatrolPoints[previousIndex].GetComponent<PatrolPoint>().WaitTime, BaseManager.PatrolRoute.PatrolPoints[currentIndex]);
        //    doneOnce = true;
        //}






        // OPTION ALTERNATIVE: DEPLACER TOUTE LA LOGIQUE DANS LE BASE MANAGER   
        // FAIRE EN SORTE DE SEULEMENT LANCER LA COROUTINE DANSLE ENTER ET LA COUPER DANS LE EXITE
    }





    public override void StartState()
    {

    }

    public override void UpdateState()
    {

    }
}
