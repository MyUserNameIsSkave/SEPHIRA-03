using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllignUiToCamera : MonoBehaviour
{

    Camera mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = GameManager.Instance.mainCamera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = mainCamera.transform.rotation;
    }
}
