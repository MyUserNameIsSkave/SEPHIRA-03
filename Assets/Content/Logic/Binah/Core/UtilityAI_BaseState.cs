using UnityEngine;

public abstract class UtilityAI_BaseState
{
    [HideInInspector]
    public UtilityAI_Manager UtilityAI_Manager;



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

    /// <summary>
    /// On Custom Time Update
    /// </summary>
    public abstract void CustomUdpateState();
}

