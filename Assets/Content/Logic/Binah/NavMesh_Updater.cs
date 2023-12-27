using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;


public class NavMesh_Updater : MonoBehaviour
{
    // ----- VARIABLES -----


    [SerializeField]
    private NavMeshSurface surface;

    [SerializeField, Range (0.1f, 1f)]
    private float UpdateRate = 0.5f;




    // ----- LOGIC -----


    #region NavMesh Updating

    private void Start()
    {
        StartCoroutine(KeepNavmeshUpdated());
    }
   
    
    private IEnumerator KeepNavmeshUpdated()
    {
        surface.UpdateNavMesh(surface.navMeshData);

        yield return new WaitForSeconds(UpdateRate);

        //Loop
        StartCoroutine(KeepNavmeshUpdated());
    }


    #endregion






    #region Update the Navmesh when back in Inspector

    private void OnValidate()
    {   
        if (surface.isActiveAndEnabled)
        {
            StartCoroutine(UpdateOnClose());
        }
    }


    private IEnumerator UpdateOnClose()
    {
        yield return new WaitForEndOfFrame();

        surface.UpdateNavMesh(surface.navMeshData);
    }

    #endregion
}

