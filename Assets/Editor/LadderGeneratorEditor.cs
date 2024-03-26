using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CustomEditor(typeof(LadderGenerator))]
public class LadderGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LadderGenerator ladderGenerator = (LadderGenerator)target;


        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); // Cela crée un espace flexible qui pousse le bouton au centre de l'écran.
        if (GUILayout.Button("GENERATE", GUILayout.Width(350), GUILayout.Height(50)))
        {
            ladderGenerator.UpdateLadder();
        }
        GUILayout.FlexibleSpace(); // Ajoute un autre espace flexible après le bouton pour le centrer.
        GUILayout.EndHorizontal();

        GUILayout.Space(20);




        EditorGUILayout.LabelField("Structure Settings", EditorStyles.boldLabel);
        ladderGenerator.pieceParent = (Transform)EditorGUILayout.ObjectField("Piece Parent", ladderGenerator.pieceParent, typeof(Transform), true);
        ladderGenerator.ladderPiece = (GameObject)EditorGUILayout.ObjectField("Ladder Piece", ladderGenerator.ladderPiece, typeof(GameObject), true);


        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Height Settings", EditorStyles.boldLabel);
        ladderGenerator.topHandle = (Transform)EditorGUILayout.ObjectField("Top Handle", ladderGenerator.topHandle, typeof(Transform), true);
        ladderGenerator.bottomHandle = (Transform)EditorGUILayout.ObjectField("Bottom Handle", ladderGenerator.bottomHandle, typeof(Transform), true);
    }
}
