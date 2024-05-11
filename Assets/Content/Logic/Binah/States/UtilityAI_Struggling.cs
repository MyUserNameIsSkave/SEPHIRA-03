using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAI_Struggling : UtilityAI_BaseState
{
    public override void EnterState()
    {
        Debug.Log("ENTER STRUGGLING STATE");

        UtilityAI_Manager.animator.SetTrigger("StartStruggling");

        UtilityAI_Manager.CanRecieveInput = false;
        UtilityAI_Manager.Agent.SetDestination(UtilityAI_Manager.Object.transform.position);
    }

    public override void ExitState()
    {
        UtilityAI_Manager.CanRecieveInput = true;
    }

    public override void FixedUpdateState()
    {
        Debug.Log("SALOPE");
    }

    public override void UpdateState()
    {

    }
    
    public override void CustomUdpateState()
    {

    }
}
