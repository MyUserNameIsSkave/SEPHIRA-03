using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinahIndicator : MonoBehaviour
{

    private GameObject player;
    private Transform cameraOutlineParent;
    private GameObject binahOutline;
    private BinahOutlineVisualChange currentOutlineScript;





    [SerializeField]
    private float outOfScreenMargin = 0;




    [SerializeField]
    private GameObject cameraOutlinePrefab;


    [SerializeField]
    private LayerMask layerMask;



    private bool isVisible = false;
    private bool inMargin = false;
    private bool inScreen = false;





    private void Awake()
    {
        cameraOutlineParent = GameObject.FindWithTag("CameraOutlineParent").transform;
        
    }


    private void Start()
    {
        player = GameManager.Instance.Player;
    }


    void FixedUpdate()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);



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

                currentOutlineScript.ChangeOutlineSprite(BinahOutlineVisualChange.OutlineSprite.InMargin);
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

                currentOutlineScript.ChangeOutlineSprite(BinahOutlineVisualChange.OutlineSprite.OnScreen);
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






        if (IsBinahVisible() && !inMargin)
        {
            DestroyUI();
            return;
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

        isVisible = true;

        binahOutline = Instantiate(cameraOutlinePrefab, cameraOutlineParent);
        currentOutlineScript = binahOutline.GetComponent<BinahOutlineVisualChange>();


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
        Destroy(binahOutline);
    }


    IEnumerator UpdateUI()
    {
        RectTransform rect = binahOutline.GetComponent<RectTransform>();

        while (true)
        {
            currentOutlineScript.ChangeOutlineSize(GetAdaptedVisibleSize(), GetAdaptedMargineSize());

            Vector3 screenPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(transform.position);
            screenPosition = new Vector2(Mathf.Lerp(Screen.width, 0, screenPosition.x / Screen.width), Mathf.Lerp(Screen.height, 0, screenPosition.y / Screen.height));

            rect.transform.localPosition = new Vector2(-screenPosition.x + Screen.width / 2, -screenPosition.y + Screen.height / 2);

            yield return 0;
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





    private bool IsBinahVisible()
    {

        GameObject objA = GameManager.Instance.Player;
        GameObject objB = gameObject;



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






    [Space(20)]

    [SerializeField]
    private MeshRenderer targeRenderer;




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
