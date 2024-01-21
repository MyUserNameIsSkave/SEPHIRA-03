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
       BaseManager.StartPatrolling();
    }




    public override void ExitState()
    {
        BaseManager.StopPatrolling();
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
