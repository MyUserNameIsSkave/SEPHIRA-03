using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;




public class LadderGenerator : MonoBehaviour
{
    [Space(20)]
    [Header("     STRUCTURE SETTINGS")]
    [Space(7)]

    public GameObject ladderPiece;
    public Transform pieceParent;
    public Transform topHandle;
    public Transform bottomHandle;


    private NavMeshLink ladderLink;
    private BoxCollider ladderCollider;

   [Space(7)]






    // WORKING VARIABLES

    public float pieceSpacing = 2f;
    private float baseHeight;
    private float pieceNumber;
    private float ladderLength;
    private List<GameObject> ladderPieces = new List<GameObject>();


    private float topHeight;












private void Awake()
    {
        UpdateLadder();
    }






    public void UpdateLadder()
    {




        ladderLink = GetComponent<NavMeshLink>();
        ladderCollider = GetComponent <BoxCollider>();










        //Reset
        foreach (GameObject ladderPiece in ladderPieces)
        {
            DestroyLadderPiece(ladderPiece);
        }







        //Positions
        ladderLength = topHandle.position.y - bottomHandle.position.y;
        pieceNumber = ladderLength / pieceSpacing;


        baseHeight = bottomHandle.position.y - transform.position.y;
        topHeight = topHandle.position.y - transform.position.y;


        ladderLink.startPoint = new Vector3(ladderLink.startPoint.x, 0, ladderLink.startPoint.z);
        ladderLink.endPoint = new Vector3(ladderLink.startPoint.x, topHeight + 0.25f, ladderLink.startPoint.z);

        ladderCollider.size = new Vector3(2.5f, topHeight, 2.5f);
        ladderCollider.center = new Vector3(0, topHeight / 2, -1);





        //Instantiate
        for (int i = 0; i < pieceNumber; i++)
        {
            if (Application.isPlaying)
            {
                break;
            }



            Vector3 instanciatePosition = new Vector3(0, baseHeight + (pieceSpacing * i), 0);



            if (i == 0)
            {
                //First Ladder
                ladderPieces.Add(Instantiate(ladderPiece, instanciatePosition, Quaternion.identity * Quaternion.Euler(0, 0, 0)));
            }
            else
            {
                //Place others
                ladderPieces.Add(Instantiate(ladderPiece, instanciatePosition, Quaternion.identity * Quaternion.Euler(0, 0, 0)));
            }

            print("iNSTANCIATE");

        }

        foreach (GameObject ladderPiece in ladderPieces)
        {
            if (ladderPiece != null)
            {
                ladderPiece.transform.SetParent(pieceParent, false);
            }
        }

    }





    private void DestroyLadderPiece(GameObject piece)
    {
        //UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(piece); };
        DestroyImmediate(piece);
    }


    //private void OnApplicationQuit()
    //{
    //    if (ladderPieces.Count != 0)
    //    {
    //        foreach (GameObject ladderPiece in ladderPieces)
    //        {
    //            DestroyLadderPiece(ladderPiece);
    //        }

    //        ladderPieces.Clear();
    //    }
    //}
}