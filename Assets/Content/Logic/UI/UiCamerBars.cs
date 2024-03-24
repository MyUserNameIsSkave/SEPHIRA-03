using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UiCamerBars : MonoBehaviour
{

    public static UiCamerBars Instance;

    [SerializeField]
    private Slider topSlider;

    [SerializeField]
    private Slider leftSlider;

    [SerializeField]
    private Slider zoomSlider;


    [Space(20)]


    [SerializeField]
    private Slider topBase;

    [SerializeField]
    private Slider leftBase;

    [SerializeField]
    private Slider zoomBase;




    private CameraController cameraController;
    private CameraBase currentCamera;




    private void Awake()
    {
        Instance = this;
        cameraController = GameManager.Instance.CameraController;
        UpdateUI();
    }










    public void UpdateUI()
    {
        StartCoroutine(UpdateBarsPositions());
        print("camera switch");

    }



    IEnumerator UpdateBarsPositions()
    {

        if (currentCamera != cameraController.CurrentCamera)
        {
            currentCamera = cameraController.CurrentCamera;
            UpdateBaseIndicator();
        }



        topSlider.value = (currentCamera.BaseYaw - currentCamera.HorizontalRange.x) / (Mathf.Abs(currentCamera.HorizontalRange.y) + Mathf.Abs(currentCamera.HorizontalRange.x));
        leftSlider.value = (currentCamera.BasePitch + currentCamera.VerticalRange.y) / (Mathf.Abs(currentCamera.VerticalRange.y) + Mathf.Abs(currentCamera.VerticalRange.x));
        zoomSlider.value = currentCamera.currentCameraFOV / (currentCamera.FOVRange.x + currentCamera.FOVRange.y);

        yield return null;
    }



    private void UpdateBaseIndicator()
    {

        //Creer une variable dans les camera qui retient la position de base qui lui est attribué et récupérer celle la plutot que la position actuelle
        //pour faire en sorte de baser la positio nde l'indicateurr sur la vrais position de base plus que sur la posiition d'arrivé sur une camera

        topBase.value = (currentCamera.BaseYaw - currentCamera.HorizontalRange.x) / (Mathf.Abs(currentCamera.HorizontalRange.y) + Mathf.Abs(currentCamera.HorizontalRange.x));
        leftBase.value = (currentCamera.BasePitch + currentCamera.VerticalRange.y) / (Mathf.Abs(currentCamera.VerticalRange.y) + Mathf.Abs(currentCamera.VerticalRange.x));
        zoomBase.value = currentCamera.currentCameraFOV / (currentCamera.FOVRange.x + currentCamera.FOVRange.y);
    }
}