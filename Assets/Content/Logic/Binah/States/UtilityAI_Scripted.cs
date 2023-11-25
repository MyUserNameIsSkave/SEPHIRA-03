using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The AI is Not Responsive to Player Ipputs
/// </summary>
public class UtilityAI_Scripted : UtilityAI_BaseState
{
    public override void EnterState()
    {
        UtilityAI_Manager.CanRecieveInput = false;
        UtilityAI_Manager.Agent.SetDestination(UtilityAI_Manager.Object.transform.position);
    }

    public override void ExitState()
    {
        UtilityAI_Manager.CanRecieveInput = true;
    }




    public override void CustomUdpateState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {

    }

}