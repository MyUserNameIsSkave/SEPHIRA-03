using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : Enemy_NeutralState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Idle ");

        //Stop Moving
        BaseManager.MoveAgentTo(BaseManager.transform.position);
    }


    #region Useless
    public override void ExitState()
    {
        Debug.Log("Enemy Exite Idle ");
    }
    #endregion


    public override void FixedUpdateState()
    {

    }


    #region Useless
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
    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");
    }
}
