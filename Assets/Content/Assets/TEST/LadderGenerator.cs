using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Rendering;

public class LadderGenerator : MonoBehaviour
{

    [Header("     Structure Settings")]
    [Space(7)]

    public Transform pieceParent;

        [Space(7)]

    public GameObject ladderPiece;
    public NavMeshLink ladderLink;
    public BoxCollider collider;



    [Header ("     Height Settings")]
    [Space (7)]

    public Transform topHandle;
    public Transform bottomHandle;





    // WORKING VARIABLES
    private float pieceSpacing = 0.36f;
    private float baseHeight;
    private float pieceNumber;
    private float ladderLength;
    private List<GameObject> ladderPieces = new List<GameObject>();


    private float topHeight;



    private void Awake()
    {
        ladderLink.startPoint = new Vector3(ladderLink.startPoint.x, 0, ladderLink.startPoint.z);
        ladderLink.endPoint = new Vector3(ladderLink.startPoint.x, topHeight + 0.25f, ladderLink.startPoint.z);

        collider.size = new Vector3(2.5f, topHeight, 2.5f);
        collider.center = new Vector3(0, topHeight / 2, -1);
    }




    private void OnValidate()
    {
        UpdateLadder();
    }




    public void UpdateLadder()
    {
        //Reset
        if (ladderPieces.Count != 0)
        {
            foreach (GameObject ladderPiece in ladderPieces)
            {
                DestroyLadderPiece(ladderPiece);
            }

            ladderPieces.Clear();
        }
        else
        {
            ladderPieces.Clear();
        }







        //Positions
        ladderLength = topHandle.position.y - bottomHandle.position.y;
        pieceNumber = ladderLength / pieceSpacing;


        baseHeight = bottomHandle.position.y - transform.position.y;
        topHeight = topHandle.position.y - transform.position.y;




        //Instantiate
        for (int i = 0; i < pieceNumber; i++)
        {
            Vector3 instanciatePosition = new Vector3(0, baseHeight + (pieceSpacing * i), 0);

            if (i == 0)
            {
                //First Ladder
                ladderPieces.Add(Instantiate(ladderPiece, instanciatePosition, Quaternion.identity * Quaternion.Euler(-90, 0, 0)));
            }
            else
            {
                //Place others
                ladderPieces.Add(Instantiate(ladderPiece, instanciatePosition, Quaternion.identity * Quaternion.Euler(-90, 0, 0)));
            }
        }

        foreach (GameObject ladderPiece in ladderPieces)
        {
            ladderPiece.transform.SetParent(pieceParent, false);
        }

    }





    private void DestroyLadderPiece(GameObject piece)
    {
        UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(piece); };
    }


    void OnApplicationQuit()
    {
        if (ladderPieces.Count != 0)
        {
            foreach (GameObject ladderPiece in ladderPieces)
            {
                DestroyLadderPiece(ladderPiece);
            }

            ladderPieces.Clear();
        }
    }
}
