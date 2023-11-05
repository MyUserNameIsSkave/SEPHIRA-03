using System.Collections;
using System.Collections.Generic;
using System.Linq;
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



        //if Warning Only
        if (BaseManager.InRangeStates.Contains(BaseManager.WarningState))
        {
            if (BaseManager.InRangeStates.Count == 1)
            {
                BaseManager.SwitchState(BaseManager.WarningState);
            }
        }
    }




    #region Useless
    public override void ExitState()
    {
        Debug.Log("Enemy Exite Chasing ");
    }

    public override void FixedUpdateState()
    {




        //Switch State
        if (Vector3.Distance(BaseManager.transform.position, BaseManager.Binah.transform.position) <= BaseManager.canAttackDistance)
        {
            List<Enemy_InRangeState> inRangeStates = new List<Enemy_InRangeState>(BaseManager.InRangeStates);
            inRangeStates.Remove(BaseManager.WarningState);

            //Switch State
            BaseManager.SwitchState(inRangeStates[Random.Range(0, inRangeStates.Count)]);
        }
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

