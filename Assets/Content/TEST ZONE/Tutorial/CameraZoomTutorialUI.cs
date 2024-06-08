using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTutorialUI : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (GameManager.Instance.CameraController.currentFOV < 30)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
