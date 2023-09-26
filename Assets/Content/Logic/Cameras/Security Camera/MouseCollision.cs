using System.Collections;

using UnityEngine;


public class MouseCollision : MonoBehaviour
{
    public PlayerController PlayerControllerRef;
    private CameraController CameraController;

        





    //PLAYER INTERACTION - INPUTS
    private void OnMouseEnter()
    {
        if (PlayerControllerRef == null)
        {
            PlayerControllerRef = GameManager.PlayerControllerRef;
        }
    }

    private void OnMouseDown()
    {
        PlayerControllerRef.ClickOnCamera(gameObject);
    }


}
