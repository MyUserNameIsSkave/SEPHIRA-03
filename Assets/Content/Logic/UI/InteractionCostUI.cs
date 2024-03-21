using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InteractionCostUI : MonoBehaviour
{
    [SerializeField]
    private string interactionName;


    [Space(20)]


    [SerializeField]
    private GameObject anchorPoint;

    [SerializeField]
    private float horizontalOffset;


    [SerializeField]
    private GameObject informationsPrefab;



    private Transform parentPanel;
    private GameObject uiObject;
    private int stamCost;


    private void Awake()
    {
        parentPanel = GameObject.FindGameObjectWithTag("Interaction Informations").transform;
        stamCost = GetCost();
    }







    private void OnMouseEnter()
    {
        uiObject = Instantiate(informationsPrefab, parentPanel);

        UpdatePosition();
    }


    private void OnMouseOver()
    {
        //Utile en cas de zoom
        UpdatePosition();
    }




    private void OnMouseExit()
    {
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



    private void UpdatePosition()
    {
        Vector3 screenPosition = GameManager.Instance.mainCamera.WorldToScreenPoint(anchorPoint.transform.position);
        uiObject.GetComponent<RectTransform>().transform.position = new Vector2(screenPosition.x + horizontalOffset, screenPosition.y);

        //Cost Text
        uiObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = stamCost.ToString();

        //Get child text and set text to cost
        uiObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = interactionName;
    }


}
