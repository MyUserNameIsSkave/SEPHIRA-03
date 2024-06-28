using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;


public class NavMesh_Updater : MonoBehaviour
{
    // ----- VARIABLES -----


    [SerializeField]
    private NavMeshSurface surface;

    [SerializeField, Range (0.1f, 1f)]
    private float UpdateRate = 0.5f;

    private GameObject binah;


    public bool Enabled = true;

    // ----- LOGIC -----


    #region NavMesh Updating

    private void Start()
    {
        if (!Enabled)
        {
            return;
        }


        binah = GameManager.Instance.Binah;
        StartCoroutine(KeepNavmeshUpdated());
    }

    private void MoveBounds()
    {
        surface.center = binah.transform.position - transform.position;

    }



    private IEnumerator KeepNavmeshUpdated()
    {
        while (true)
        {
            //yield return new WaitForNextFrameUnit();

            MoveBounds();
            surface.UpdateNavMesh(surface.navMeshData);

            yield return new WaitForSeconds(UpdateRate);
        }
    }


    #endregion






    #region Update the Navmesh when back in Inspector

    private void OnValidate()
    {   
        if (surface.isActiveAndEnabled)
        {
            StartCoroutine(UpdateOnClose());
        }
        else
        {
            StopAllCoroutines();
        }
    }


    private IEnumerator UpdateOnClose()
    {
        yield return new WaitForEndOfFrame();

        surface.UpdateNavMesh(surface.navMeshData);
    }

    #endregion
}

