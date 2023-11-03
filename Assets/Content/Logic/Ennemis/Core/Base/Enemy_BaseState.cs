using UnityEngine;
using UnityEngine.UIElements;

public abstract class Enemy_BaseState
{
    [HideInInspector]
    public Enemy_BaseManager BaseManager;




    /// <summary>
    /// On State Enter
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// On State Exite
    /// </summary>
    public abstract void ExitState();



    /// <summary>
    /// On Update
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// On Fixed Update
    /// </summary>
    public abstract void FixedUpdateState();





    public abstract void HeardSuspectNoise();
    public abstract void SeenSuspectThing();
    public abstract void DetectedBinah();
    public abstract void LostBinah();

}
