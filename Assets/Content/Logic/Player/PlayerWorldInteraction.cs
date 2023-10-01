using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldInteraction : MonoBehaviour
{
    //Settings
    [SerializeField]
    private LayerMask interactableLayer;                //WHY THO ?!


    //Working Variables
    private Camera _camera;

    private UtilityAI_Manager UtilityAI_Manager;






    //----------------------------------------------------------------


    private void Awake()
    {
        _camera = Camera.main;
        UtilityAI_Manager = FindAnyObjectByType<UtilityAI_Manager>();
    }




    void Update()
    {

        //Interaction Input
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");

            RaycastHit hit = new RaycastHit();
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.Log("False");
                return;
            }
;
            Debug.Log("True");

            // ----- If Raycast Hit

            #region Interaction



            //Trigger SelectedByPlayer() in the Object. 
            //The Interface MUST be on the colliding object
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                Debug.Log("Clicked on Interaction");
                hit.collider.GetComponent<IInteractable>().SelectedByPlayer();

            }
            #endregion


            #region Move AI
            if (hit.collider.CompareTag("Walkable"))
            {
                //Trigger Movement
                //ai_agent.MoveTo(hit.point);
                UtilityAI_Manager.IndicatedPosition = hit.point;
                UtilityAI_Manager.SwitchState(UtilityAI_Manager.MovingState);

            }
            #endregion
        }
    }
}
