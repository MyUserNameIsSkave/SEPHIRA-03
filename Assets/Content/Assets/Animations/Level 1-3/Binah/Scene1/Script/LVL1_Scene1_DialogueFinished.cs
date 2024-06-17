using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_DialogueFinished : MonoBehaviour, IEventTriggerable
{

    public GameObject Binah; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanDF; // Le nom de la booléenne à activer


    private Animator animatorB;


    void Start()
    {
        // Récupère l'Animator du GameObject cible
        animatorB = Binah.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la booléenne dans l'Animator
        Debug.Log("La booléenne " + booleanDF + " a été activée.");
        animatorB.SetBool(booleanDF, true);
    }
}
