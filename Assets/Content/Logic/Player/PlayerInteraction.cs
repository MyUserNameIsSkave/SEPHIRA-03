using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //Settings
    [SerializeField]
    private LayerMask interactableLayer;                //WHY THO ?!


    //Working Variables
    private Camera _camera;

    private UtilityAI_Manager UtilityAI_Manager;






    //----------------------------------------------------------------


    private void Start()
    {
        _camera = Camera.main;
        UtilityAI_Manager = FindAnyObjectByType<UtilityAI_Manager>();
    }


    void Update()
    {

        //Interaction Input
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                return;
            }
;


            // ----- If Raycast Hit

            #region Interaction

            //Trigger PlayerIndication() in the Object
            if (hit.collider.GetComponent<IInteractable>() != null)
            {

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
