using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;


public class CameraIndicator : MonoBehaviour
{

    [SerializeField]
    private float outOfScreenMargin = 0;


    [SerializeField]
    private LayerMask layerMask;


        [Space(20)]


    [SerializeField]
    private GameObject cameraOutlinePrefab;

    [SerializeField]
    private GameObject indicatorCenter;





    [Space(20)]
    [Header("  VISIBLE ICON SIZE SETTINGS")]
    [Space(7)]

    [SerializeField]
    private float minPercentMargin;

    [SerializeField]
    private float maxPercentMargin;


    [SerializeField]
    private float minObjectScreenSize;

    [SerializeField]
    private float maxObjectScreenSize;

    [Space(20)]
    [Header("  MARGINE ICON SIZE SETTINGS")]
    [Space(7)]

    [SerializeField]
    private float minMarginPercentSize;

    [SerializeField]
    private float maxMarginPercentSize;


    [SerializeField]
    private float minMarginDistance;

    [SerializeField]
    private float maxMarginDistance;








    private GameObject player;
    private Transform cameraOutlineParent;
    private GameObject curentCameraOutline;
    private CameraOutlineVisualChange currentOutlineScript;
    private CameraBase cameraScript;
    private bool isVisible = false;
    private bool inMargin = false;
    private bool inScreen = false;



    [SerializeField]
    private MeshRenderer targeRenderer;




























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
        Vector2 screenPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(indicatorCenter.transform.position);

        if (cameraScript.MaxInteractionDistance != 0)
        {
            if (cameraScript.DistanceWithPlayer > cameraScript.MaxInteractionDistance)
            {
                DestroyUI();
                return;
            }
        }





        if (!IsCameraInFront())
        {
            DestroyUI();
            return;
        }

        if (!IsCameraVisible())
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
        if (currentOutlineScript != null)
        {
            return;
        }


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

        if (currentOutlineScript == null)
        {
            return;
        }
        StopAllCoroutines();
        Destroy(curentCameraOutline);
    }


    IEnumerator UpdateUI()
    {
        RectTransform rect = curentCameraOutline.GetComponent<RectTransform>();

        while (true)
        {
            currentOutlineScript.ChangeOutlineSize(GetAdaptedVisibleSize(), GetAdaptedMargineSize());

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(indicatorCenter.transform.position);
            screenPosition = new Vector2(Mathf.Lerp(Screen.width, 0, screenPosition.x / Screen.width), Mathf.Lerp(Screen.height, 0, screenPosition.y / Screen.height));

            rect.transform.localPosition = new Vector2(-screenPosition.x + Screen.width / 2, -screenPosition.y + Screen.height / 2);

            yield return 0;
        }
    }






    private bool IsCameraInFront()
    {
        Vector3 cameraDirection = Camera.main.transform.forward;

        Vector3 viewDirection = Camera.main.transform.forward;
        Vector3 directionToCamera = (indicatorCenter.transform.position - Camera.main.transform.position).normalized;

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



    private bool IsCameraVisible()
    {

        GameObject objA = GameManager.Instance.Player;
        GameObject objB = indicatorCenter;



        Vector3 direction = (objB.transform.position - objA.transform.position).normalized; // Calcule la direction du raycast
        float distance = Vector3.Distance(objA.transform.position, objB.transform.position); // Calcule la distance entre les deux objets


        Ray ray = new Ray(objA.transform.position, direction); // Crée un rayon
        RaycastHit hit; // Variable pour stocker les informations de collision

        bool isHit = Physics.Raycast(ray, out hit, distance - 1, ~layerMask);

        if (isHit)
        {
            return false;
        }
        else
        {
            return true;
        }
    }






    private Vector2 GetAdaptedVisibleSize()
    {
        Vector2 minScreenPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(targeRenderer.bounds.min);
        Vector2 maxScreenPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(targeRenderer.bounds.max);

        float screenSize = Vector2.Distance(minScreenPosition, maxScreenPosition);
        float minPercent = Screen.width * minPercentMargin / 100;
        float maxPercent = Screen.width * maxPercentMargin / 100;

        float sizeLerp = Mathf.Clamp01(screenSize / maxObjectScreenSize);

        Vector2 size = Vector2.one * Mathf.Lerp(minPercent, maxPercent, sizeLerp);

        return size;
    }





    private Vector2 GetAdaptedMargineSize()
    {
        float minPercent = Screen.width * minMarginPercentSize / 100;
        float maxPercent = Screen.width * maxMarginPercentSize / 100;

        float lerpValue = (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) - minMarginDistance) / (maxMarginDistance - minMarginDistance);

        Vector2 size = Vector2.one * Mathf.Lerp(maxPercent, minPercent, lerpValue);

        return size;
    }




}
