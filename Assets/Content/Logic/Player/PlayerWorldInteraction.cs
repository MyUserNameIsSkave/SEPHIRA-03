using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWorldInteraction : MonoBehaviour
{
    [Header("     LAYER SELECTION")]
    [Space(7)]


    [SerializeField]
    private LayerMask PlayerInteractableLayer;

    [SerializeField]
    public LayerMask BinahLayer;




    private Camera _camera;
    private UtilityAI_Manager UtilityAI_Manager;







    //----------------------------------------------------------------


    private void Awake()
    {
        _camera = Camera.main;
        UtilityAI_Manager = FindAnyObjectByType<UtilityAI_Manager>();
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


        Interaction(hit);

    }





    private void Interaction(RaycastHit hit)
    {
        if (!DoubleCheckRaycast(PlayerInteractableLayer))
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
