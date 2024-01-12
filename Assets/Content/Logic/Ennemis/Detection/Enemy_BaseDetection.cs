using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BaseDetection : MonoBehaviour
{
    //Base Manager Reference
    protected Enemy_BaseManager enemyManager;




    private void Awake()
    {
        enemyManager = GetComponent<Enemy_BaseManager>();
    }



    public void HeardSomething()
    {
        //enemyManager.currentState.HeardSuspectNoise();

    }

    public void SeenSomething(float detectionIncrement)
    {
        //enemyManager.currentState.SeenSuspectThing(detectionIncrement);
    }

    public void DetectedBinah()
    {
        //enemyManager.currentState.DetectedBinah();

    }

    public void LostBinah()
    {
        //enemyManager.currentState.LostBinah();
    }
}
