using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_NPCMarchand : MonoBehaviour, IEventTriggerable
{

    public GameObject NPC; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanStart; // Le nom de la bool�enne � activer


    private Animator animatorNPC;


    void Start()
    {
        // R�cup�re l'Animator du GameObject cible
        animatorNPC = NPC.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la bool�enne dans l'Animator
        Debug.Log("La bool�enne " + booleanStart + " a �t� activ�e.");
        animatorNPC.SetBool(booleanStart, true);
    }
}
