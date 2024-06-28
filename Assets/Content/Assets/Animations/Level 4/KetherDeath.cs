using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetherDeath : MonoBehaviour, IEventTriggerable
{
    public float vitesseDescente = 0.1f; // Vitesse de descente sur l'axe Y
    public float tempsOpacite = 2f; // Temps pour réduire l'opacité à 0

    public SkinnedMeshRenderer meshRenderer;
    private Color objectColor;
    private float timer;
    public Material matGris; // Ajoutez cette variable pour le matériau gris
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

            // Réduit l'opacité du mesh
            objectColor.a = pourcentage;
            meshRenderer.material.color = objectColor;

            yield return null;
        }

        // Assure que l'opacité est à 0 à la fin
        objectColor.a = 0;
        meshRenderer.material.color = objectColor;
        Destroy(animatorKether);

        // Change le matériau pour qu'il devienne gris
        meshRenderer.material = matGris;
    }
}
