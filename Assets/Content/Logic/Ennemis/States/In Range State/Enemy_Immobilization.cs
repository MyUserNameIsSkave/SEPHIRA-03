using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy_Immobilization : Enemy_InRangeState
{
    public override void EnterState()
    {
        Debug.Log("Enemy Enter Immobilization ");

        UtilityAI_Manager binahManager = BaseManager.BinahManager;
        binahManager.GetNeutralized();

        BaseManager.MoveAgentTo(BaseManager.transform.position);
        Debug.Log("BINAH MUST BE IMMOBILIZED");
    }

    public override void ExitState()
    {
        Debug.Log("Enemy Exite Immobilization ");
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

    public override void SeenSuspectThing()
    {
        Debug.Log(BaseManager.gameObject.name + " Seen Something");

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
