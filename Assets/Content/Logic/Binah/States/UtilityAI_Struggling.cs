using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class UtilityAI_Struggling : UtilityAI_BaseState
{
    private Quaternion BaseRotation;
    private NavMeshAgent binahNavMeshAgent;



    public override void EnterState()
    {
        BaseRotation = UtilityAI_Manager.transform.rotation;
        binahNavMeshAgent = UtilityAI_Manager.BinahRef.GetComponent<NavMeshAgent>();

        if (GameManager.Instance.StrugglingWith != null)
        {
            // Position
            UtilityAI_Manager.transform.position = GameManager.Instance.StrugglingWith.transform.position;

            // Rotation
            //UtilityAI_Manager.transform.LookAt(GameManager.Instance.StrugglingWith.transform);
            // Désactive le NavMeshAgent
            binahNavMeshAgent.enabled = false;
            binahNavMeshAgent.enabled = true;
            UtilityAI_Manager.transform.rotation = GameManager.Instance.StrugglingWith.transform.rotation;// * Quaternion.Euler(new Vector3(0, 180, 0));
                                                                                                         
        }


        Debug.Log("ENTER STRUGGLING STATE");

        UtilityAI_Manager.animator.SetTrigger("StartStruggling");

        UtilityAI_Manager.CanRecieveInput = false;
        UtilityAI_Manager.Agent.SetDestination(UtilityAI_Manager.Object.transform.position);
    }

    public override void ExitState()
    {

        UtilityAI_Manager.CanRecieveInput = true;
        GameManager.Instance.StrugglingWith = null;
        UtilityAI_Manager.transform.rotation = BaseRotation;
    }

    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {

    }
    
    public override void CustomUdpateState()
    {

    }
}
