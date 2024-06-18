using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class BinahIndication : MonoBehaviour
{

    [Header ("     LAYER SELECTION")]
    [Space (7)]

    [SerializeField]
    private GameObject positionDecal;

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



    private void Update()
    {
        if (!inIndicationMode)
        {
            positionDecal.transform.position = Vector3.one * 10000;
            return;
        }


        RaycastHit hit;
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ~BinahLayer);


        if (MovementIndicationPosition(hit) == Vector3.zero)
        {
            return;
        }


        positionDecal.transform.position = MovementIndicationPosition(hit);
        positionDecal.transform.rotation = Quaternion.Euler(90, 0, 0);
        print(MovementIndicationPosition(hit));
    }





    private Vector3 MovementIndicationPosition(RaycastHit hit)
    {
        if (!DoubleCheckRaycast(WalkableLayer))
        {
            return Vector3.zero;
        }


        // Slope Verifiaction
        Vector3 surfaceNormal = hit.normal;
        float slopeAngle = Vector3.Angle(Vector3.up, surfaceNormal);

        if (slopeAngle > maxSlop)
        {
            return Vector3.zero;
        }


        return hit.point;
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
        if (GameManager.Instance.playerInputLocked)
        {
            return;
        }


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

        if (GameManager.Instance.playerInputLocked)
        {
            return;
        }


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
