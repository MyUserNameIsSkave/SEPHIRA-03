using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAnimation : MonoBehaviour
{
    [SerializeField]
    private Mesh[] poses;

    [SerializeField, Range(0.01f, 1f)]
    private float poseDuration;


    MeshFilter meshFilter;


    [SerializeField]
    private bool synchronizedUpdate = true;



    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        StartCoroutine(AnimationLoop());
    }


    IEnumerator AnimationLoop()
    {
        if (!synchronizedUpdate)
        {
            yield return new WaitForSeconds(Random.Range(0f, poseDuration));
        }

        while (true)
        {

            for (int i = Random.Range(0, poses.Length); i < poses.Length; i++)
            {
                meshFilter.mesh = poses[i];
                yield return new WaitForSeconds(poseDuration);
            }


        }
    }
}
