using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attractable : MonoBehaviour
{
    // See below for Abstract Properties
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-define-abstract-properties
    public bool shot = false;
    public abstract float FireMagnitude
    {
        get;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 20)
        {
            GameManager.RemoveAttractable(this);
            Destroy(gameObject);
        }
    }
    public void setShot(bool status) {
        shot = status;
    }
}
