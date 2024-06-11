using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class DroneInteractionTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject clicCanvas;

    [SerializeField]
    Canvas movementCanvas;


    private void FixedUpdate()
    {
        if (GetComponent<CameraBase>().alreadyUsed)
        {
            if (clicCanvas != null)
            {
                Destroy(clicCanvas);
                Destroy(this);
            }

            if (movementCanvas != null)
            {
                movementCanvas.enabled = true;
            }
        }
    }

}
