using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{




    //Ajouter une orientation cible (Genre Un point a viser)



    [SerializeField]
    private Transform lookAtTarget;


    [SerializeField]
    private GameObject[] npcPrefabs;

    [SerializeField]
    private Material[] npcMaterials;

    [SerializeField]
    private int length, width;

    [SerializeField]
    private float spacing;



    [SerializeField]
    private bool spawnNow = false;


    [SerializeField]
    private float positionNoise;



    private void OnValidate()
    {
        if (!spawnNow)
        {
            return;
        }



        Vector3 basePosition = transform.position;
        Vector3 currentPosition = Vector3.zero;

        Transform parent = Instantiate(new GameObject("Crowd"), transform).GetComponent<Transform>();

        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                currentPosition = new Vector3(basePosition.x + spacing * x, currentPosition.y, basePosition.z + spacing * z);
                currentPosition = new Vector3(currentPosition.x + Random.Range(-positionNoise, positionNoise), currentPosition.y, currentPosition.z + Random.Range(-positionNoise, positionNoise));
                GameObject npc = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)], currentPosition, Quaternion.identity, parent);
                npc.transform.localScale = Vector3.one * Random.Range(0.85f, 1.15f);
                
                if (lookAtTarget != null)
                {
                    npc.transform.LookAt(lookAtTarget);
                }

                //npc.transform.Rotate(new Vector3(-90, 0, 0));
                npc.transform.position = new Vector3 (npc.transform.position.x, npc.transform.position.y + 0.875f, npc.transform.position.z);


                npc.transform.GetChild(0).GetComponent<MeshRenderer>().material = npcMaterials[Random.Range(0, npcMaterials.Length)];

            }
        }

        spawnNow = false;
    }



}
