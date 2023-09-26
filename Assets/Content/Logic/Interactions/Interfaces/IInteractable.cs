using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Demande a UtilityAI_Action d'executer un action
    /// </summary>
    public void SelectedByPlayer();



    /// <summary>
    /// Contient la logique a executer pour effectuer l'action
    /// </summary>
    public void Interaction();
}
