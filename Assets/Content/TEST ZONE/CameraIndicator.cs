using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;


public class CameraIndicator : MonoBehaviour
{

    private GameObject player;
    private Transform cameraOutlineParent;
    private GameObject curentCameraOutline;



    [SerializeField]
    private GameObject cameraOutlinePrefab;






    private void Awake()
    {
        cameraOutlineParent = GameObject.FindWithTag("CameraOutlineParent").transform;
    }


    private void Start()
    {
        player = GameManager.Instance.Player;
    }


    //private void Update()
    //{
    //    RectTransform rect = curentCameraOutline.GetComponent<RectTransform>();

    //    Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
    //    screenPosition = new Vector2(Mathf.Lerp(Screen.width, 0, screenPosition.x / Screen.width), Mathf.Lerp(Screen.height, 0, screenPosition.y / Screen.height));

    //    rect.transform.localPosition = new Vector2(-screenPosition.x + Screen.width / 2, -screenPosition.y + Screen.height / 2);

    //}





    //PAS FIABLE
    private void OnBecameVisible()
    {
        if (transform.parent.GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
        {

            return;
        }

        SpawnUI();
    }

    //PAS FIABLE
    private void OnBecameInvisible()
    {
        DestroyUI();
    }




    private void SpawnUI()
    {
        curentCameraOutline = Instantiate(cameraOutlinePrefab, cameraOutlineParent);
        StartCoroutine(UpdateUI());
    }


    private void DestroyUI()
    {

        StopAllCoroutines();
        Destroy(curentCameraOutline);
    }


    IEnumerator UpdateUI()
    {
        RectTransform rect = curentCameraOutline.GetComponent<RectTransform>();

        while (true)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition = new Vector2(Mathf.Lerp(Screen.width, 0, screenPosition.x / Screen.width), Mathf.Lerp(Screen.height, 0, screenPosition.y / Screen.height));

            rect.transform.localPosition = new Vector2(-screenPosition.x + Screen.width / 2, -screenPosition.y + Screen.height / 2);

            yield return new LateUpdate();
        }
    }


}
