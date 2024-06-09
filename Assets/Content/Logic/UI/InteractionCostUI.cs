using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static UnityEngine.UI.GridLayoutGroup;
using System.Runtime.CompilerServices;
using static UnityEngine.PlayerLoop.PreUpdate;

public class InteractionCostUI : MonoBehaviour
{
    [SerializeField]
    private string interactionName;


    [Space(20)]


    [SerializeField]
    private GameObject anchorPoint;

    [SerializeField]
    private float horizontalPercentOffset;


    private float appliedHorizontalOffset;


    [SerializeField]
    private GameObject informationsPrefab;



    private Transform parentPanel;
    private GameObject uiObject;
    private int stamCost;


    [SerializeField]
    private MeshRenderer targetMesh;


    public bool IsActive = true;


    private AI_Interaction aiInteraction;
    private Player_Interaction playerInteraction;





    private void OnValidate()
    {
        appliedHorizontalOffset = (Screen.width * horizontalPercentOffset) / 100;
    }

    private void Awake()
    {
        aiInteraction = GetComponent<AI_Interaction>();
        playerInteraction = GetComponent<Player_Interaction>();


        appliedHorizontalOffset = (Screen.width * horizontalPercentOffset) / 100;

        parentPanel = GameObject.FindGameObjectWithTag("Interaction Informations").transform;
        stamCost = GetCost();
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance.playerInputLocked)
        {
            return;
        }

        if (!IsActive)
        {
            return;
        }

        if (!CheckDistance())
        {
            return;
        }


        UpdateStamPreview(stamCost);




        uiObject = Instantiate(informationsPrefab, parentPanel);

        //Cost Text
        uiObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stamCost.ToString();

        //Get child text and set text to cost
        uiObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = interactionName;



        UpdatePosition();
    }


    private bool CheckDistance()
    {
        if (aiInteraction != null)
        {
            return aiInteraction.CheckDistance();
        }
        else
        {
            return playerInteraction.CheckDistance();
        }
    }



    private void OnMouseOver()
    {
        if (GameManager.Instance.playerInputLocked)
        {
            return;
        }

        if (!IsActive)
        {
            if (uiObject != null)
            {
                Destroy(uiObject);
            }
            return;
        }


        //Utile en cas de zoom

        if (CheckDistance())
        {
            UpdatePosition();

        }
    }




    private void OnMouseExit()
    {
        if (GameManager.Instance.playerInputLocked)
        {
            return;
        }


        if (GameManager.Instance.PlayerStam.staminaUI == null)
        {
            return;
        }


        UpdateStamPreview(0);
        GameManager.Instance.PlayerStam.staminaUI.ValueChanged(false);
        Destroy(uiObject);
    }







    private int GetCost()
    {
        Player_Interaction _PlayerInteractionScript = GetComponent<Player_Interaction>();
        AI_Interaction _AIInteractionScript = GetComponent<AI_Interaction>();

        if (_PlayerInteractionScript)
        {
            return _PlayerInteractionScript.stamCost;
        }
        else if (_AIInteractionScript)
        {
            return _AIInteractionScript.stamCost;
        }

        return 0;
    }


    private void UpdateStamPreview(int newCurrentCost)
    {
        if (GameManager.Instance.PlayerStam.staminaUI == null)
        {
            return;
        }

        GameManager.Instance.PlayerStam.staminaUI.currentCost = newCurrentCost;

        if (newCurrentCost != 0)
        {
            GameManager.Instance.PlayerStam.staminaUI.ValueChanged(false);
        }
    }





    [SerializeField]
    private GameObject[] corners;



    private void UpdatePosition()
    {
        float leftestPoint = -Mathf.Infinity;
        foreach (GameObject corner in corners)
        {
            float currentCornerPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(corner.transform.position).x - Screen.width / 2;

            if (currentCornerPosition > leftestPoint)
            {
                leftestPoint = currentCornerPosition;
            }
        }

        Vector2 screenPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(targetMesh.bounds.center);
        screenPosition = new Vector2(leftestPoint + appliedHorizontalOffset, screenPosition.y - Screen.height / 2);

        uiObject.GetComponent<RectTransform>().transform.localPosition = screenPosition;
    }


    private void UpdateSize()
    {
        // Logique: https://discord.com/channels/1096475787860906074/1096475788301320206/1224048736242634852


        //For 1, Get the Screen Height of the object with Corners Screen Position
        //For 2 Just put a settings in the Inspector
        //See to adjust "appliedHorizontalOffset" with Object Screen Size (Percentage of the On Screen Width of the Object ?
    }


    private void OnDestroy()
    {

        if (GameManager.Instance.PlayerStam.staminaUI == null)
        {
            return;
        }

        UpdateStamPreview(0);
        GameManager.Instance.PlayerStam.staminaUI.ValueChanged(false);
        Destroy(uiObject);
    }



}
