using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Utility AI/Consideration", order = 1)]
public class Consideration : ScriptableObject
{
    //Name of the State Variables
    public enum VariableOption
    {
        Courage,
        Determination,
        Curiosity,
        Distance
    }

    //Enum Seleciton
    public VariableOption[] Variables;





    //Enum Copy into Strings
    private string[] VariablesNames;

    //Evaluation Curve
    public AnimationCurve[] Curves;


    //Dictionnary of the variable Names and associated Curves
    public Dictionary<string, AnimationCurve> VariablesSettings = new Dictionary<string, AnimationCurve>();





    public float MinimumScore;
    public float MaximumScore;

    [HideInInspector]
    public Vector2 ScoreRange;





    private void OnValidate()
    {
        //Avoid Error at Launch
        if (Variables == null || VariablesNames == null)
        {
            return;
        }


        // Synchroniser la longueur de Curves[] avec VariablesNames[] seulement si nécessaire
        if (Curves.Length != Variables.Length || VariablesNames.Length != Variables.Length)
        {
            System.Array.Resize(ref VariablesNames, Variables.Length);
            System.Array.Resize(ref Curves, Variables.Length);
        }

        // Stocker l'enum dans des chaînes dans un autre tableau
        for (int i = 0; i < Variables.Length; i++)
        {
            VariablesNames[i] = Variables[i].ToString();
        }

        // Créer un dictionnaire avec le nom de la variable et sa courbe d'évaluation
        VariablesSettings.Clear();
        for (int i = 0; i < VariablesNames.Length; i++)
        {
            VariablesSettings[VariablesNames[i]] = Curves[i];
        }



        MinimumScore = 0;
        MaximumScore = 0;


        foreach (KeyValuePair<string, AnimationCurve> consideration in VariablesSettings)
        {
            if (consideration.Key == "Distance")
            {
                return;
            }


            int totalSample = 100;
            //int currentSample;


            float highestPoint = consideration.Value.Evaluate(0);
            float lowestPoint = consideration.Value.Evaluate(0);




            for (int i = 0; i < totalSample + 1; i++)
            {
                float currentSample = consideration.Value.Evaluate((float)i / totalSample);

                highestPoint = currentSample > highestPoint ? highestPoint : currentSample;
                lowestPoint = currentSample < lowestPoint ? lowestPoint : currentSample;
            }

            MinimumScore += highestPoint;
            MaximumScore += lowestPoint;

            ScoreRange = new Vector2(MinimumScore, MaximumScore);
        }



    }

}
