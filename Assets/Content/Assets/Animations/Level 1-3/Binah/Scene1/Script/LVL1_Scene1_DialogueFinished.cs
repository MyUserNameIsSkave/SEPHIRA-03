using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_DialogueFinished : MonoBehaviour, IEventTriggerable
{

    public GameObject Binah; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanDF; // Le nom de la bool�enne � activer


    private Animator animatorB;


    void Start()
    {
        // R�cup�re l'Animator du GameObject cible
        animatorB = Binah.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la bool�enne dans l'Animator
        Debug.Log("La bool�enne " + booleanDF + " a �t� activ�e.");
        animatorB.SetBool(booleanDF, true);
    }
}
