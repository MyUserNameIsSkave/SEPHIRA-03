using UnityEngine;
using UnityEngine.InputSystem;

public class BinahIndication : MonoBehaviour
{


    public bool inIndicationMode;


    public LayerMask WalkableLayer;
    public LayerMask AIInteractionLayer;
    public LayerMask BinahLayer;

    //Working Variables
    private Camera _camera;
    private UtilityAI_Manager UtilityAI_Manager;





    //----------------------------------------------------------------


    private void Awake()
    {
        _camera = Camera.main;
        UtilityAI_Manager = FindAnyObjectByType<UtilityAI_Manager>();
    }







    private void OnMovementIndication(InputValue value)
    {
        if (value.Get<float>() == 0)
        {
            inIndicationMode = false;
        }
        else
        {
            inIndicationMode = true;
        }
    }


    private void OnInteraction(InputValue value)
    {
        //Only on Press
        if (value.Get<float>() == 0)
        {
            return;
        }



        RaycastHit hit;
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ~BinahLayer);



        if (!inIndicationMode)
        {
            ActionIndication(hit);
            return;
        }
        else
        {
            MovementIndication(hit);
            return;
        }

    }





    private void ActionIndication(RaycastHit hit)
    {
        if (!DoubleCheckRaycast(AIInteractionLayer))
        {
            return;
        }


        IInteractable interactionInterface = hit.collider.GetComponent<IInteractable>();

        if (interactionInterface != null)
        {
            interactionInterface.SelectedByPlayer();
            return;
        }
    }



    private void MovementIndication(RaycastHit hit)
    {
        if (!DoubleCheckRaycast(WalkableLayer))
        {
            return;
        }


        //MUST add slop verification

        UtilityAI_Manager.IndicatedPosition = hit.point;
        UtilityAI_Manager.SwitchState(UtilityAI_Manager.MovingState);
    }












    /// <summary>
    /// Check if an a Valid Object is directly under the Cursor. Return true if it is the case.
    /// </summary>
    private bool DoubleCheckRaycast(LayerMask targetLayer)
    {
        RaycastHit initialHit;

        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out initialHit, Mathf.Infinity, targetLayer))
        {
            return false;
        }

        RaycastHit verificationHit;
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out verificationHit, Mathf.Infinity);

        if (initialHit.collider != verificationHit.collider)
        {
            return false;
        }

        return true;
    }

}
