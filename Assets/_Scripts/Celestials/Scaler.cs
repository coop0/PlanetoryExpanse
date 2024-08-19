using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _max;
    [SerializeField] private float _min;
    [SerializeField] private float _start;
    [SerializeField] private float _sizeConstant;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        SetMass(_start);
    }

    public bool AddMass(float amount)
    {
        if (amount > 0) {
            if (rb.mass >= _max) {

                return false;
            }
        }
        else {
            if (rb.mass <= _min) {
                return false;
            }
        } 
        SetMass(rb.mass + amount);
        return true;
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
        var percentage = rb.mass / _max;
        var sizing = percentage * _sizeConstant;
        SoundManager.Instance.PlayPercentage(1 - sizing);
        transform.localScale = new Vector3(sizing, sizing, 0);
    }

}
