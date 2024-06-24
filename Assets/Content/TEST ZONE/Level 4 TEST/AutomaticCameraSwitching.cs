using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCameraSwitching : MonoBehaviour
{
    [SerializeField]
    private CameraBase nextCamera;

    public KeterInteraction keterounet;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Binah") && keterounet.secondPhase)
        {


            GameManager.Instance.CameraController.CurrentCamera = nextCamera;
        }
    }

}
