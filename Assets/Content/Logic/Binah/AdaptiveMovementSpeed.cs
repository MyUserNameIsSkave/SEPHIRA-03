using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

public class AdaptiveMovementSpeed : MonoBehaviour
{



    private UtilityAI_Manager binahManager;
    private NavMeshAgent agent;


    [SerializeField]
    private float outOfScreenMargin;

    private bool isOutOfScreen;


    //binahManager.speedMultiplier = binahManager.outOfScreenSpeedMultiplier;



    // Start is called before the first frame update
    void Awake()
    {
        binahManager = GameManager.Instance.BinahManager;
        agent = GameManager.Instance.Binah.GetComponent<NavMeshAgent>();
    }





    private void FixedUpdate()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);


        if ((screenPosition.x < -outOfScreenMargin || screenPosition.x > Screen.width + outOfScreenMargin) ||
            (screenPosition.y < -outOfScreenMargin || screenPosition.y > Screen.height + outOfScreenMargin))
        {
            binahManager.speedMultiplier = binahManager.outOfScreenSpeedMultiplier;
            isOutOfScreen = true;
        }
        else if (isOutOfScreen)
        {
            binahManager.speedMultiplier = 1;
        }



    }

}
