using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SecurityCameraEventTrigger_Camera : CameraBase
{


    [Space(40)]
    [Header("  EVENT SETTINGS")]
    [Space(10)]


    [SerializedDictionary("New Camera Script", "Trigger Delay")]
    public SerializedDictionary<CameraBase, float> NewCamera;

    [Space(10)]

    [SerializedDictionary("Event to Trigger", "Trigger Delay")]
    public SerializedDictionary<MonoBehaviour, float> EventToTrigger;




    private void Start()
    {
        if (cameraController.CurrentCamera == this)
        {
            if (this is IInteractable interactionInterface)
            {
                interactionInterface.Interaction();
            }
        }
    }



    protected override void Transitionned()
    {
        CheckForCameraChange();
        CheckForEventsToDo();
    }





    private void CheckForCameraChange()
    {
        foreach (KeyValuePair<CameraBase, float> kvp in NewCamera)
        {
            StartCoroutine(ChangeCameraEvent(kvp));
        }
    }


    IEnumerator ChangeCameraEvent(KeyValuePair<CameraBase, float> kvp)
    {

        yield return new WaitForSeconds(kvp.Value);
        cameraController.CurrentCamera = kvp.Key;
    }





    private void CheckForEventsToDo()
    {
        foreach (KeyValuePair<MonoBehaviour, float> kvp in EventToTrigger)
        {
            StartCoroutine(ExecuteEvents(kvp));
        }
    }


    IEnumerator ExecuteEvents(KeyValuePair<MonoBehaviour, float> kvp)
    {
        IEventTriggerable EvenTriggerInterface = kvp.Key.GetComponent<IEventTriggerable>();
        if (EvenTriggerInterface != null)
        {
            yield return new WaitForSeconds(kvp.Value);
            EvenTriggerInterface.TriggerEvent();
        }

        yield return null;

    }


}
