using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class CameraIndicator : MonoBehaviour
{

    private GameObject player;
    private Transform cameraOutlineParent;
    private GameObject curentCameraOutline;
    private CameraOutlineVisualChange currentOutlineScript;
    private CameraBase cameraScript;




    [SerializeField]
    private float outOfScreenMargin = 0;




    [SerializeField]
    private GameObject cameraOutlinePrefab;






    [SerializeField]
    private bool isVisible = false;
    private bool inMargin = false;
    private bool inScreen = false;









    private void Awake()
    {
        cameraOutlineParent = GameObject.FindWithTag("CameraOutlineParent").transform;
        cameraScript = GetComponent<CameraBase>();
    }


    private void Start()
    {
        player = GameManager.Instance.Player;
    }


    void FixedUpdate()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);




        IsCameraInFront();
        if (!IsCameraInFront())
        {
            DestroyUI();
            return;
        }

        if ((screenPosition.x > -outOfScreenMargin && screenPosition.x < 0) || (screenPosition.x > Screen.width && screenPosition.x < Screen.width + outOfScreenMargin) || 
            (screenPosition.y > -outOfScreenMargin && screenPosition.y < 0) || (screenPosition.y > Screen.height && screenPosition.y < Screen.height + outOfScreenMargin))
        {
            // In Margin
            if (!isVisible)
            {
                SpawnUI();
            }
            if (!inMargin)
            {
                inMargin = true;
                inScreen = false;

                if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
                {
                    return;
                }

                currentOutlineScript.ChangeOutlineSprite(CameraOutlineVisualChange.OutlineSprite.InMargin, cameraScript.alreadyUsed);
            }
        }
        else if ((0 < screenPosition.x && screenPosition.x < Screen.width) && 
            (0 < screenPosition.y && screenPosition.y < Screen.height))
        {
            // In Screen
            if (!isVisible)
            {
                SpawnUI();
            }
            if (!inScreen)
            {
                inMargin = false;
                inScreen = true;

                if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
                {
                    return;
                }

                currentOutlineScript.ChangeOutlineSprite(CameraOutlineVisualChange.OutlineSprite.OnScreen, cameraScript.alreadyUsed);
            }
        }
        else
        {
            //Out of Margin and Screen
            DestroyUI();
        }






        if ((-outOfScreenMargin < screenPosition.x && screenPosition.x < Screen.width + outOfScreenMargin) && (-outOfScreenMargin < screenPosition.y && screenPosition.y < Screen.height + outOfScreenMargin))
        {
            if (!isVisible)
            {
                if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
                {
                    return;
                }


                SpawnUI();
            }
        }
        else
        {
            if (isVisible)
            {
                DestroyUI();
            }
        }
    }






    public void TransitionnedFrom()
    {
        DestroyUI();
    }




    private void SpawnUI()
    {
        if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
        {
            return;
        }


        isVisible = true;

        curentCameraOutline = Instantiate(cameraOutlinePrefab, cameraOutlineParent);
        currentOutlineScript = curentCameraOutline.GetComponent<CameraOutlineVisualChange>();
        

        StartCoroutine(UpdateUI());
    }


    private void DestroyUI()
    {
        isVisible = false;
        inMargin = false;
        inScreen = false;

        StopAllCoroutines();
        Destroy(curentCameraOutline);
    }


    IEnumerator UpdateUI()
    {
        RectTransform rect = curentCameraOutline.GetComponent<RectTransform>();

        while (true)
        {
            yield return new LateUpdate();

            //if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
            //{
            //    DestroyUI();
            //    yield return null;
            //}


            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition = new Vector2(Mathf.Lerp(Screen.width, 0, screenPosition.x / Screen.width), Mathf.Lerp(Screen.height, 0, screenPosition.y / Screen.height));

            rect.transform.localPosition = new Vector2(-screenPosition.x + Screen.width / 2, -screenPosition.y + Screen.height / 2);

        }
    }






    private bool IsCameraInFront()
    {
        Vector3 cameraDirection = Camera.main.transform.forward;

        Vector3 viewDirection = Camera.main.transform.forward;
        Vector3 directionToCamera = (transform.position - Camera.main.transform.position).normalized;

        float FrontAngle = Vector3.Angle(viewDirection, directionToCamera);
        float BackAngle = Vector3.Angle(-viewDirection, directionToCamera);


        if (FrontAngle < BackAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
