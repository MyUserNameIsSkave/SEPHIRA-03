using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSubtitle : MonoBehaviour
{
    private TextMeshProUGUI subtitleText;

    [HideInInspector]
    public GameObject AnchorPoint;


    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float screenVerticalBorder;

    [SerializeField]
    private float screenHorizontalBorder;



    private void Start()
    {
        subtitleText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateUI());
    }



    public void SetSubtittleTo(string sub)
    {
        subtitleText.text = sub;
    }






    IEnumerator UpdateUI()
    {

        RectTransform rect = subtitleText.GetComponent<RectTransform>();

        while (true)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(AnchorPoint.transform.position);
            screenPosition = new Vector2(Mathf.Clamp(Mathf.Lerp(Screen.width, 0, screenPosition.x / Screen.width), 0 + screenHorizontalBorder, Screen.width - screenHorizontalBorder), Mathf.Clamp(Mathf.Lerp(Screen.height, 0, screenPosition.y / Screen.height), screenVerticalBorder, Screen.height - screenVerticalBorder));
            rect.transform.localPosition = new Vector2(-screenPosition.x + Screen.width / 2, -screenPosition.y + Screen.height / 2);

            yield return 0;
        }
    }






    private void FixedUpdate()
    {
        if (IsCameraVisible())
        {
            ShowText();
        }
        else
        {
            HideText();
        }


        #region

        //{
        //    Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //    if (cameraScript.MaxInteractionDistance != 0)
        //    {
        //        if (cameraScript.DistanceWithPlayer > cameraScript.MaxInteractionDistance)
        //        {
        //            DestroyUI();
        //            return;
        //        }
        //    }





        //    if (!IsCameraInFront())
        //    {
        //        DestroyUI();
        //        return;
        //    }

        //    if (!IsCameraVisible())
        //    {
        //        DestroyUI();
        //        return;
        //    }




        //    if ((screenPosition.x > -outOfScreenMargin && screenPosition.x < 0) || (screenPosition.x > Screen.width && screenPosition.x < Screen.width + outOfScreenMargin) ||
        //        (screenPosition.y > -outOfScreenMargin && screenPosition.y < 0) || (screenPosition.y > Screen.height && screenPosition.y < Screen.height + outOfScreenMargin))
        //    {
        //        // In Margin
        //        if (!isVisible)
        //        {
        //            SpawnUI();
        //        }
        //        if (!inMargin)
        //        {
        //            inMargin = true;
        //            inScreen = false;

        //            if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
        //            {
        //                return;
        //            }

        //            currentOutlineScript.ChangeOutlineSprite(CameraOutlineVisualChange.OutlineSprite.InMargin, cameraScript.alreadyUsed);
        //        }
        //    }
        //    else if ((0 < screenPosition.x && screenPosition.x < Screen.width) &&
        //        (0 < screenPosition.y && screenPosition.y < Screen.height))
        //    {
        //        // In Screen
        //        if (!isVisible)
        //        {
        //            SpawnUI();
        //        }
        //        if (!inScreen)
        //        {
        //            inMargin = false;
        //            inScreen = true;

        //            if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
        //            {
        //                return;
        //            }

        //            currentOutlineScript.ChangeOutlineSprite(CameraOutlineVisualChange.OutlineSprite.OnScreen, cameraScript.alreadyUsed);
        //        }
        //    }
        //    else
        //    {
        //        //Out of Margin and Screen
        //        DestroyUI();
        //    }






        //    if ((-outOfScreenMargin < screenPosition.x && screenPosition.x < Screen.width + outOfScreenMargin) && (-outOfScreenMargin < screenPosition.y && screenPosition.y < Screen.height + outOfScreenMargin))
        //    {
        //        if (!isVisible)
        //        {
        //            if (GetComponent<CameraBase>() == GameManager.Instance.CameraController.CurrentCamera)
        //            {
        //                return;
        //            }


        //            SpawnUI();
        //        }
        //    }
        //    else
        //    {
        //        if (isVisible)
        //        {
        //            DestroyUI();
        //        }
        //    }
        //}
        #endregion
    }






    private void ShowText()
    {
        subtitleText.color = new Color(1f, 1f , 1f, 1f);
    }

    private void HideText()
    {
        subtitleText.color = new Color(1f, 1f, 1f, 0f);
    }





    private bool IsCameraInFront()
    {
        Vector3 viewDirection = Camera.main.transform.forward;
        Vector3 directionToCamera = (AnchorPoint.transform.position - Camera.main.transform.position).normalized;

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
        if (!IsCameraInFront())
        {
            return false;
        }


        Vector2 screenPosition = Camera.main.WorldToScreenPoint(AnchorPoint.transform.position);

        //if ((screenPosition.x > 0 && screenPosition.x < Screen.width) && (screenPosition.y > 0 && screenPosition.y < Screen.height))
        //{
        //    //Inside
        //    ShowText();
        //}
        //else
        //{
        //    //Out
        //    HideText();
        //    return false;

        //}








        GameObject objA = GameManager.Instance.Player;
        GameObject objB = AnchorPoint;



        Vector3 direction = (objB.transform.position - objA.transform.position).normalized; // Calcule la direction du raycast
        float distance = Vector3.Distance(objA.transform.position, objB.transform.position); // Calcule la distance entre les deux objets


        Ray ray = new Ray(objA.transform.position, direction); // Crée un rayon
        RaycastHit hit; // Variable pour stocker les informations de collision

        bool isHit = Physics.Raycast(ray, out hit, distance - 1, ~layerMask);

        //if (isHit)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
        return true;
    }





    public void DestroySubtitle()
    {
        Destroy(gameObject);
    }




}
