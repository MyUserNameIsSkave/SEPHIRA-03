using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level1IntroManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ZoomTutorialCanvas;


    private void Awake()
    {
        GameManager.Instance.playerInputLocked = true;

        if (GameManager.CurrentIndex != -1)
        {
            Destroy(gameObject);
        }

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
