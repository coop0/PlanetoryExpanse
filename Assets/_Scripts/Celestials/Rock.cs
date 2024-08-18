using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Attractable
{
    // NOTE: Static fields are not accessible through Serialization for reasons beyond my understanding
    //private static float _fireMagnitude;
    public override float FireMagnitude
    {
        get
        {
            return 10;
        }
    }
}
