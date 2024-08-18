using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attractable : MonoBehaviour
{
    // See below for Abstract Properties
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-define-abstract-properties
    public abstract float FireMagnitude
    {
        get;
    }
}
