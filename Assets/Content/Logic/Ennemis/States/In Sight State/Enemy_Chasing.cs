using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chasing : Enemy_InSightState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Chasing ");


        //Change MoveSpeed
        BaseManager.ChangeAgentSpeed(BaseManager.RunSpeed);

        BaseManager.isLosingInterest = false;
        BaseManager.DetectionProgression = 100;
    }




    #region Useless
    public override void ExitState()
    {
        Debug.Log("Enemy Exite Chasing ");
    }

    public override void FixedUpdateState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }



    public override void HeardSuspectNoise()
    {
        Debug.Log(BaseManager.gameObject.name + " Heard Something");
    }
    #endregion


    public override void SeenSuspectThing()
    {
        Debug.Log(BaseManager.gameObject.name + " Seen Something");

        //Move agent
        BaseManager.lastSeenPosition = BaseManager.Binah.transform.position;
        BaseManager.MoveAgentTo(BaseManager.lastSeenPosition);
    }


    #region Useless
    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");

    }
    #endregion


    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");

        
        //Search or Neutral State
        if (BaseManager.SearchState != null)
        {
            BaseManager.SwitchState(BaseManager.SearchState);
        }
        else
        {
            BaseManager.SwitchState(BaseManager.NeutralStates[Random.Range(0, BaseManager.NeutralStates.Count)]);
        }
    }

}

