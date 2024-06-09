using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Enemy_Struggling : Enemy_AttackingState
{
    
    public override void EnterState()
    {
        GameManager.Instance.StrugglingWith = BaseManager.gameObject;

        BaseManager.animator.SetTrigger("StartStruggling");
        BaseManager.Agent.speed = BaseManager.WalkMoveSpeed;
        BaseManager.MoveAgent(BaseManager.transform.position);
    }



    public override void FixedUpdateState()
    {
        BaseManager.DetectionProgression = 100f;
    }

    public void YakuzaWin()
    {
        //GameManager.Instance.GameOver();
        Transform.FindObjectOfType<GameOverMenuManager>().gameObject.GetComponent<Canvas>().enabled = true;
    }
}
