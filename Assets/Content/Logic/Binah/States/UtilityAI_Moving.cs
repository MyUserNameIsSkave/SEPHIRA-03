using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State for the Movements asked by Player
/// </summary>
public class UtilityAI_Moving : UtilityAI_BaseState
{
    public override void EnterState()
    {
        UtilityAI_Manager.Agent.SetDestination(UtilityAI_Manager.IndicatedPosition);
    }

    public override void ExitState()
    {
       
    }




    public override void CustomUdpateState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {
        if (Vector3.Distance(UtilityAI_Manager.Object.transform.position, UtilityAI_Manager.IndicatedPosition) < 1.4f)      //The Float must be 1.4 because of the AI center Heigth
        {
            UtilityAI_Manager.SwitchState(UtilityAI_Manager.IdleState);
        }
    }

}