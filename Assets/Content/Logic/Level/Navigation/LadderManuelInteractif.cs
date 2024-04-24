using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LadderManuelInteractif : MonoBehaviour
{
    [SerializeField]
    private GameObject topPoint;

    [SerializeField]
    private GameObject bottomPoint;



    private void OnMouseDown()
    {
        bool isBinahOver;

        if (Input.GetMouseButtonDown(0))
        {

            if (GameManager.Instance.Binah.transform.position.y > transform.position.y)
            {
                isBinahOver = true;
            }
            else
            {
                isBinahOver = false;
            }




            if (isBinahOver)
            {
                GameManager.Instance.BinahManager.SendBinahToLocation(bottomPoint.transform.position);
            }
            else
            {
                GameManager.Instance.BinahManager.SendBinahToLocation(topPoint.transform.position);
            }
        }
    }
}
