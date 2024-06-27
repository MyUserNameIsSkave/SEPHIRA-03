using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetherDeath : MonoBehaviour, IEventTriggerable
{
    public float vitesseDescente = 0.1f; // Vitesse de descente sur l'axe Z

    public void TriggerEvent()
    {
        // Fait descendre le GameObject sur l'axe Z à chaque frame
        transform.Translate(Vector3.forward * vitesseDescente * Time.deltaTime);
    }
}
