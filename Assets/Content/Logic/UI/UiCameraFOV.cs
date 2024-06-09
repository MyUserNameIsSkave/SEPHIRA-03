using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCameraFOV : MonoBehaviour
{

    Camera mainCamera;
    Camera uiCamera;

    private void Start()
    {
        mainCamera = GameManager.Instance.mainCamera;
        uiCamera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Update()
    {
        uiCamera.fieldOfView = mainCamera.fieldOfView;
    }


}
