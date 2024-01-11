using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_LostState : Enemy_SearchingState
{
    public override void EnterState()
    {
        BaseManager.MoveAgent(BaseManager.LastKnownPosition);
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





    public override void UpdateState()
    {
        if (BaseManager.Agent.velocity.magnitude != 0)
        {
            return;
        }

        BaseManager.transform.Rotate(Vector3.up * Time.deltaTime * 100f);   //Valeur arbitraire






    }





    public override void FixedUpdateState()
    {

    }



}