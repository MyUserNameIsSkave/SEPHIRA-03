using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiCamerBars : MonoBehaviour
{
    
    public static UiCamerBars Instance;

    [SerializeField]
    private Slider topSlider, leftSlider, zoomSlider;


    private CameraController cameraController;




    private void Awake()
    {
        Instance = this;
        cameraController = GameManager.Instance.CameraController;
        UpdateUI();
    }



    public void UpdateUI()
    {
        StartCoroutine(updateUI());
    }



    IEnumerator updateUI()
    {
        topSlider.value = (cameraController.CurrentCamera.BaseYaw - cameraController.CurrentCamera.HorizontalRange.x) / (Mathf.Abs(cameraController.CurrentCamera.HorizontalRange.y) + Mathf.Abs(cameraController.CurrentCamera.HorizontalRange.x));
        yield return new WaitForNextFrameUnit();

        leftSlider.value = (cameraController.CurrentCamera.BasePitch + cameraController.CurrentCamera.VerticalRange.y) / (Mathf.Abs(cameraController.CurrentCamera.VerticalRange.y) + Mathf.Abs(cameraController.CurrentCamera.VerticalRange.x));
        yield return new WaitForNextFrameUnit();

        zoomSlider.value = cameraController.CurrentCamera.currentCameraFOV / (cameraController.CurrentCamera.FOVRange.x + cameraController.CurrentCamera.FOVRange.y);
        yield return new WaitForNextFrameUnit();
    }






}
