using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header ("    SETTINGS")]                                   


    public int maxStam;

    [SerializeField, Tooltip ("The amount of Stamina regenerate each second. Must be superior to 0")]
    private float stamRegenRate;


    [SerializeField]
    private int _currentStam;

    [HideInInspector]
    public int CurrentStam
    {
        get
        {
            return _currentStam;
        }
        set
        {
            int temp = Mathf.Clamp(value, 0, maxStam);
            if (temp == CurrentStam)
            {
                return;
            }



            staminaUI.previousPoints = _currentStam;
            _currentStam = temp;



            if (value == maxStam)
            {
                isRegenerating = false;
            }
            else if (!isRegenerating)
            {
                isRegenerating = true;
                StartCoroutine(StamRegen());
            }




            if (value <= maxStam)
                staminaUI.ValueChanged(true);
        }
    }

    [HideInInspector]
    public StaminaUI staminaUI;


    private bool isRegenerating = true;



    private void Awake()
    {
        staminaUI = GameObject.FindGameObjectWithTag("Stamina").GetComponent<StaminaUI>();
    }


    private void Start()
    {
        if (CurrentStam == maxStam)
        {
            isRegenerating = false;
            return;
        }
        StartCoroutine(StamRegen());
    }






    private IEnumerator StamRegen()
    {
        while (isRegenerating)
        {
            yield return new WaitForSeconds(1 / Mathf.Clamp(stamRegenRate, 0.01f, Mathf.Infinity));
            CurrentStam += 1;
        }

    }
}
