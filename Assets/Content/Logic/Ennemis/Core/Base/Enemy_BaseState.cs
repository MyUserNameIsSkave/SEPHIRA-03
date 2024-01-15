using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// On State Enter
    /// </summary>
    public abstract void AwakeState();

    /// <summary>
    /// On State Exite
    /// </summary>
    public abstract void StartState();

    /// <summary>
    /// On Update
    /// </summary>
    public abstract void UpdateState();

    /// <summary>
    /// On Fixed Update
    /// </summary>
    public abstract void FixedUpdateState();
}
