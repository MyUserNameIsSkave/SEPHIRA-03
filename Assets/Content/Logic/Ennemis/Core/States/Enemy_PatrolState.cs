using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PatrolState : Enemy_NeutralState
{
    private int currentPatrol = 0;
    private int maxPatrol;




    public override void AwakeState()
    {
        maxPatrol = BaseManager.PatrolPoints.Length;
        Debug.Log(maxPatrol);
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {
      
    }

    public override void FixedUpdateState()
    {
        if (BaseManager.Agent.velocity.magnitude != 0)
        {
            return;
        }

        currentPatrol += 1;
        if (currentPatrol >= maxPatrol)
        {
            currentPatrol = 0;
        }

        Debug.Log(currentPatrol);

        BaseManager.MoveAgent(BaseManager.PatrolPoints[currentPatrol].transform.position);
    }





    public override void StartState()
    {

    }

    public override void UpdateState()
    {

    }
}
