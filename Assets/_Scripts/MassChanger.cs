using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassChanger : MonoBehaviour
{
    [SerializeField] private float _max;
    [SerializeField] private float _min;
    [field: SerializeField] public float Mass { get; private set; }
    [SerializeField] private Rigidbody2D rb;

    public void MassUp(float amount)
    {

        Mass += amount;
        if (Mass > _max) Mass = _max;
        applyMass();
    }

    public void MassDown(float amount)
    {
        Mass -= amount;
        if(Mass < _min) Mass = _min;
        applyMass();
    }

    private void applyMass()
    {
        rb.mass = Mass;
    }
}
