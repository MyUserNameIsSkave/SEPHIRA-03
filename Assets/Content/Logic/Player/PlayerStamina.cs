using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField]
    private int maxStam;

    [SerializeField]
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
            UpdateUI();
            _currentStam = Mathf.Clamp(value, 0, maxStam);
        }
    }


    private void UpdateUI()
    {
        print("UPDATE UI");
    }
}
