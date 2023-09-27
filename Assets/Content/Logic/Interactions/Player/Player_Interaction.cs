using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Interaction : MonoBehaviour, IInteractable
{
    public abstract void Interaction();

    public abstract void SelectedByPlayer();
}
