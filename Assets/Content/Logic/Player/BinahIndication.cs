using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class BinahIndication : MonoBehaviour
{

    [Header ("     LAYER SELECTION")]
    [Space (7)]

    public LayerMask WalkableLayer;
    public LayerMask AIInteractionLayer;
    public LayerMask BinahLayer;


    //Working Variables
    [HideInInspector]
    public float maxSlop= 45f;

    [HideInInspector]
    public bool inIndicationMode;

    [HideInInspector]
    public Camera _camera;
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


        // Slope Verifiaction
        Vector3 surfaceNormal = hit.normal;
        float slopeAngle = Vector3.Angle(Vector3.up, surfaceNormal);

        if (slopeAngle > maxSlop)
        {
            return;
        }


        // Move
        UtilityAI_Manager.SendBinahToLocation(hit.point);
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
