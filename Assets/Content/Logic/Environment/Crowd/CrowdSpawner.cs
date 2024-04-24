using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject npcPrefab;


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
                Instantiate(npcPrefab, currentPosition, Quaternion.identity, parent).transform.localScale = Vector3.one * Random.Range(0.85f, 1.15f);
            }
        }

        spawnNow = false;
    }



}
