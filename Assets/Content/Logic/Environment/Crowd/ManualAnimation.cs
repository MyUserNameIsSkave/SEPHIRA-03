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

    [SerializeField]
    private bool visible = false;

    [SerializeField]
    private LayerMask layers;



    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        StartCoroutine(AnimationLoop());
    }


    IEnumerator AnimationLoop()
    {
        yield return 0;

        if (!synchronizedUpdate)
        {
            yield return new WaitForSeconds(Random.Range(0f, poseDuration));
        }

        while (true)
        {

            for (int i = Random.Range(0, poses.Length); i < poses.Length; i++)
            {
                if (visible)
                {
                    meshFilter.mesh = poses[i];
                }

                yield return new WaitForSeconds(poseDuration);
            }
        }
    }


    private void OnBecameVisible()
    {
        RaycastHit hit;


        if (Physics.Raycast(transform.position, (GameManager.Instance.Player.transform.position - transform.position).normalized, out hit, Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) - 1, layers))
        {
            visible = false;
        }
        else
        {
            visible = true;
        }
    }

    private void OnBecameInvisible()
    {
        visible = false;
    }
}
