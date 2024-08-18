using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Attractable
{
    [SerializeField] private float _fireMagnitude;
    public override float FireMagnitude
    {
        get
        {
            return _fireMagnitude;
        }
    }
}
