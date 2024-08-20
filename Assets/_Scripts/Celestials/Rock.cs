using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rock : Attractable
{
    [SerializeField] private float _fireMagnitude;
    private Animator _animator;
    private Rigidbody2D _rb;
    public override float FireMagnitude
    {
        get
        {
            return _fireMagnitude;
        }
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (_animator == null) throw new System.Exception("Rock missing animator.");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider != null)
        {
            if (collision.collider.gameObject.layer == 7) // Celestial layer. I know, I hate it too.
            {
                StartCoroutine(Explode());
            }
            else
            {
                //print("touching " + collision.collider.tag);
            }
        }
    }

    [ContextMenu("Explode")]
    private IEnumerator Explode()
    {
        SoundManager.Instance.PlayRandomTimpani();
        _rb.simulated = false;
        _animator.SetTrigger("TrExplosion");
        yield return new WaitForSeconds(0.5f);
        GameManager.RemoveAttractable(this);
        Destroy(gameObject);
    }

}
