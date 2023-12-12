using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Warned : Enemy_LostState
{
    public override void EnterState()
    {
        BaseManager.DetectionProgression = 50f;                             //Rend l'unité suspicieuse
        BaseManager.isLosingInterest = false;


        //Change MoveSpeed
        BaseManager.ChangeAgentSpeed(BaseManager.RunSpeed);
        BaseManager.GetComponent<NavMeshAgent>().stoppingDistance = 5f;     //Valeur aléatoire temporaire 

        BaseManager.MoveAgentTo(BaseManager.warningPosition);

    }

    public override void ExitState()
    {
        BaseManager.isLosingInterest = false;
        BaseManager.GetComponent<NavMeshAgent>().stoppingDistance = 0.5f;    //Valeur de base
    }



    public override void FixedUpdateState()
    {
        if (BaseManager.agent.velocity.magnitude == 0)
        {
            if (BaseManager.useSearch)
            {
                BaseManager.SwitchState(BaseManager.SearchState);
            }
            else
            {
                BaseManager.SwitchToNeutralState();
            }
        }
    }



    #region Useless

    public override void UpdateState()
    {
        return;
    }
    #endregion



    public override void HeardSuspectNoise()
    {
        Debug.Log(BaseManager.gameObject.name + " Heard Something");
    }


    public override void SeenSuspectThing()
    {
        Debug.Log(BaseManager.gameObject.name + " Seen Something");


        //Switch State
        if (BaseManager.InvestingatingState != null)
        {
            BaseManager.lastSeenPosition = BaseManager.Binah.transform.position;
            BaseManager.SwitchState(BaseManager.InvestingatingState);
        }
        else
        {
            Debug.LogError("no InvestingatingState set on " + BaseManager.gameObject.name + ", it needs it to work currently");
        }
    }




    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");

        //Switch State
        BaseManager.SwitchState(BaseManager.ChasingState);

    }






    #region Useless
    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");
    }
    #endregion
}
