using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level1IntroManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ZoomTutorialCanvas;


    private void Start()
    {
        GameManager.Instance.playerInputLocked = true;
    }


    private void Update()
    {
        if (GameManager.Instance.CameraController.CurrentCamera.BaseYaw > 70)
        {
            ZoomTutorialCanvas.SetActive(true);
            Destroy(gameObject);
        }
    }
}
