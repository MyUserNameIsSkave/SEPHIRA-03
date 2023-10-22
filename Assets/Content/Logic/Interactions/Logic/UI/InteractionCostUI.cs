using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InteractionCostUI : MonoBehaviour
{
    private GameObject text;
    private int stamCost;



    private void Awake()
    {
        text = GameObject.FindGameObjectWithTag("Stam Cost Text (TEMPORARY)");
        stamCost = GetCost();
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






    private void OnMouseEnter()
    {
        //Activate
        text.SetActive(true);

        // Check Cost and Update Text
        stamCost = GetCost();
        text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(stamCost.ToString());
    }

    private void OnMouseOver()
    {
        text.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    private void OnMouseExit()
    {
        //Deactivate
        text.SetActive(false);
    }
}
