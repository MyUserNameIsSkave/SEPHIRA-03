using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GettingCloser : Enemy_NeutralState
{

    public override void EnterState()
    {
        Debug.Log("Enemy Enter GettingCloser ");

        BaseManager.ChangeAgentSpeed(BaseManager.WalkSpeed);
    }


    #region Useless
    public override void ExitState()
    {
        Debug.Log("Enemy Exite GettingCloser ");
    }
    #endregion


    public override void FixedUpdateState()
    {
        BaseManager.MoveAgentTo(BaseManager.Binah.transform.position);
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


    #region Useless
    public override void DetectedBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Detected Binah");
    }

    public override void LostBinah()
    {
        Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
    #endregion
}
