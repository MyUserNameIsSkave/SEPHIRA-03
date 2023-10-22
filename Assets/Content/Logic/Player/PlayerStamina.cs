using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header ("    SETTINGS")]                                   


    [SerializeField]
    private int maxStam;

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
            _currentStam = Mathf.Clamp(value, 0, maxStam);
            UpdateUI();
        }
    }



    [Header("    REFERENCE")]

    [SerializeField]
    private Image stamBarForground;













    private void Start()
    {
        StartCoroutine(StamRegen());
    }






    private IEnumerator StamRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / Mathf.Clamp(stamRegenRate, 0.01f, Mathf.Infinity));
            CurrentStam += 1;
        }

    }






    private void UpdateUI()
    {
        float imageFill = (float)CurrentStam / (float)maxStam;
        stamBarForground.fillAmount = imageFill;
    }
}
