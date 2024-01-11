using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CurrentStateDebug : MonoBehaviour
{
    private Transform cameraTransform;
    private Enemy_BaseManager enemyManager;
    private TextMeshPro textMeshPro;


    private void Awake()
    {
        enemyManager = transform.parent.parent.GetComponent<Enemy_BaseManager>();
        textMeshPro = transform.GetComponent<TextMeshPro>();

        transform.parent.localScale = new Vector3(1 / transform.parent.lossyScale.x, 1 / transform.parent.lossyScale.y, 1 / transform.parent.lossyScale.z);
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform; // Assurez-vous que votre caméra principale est "taggée" comme "MainCamera"
    }




    private void LateUpdate()
    {
        if (cameraTransform != null)
        {
            transform.LookAt(cameraTransform);
            transform.Rotate(0, 180, 0);
        }

        textMeshPro.text = enemyManager.currentState.ToString();
    }
}
