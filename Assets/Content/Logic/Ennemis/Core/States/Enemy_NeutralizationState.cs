using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NeutralizationState : Enemy_AttackingState
{
    public override void EnterState()
    {
        BaseManager.Agent.speed = BaseManager.RunMoveSpeed;
    }

    public override void ExitState()
    {

    }





    public override void AwakeState()
    {

    }

    public override void StartState()
    {

    }
    public override void FixedUpdateState()
    {
        if (Vector3.Distance(BaseManager.transform.position, BaseManager.Binah.transform.position) > BaseManager.AttackRange)
        {
            //Neutralize
        }

        BaseManager.MoveAgent(BaseManager.Binah.transform.position);
    }

}
