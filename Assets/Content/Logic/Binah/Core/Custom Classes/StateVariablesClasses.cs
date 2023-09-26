using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.TestTools;




/// <summary>
/// Class to use in order to store the State Variables
/// </summary>
/// 
[System.Serializable]
public class StateVariable
{
    public string Name;

    [Range(0f, 100f)] 
    public float Value;
}



/// <summary>
/// A Custom Dictionnary for the StateVariables. Use SetStateVariables() or AddStateVariables() to Modify Values in Script.
/// </summary>
[System.Serializable]
public class StateVariablesDictionnary
{
    public StateVariable[] StateVariables = new StateVariable[]
        {
            new StateVariable { Name = "Courage", Value = 0f },
            new StateVariable { Name = "Determination", Value = 25f },
            new StateVariable { Name = "Curiosity", Value = 0f },
            new StateVariable { Name = "Independence", Value = 0f },
            // ... add more entries as needed
        };



    /// <summary>
    /// Methode to call in order to set new StateVariables Values
    /// </summary>
    public void SetStateVariables(float Courage, float Determination, float Curiosity, float Independence)
    {
        float[] newVariables = new float[] { Courage, Determination, Curiosity, Independence };

        //Set Each Values
        for (int i = 0; i < newVariables.Length; i++)
        {
            StateVariables[i].Value = Mathf.Clamp(StateVariables[i].Value + newVariables[i], 0f, 100f);
        }

    }

    /// <summary>
    /// Methode to call in order to modify StateVariables Values with Additions
    /// </summary>
    public void AddStateVariables(float Courage, float Determination, float Curiosity, float Independence)
    {
        float[] addToVariables = new float[] { Courage, Determination, Curiosity, Independence };

        //Add to Each Values
        for (int i = 0; i < addToVariables.Length; i++)
        {
            StateVariables[i].Value = Mathf.Clamp(StateVariables[i].Value + addToVariables[i], 0f, 100f);
        }
    }
}




/// <summary>
/// Class to use in order to store Modification to do on the State Variables
/// </summary>

[System.Serializable]
public class StateVariablesModification
{
    [Range(-100f, 100f)]
    public float Courage;

    [Range(-100f, 100f)]
    public float Determination;

    [Range(-100f, 100f)]
    public float Curiosity;

    [Range(-100f, 100f)]
    public float Independence;
}
