using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetherDeath : MonoBehaviour, IEventTriggerable
{
    public float vitesseDescente = 0.1f; // Vitesse de descente sur l'axe Y
    public float tempsOpacite = 2f; // Temps pour r�duire l'opacit� � 0

    public SkinnedMeshRenderer meshRenderer;
    private Color objectColor;
    private float timer;
    public Material matGris; // Ajoutez cette variable pour le mat�riau gris
    private Animator animatorKether;

    void Start()
    {
        animatorKether = GetComponent<Animator>();
        objectColor = meshRenderer.material.color;
    }

    public void TriggerEvent()
    {
        timer = 0f;
        StartCoroutine(ReduireOpaciteEtDescendre());
    }

    IEnumerator ReduireOpaciteEtDescendre()
    {
        while (timer < tempsOpacite)
        {
            timer += Time.deltaTime;
            float pourcentage = 1 - (timer / tempsOpacite);

            // R�duit l'opacit� du mesh
            objectColor.a = pourcentage;
            meshRenderer.material.color = objectColor;

            yield return null;
        }

        // Assure que l'opacit� est � 0 � la fin
        objectColor.a = 0;
        meshRenderer.material.color = objectColor;
        Destroy(animatorKether);

        // Change le mat�riau pour qu'il devienne gris
        meshRenderer.material = matGris;
    }
}
