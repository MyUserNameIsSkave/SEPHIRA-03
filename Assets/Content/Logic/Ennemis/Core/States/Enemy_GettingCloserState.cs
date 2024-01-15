using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GettingCloserState : Enemy_NeutralState
{
    public override void EnterState()
    {
        BaseManager.Agent.speed = BaseManager.WalkMoveSpeed;
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
        BaseManager.MoveAgent(BaseManager.Binah.transform.position);
    }

    public override void UpdateState()
    {

    }
}