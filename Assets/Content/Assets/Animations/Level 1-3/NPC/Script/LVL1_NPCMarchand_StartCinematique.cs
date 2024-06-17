using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_NPCMarchand : MonoBehaviour, IEventTriggerable
{

    public GameObject NPC; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanStart; // Le nom de la booléenne à activer


    private Animator animatorNPC;


    void Start()
    {
        // Récupère l'Animator du GameObject cible
        animatorNPC = NPC.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la booléenne dans l'Animator
        Debug.Log("La booléenne " + booleanStart + " a été activée.");
        animatorNPC.SetBool(booleanStart, true);
    }
}
