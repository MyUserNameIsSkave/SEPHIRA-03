using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem.iOS;

public class Door : AII_EventTrigger
{
    [SerializeField]
    private Transform coverPosition;

    [SerializeField]
    private Transform outPosition;

    [Space (20)]


    [SerializeField]
    private MeshRenderer _light;

    [HideInInspector]
    public int DisjoncteursOn= 0;



    [SerializeField]
    private Light[] pointLight;



    private float[] baseLightIntensity = new float[2];



    [SerializeField]
    private AnimationCurve lightOscilation;


    private bool doorIsLocked = true;




    [SerializeField]
    private BoxCollider collisionToEnable;



    private void Start()
    {
        for (int i = 0; i < pointLight.Length; i++)
        {
            baseLightIntensity[i] = pointLight[i].intensity;
        }
        StartCoroutine(LightOscilation());

    }


    private bool alreadyInteracted = false;


    public override void InteractionSucceed()
    {
        if (!alreadyInteracted)
        {
            collisionToEnable.enabled = true;


            gameObject.layer = 0;           //Default
            GetComponent<InteractionCostUI>().IsActive = false;
            alreadyInteracted = true;

            GameManager.Instance.BinahManager.Crouching(true);
            GameManager.Instance.BinahManager.SendBinahToLocation(coverPosition.position);

        }
        else if (!doorIsLocked)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GameManager.Instance.BinahManager.SendBinahToLocation(outPosition.position);
        }
    }


    public IEnumerator DisjoncteurActivated()
    {
        DisjoncteursOn += 1;

        yield return new WaitForSeconds(1);

        if (DisjoncteursOn == 3)
        {
            _light.material = new Material(_light.material);
            _light.material.color = Color.green;
            gameObject.layer = 30;          // AI Interactible

            doorIsLocked = false;
            GetComponent<InteractionCostUI>().IsActive = true;
        }

    }

    IEnumerator LightOscilation()
    {
        float v = 0;

        //for (int i = 0; i < pointLight.Length; i++)
        //{
        //    pointLight[i].color = Color.green;
        //}


        while (doorIsLocked)
        {
            v += Time.deltaTime;

            for (int i = 0; i < pointLight.Length; i++)
            {
                pointLight[i].intensity = baseLightIntensity[i] * lightOscilation.Evaluate(v);
            }

            yield return new WaitForNextFrameUnit();
        }

        for (int i = 0; i < pointLight.Length; i++)
        {
            pointLight[i].color = Color.green;
        }
    }



}
