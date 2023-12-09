using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarnable 
{
    public void GetWarned(Vector3 _warningPosition);

    public void IsWarning();
}
