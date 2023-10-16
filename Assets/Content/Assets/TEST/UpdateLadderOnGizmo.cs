using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLadderOnGizmo : MonoBehaviour
{
    [SerializeField]
    private LadderGenerator ladderGenerator;


    private Vector3 pastPosition;

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            return;
        }


        if (transform.position != pastPosition)
        {
            ladderGenerator.UpdateLadder();
            pastPosition = transform.position;
        }
    }


    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

}
