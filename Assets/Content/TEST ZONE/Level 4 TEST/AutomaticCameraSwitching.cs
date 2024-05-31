using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCameraSwitching : MonoBehaviour
{
    [SerializeField]
    private CameraBase nextCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            GameManager.Instance.CameraController.CurrentCamera = nextCamera;
        }
    }

}
