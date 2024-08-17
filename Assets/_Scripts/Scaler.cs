using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _max;
    [SerializeField] private float _min;
    [SerializeField] private float _start;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _start = rb.mass;
    }

    public void AddMass(float amount)
    {
        SetMass(rb.mass + amount);
    }
    public void SetMass(float amount)
    {
        if (amount > _max) amount = _max;
        if (amount < _min) amount = _min;
        rb.mass = amount;
        UpdateSize();
    }
    private void UpdateSize()
    {
        var percentage = rb.mass / _start;
        transform.localScale = new Vector3(percentage, percentage, 0);
    }
}
