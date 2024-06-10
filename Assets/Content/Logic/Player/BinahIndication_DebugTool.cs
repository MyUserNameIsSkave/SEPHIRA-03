using UnityEngine;


public class BinahIndication_DebugTool : MonoBehaviour
{
    [SerializeField]
    BinahIndication indicationScript;




    [Space(10)]



    [SerializeField]
    private Color color;

    [SerializeField]
    float sphereScale;

    Camera cameraReference;


    //----------------------------------------------------------------



    private void OnDrawGizmos()
    {
        if (!indicationScript.inIndicationMode)
        {
            return;
        }


        RaycastHit hit;
        Physics.Raycast(indicationScript._camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ~indicationScript.BinahLayer);


        if (MovementIndication(hit) == Vector3.zero)
        {
            return;
        }

        Gizmos.color = color;
        Gizmos.DrawSphere(MovementIndication(hit), sphereScale);

    }





    private Vector3 MovementIndication(RaycastHit hit)
    {
        if (!DoubleCheckRaycast(indicationScript.WalkableLayer))
        {
            return Vector3.zero;
        }


        // Slope Verifiaction
        Vector3 surfaceNormal = hit.normal;
        float slopeAngle = Vector3.Angle(Vector3.up, surfaceNormal);

        if (slopeAngle > indicationScript.maxSlop)
        {
            return Vector3.zero;
        }


        return hit.point;
    }





    /// <summary>
    /// Check if an a Valid Object is directly under the Cursor. Return true if it is the case.
    /// </summary>
    private bool DoubleCheckRaycast(LayerMask targetLayer)
    {
        RaycastHit initialHit;

        if (!Physics.Raycast(indicationScript._camera.ScreenPointToRay(Input.mousePosition), out initialHit, Mathf.Infinity, targetLayer))
        {
            return false;
        }

        RaycastHit verificationHit;
        Physics.Raycast(indicationScript._camera.ScreenPointToRay(Input.mousePosition), out verificationHit, Mathf.Infinity);

        if (initialHit.collider != verificationHit.collider)
        {
            return false;
        }

        return true;
    }

}
