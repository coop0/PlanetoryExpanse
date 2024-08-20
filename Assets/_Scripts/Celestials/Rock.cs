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
                if (shot) {
                    if (collision.collider.gameObject.layer == 2) {
                        StartCoroutine(Explode());
                    }
                }
            }
        }
    }
    WaitForSeconds _timpaniPause;

    [ContextMenu("Explode")]
    private IEnumerator Explode()
    {
        if(_timpaniPause == null) _timpaniPause = new WaitForSeconds(0.25f); // Needs to be half the explosion animation length
        _rb.simulated = false;
        _animator.SetTrigger("TrExplosion");
        SoundManager.Instance.PlayRandomTimpani();
        yield return _timpaniPause;
        SoundManager.Instance.PlayRandomTimpani();
        yield return _timpaniPause;
        GameManager.RemoveAttractable(this);
        Destroy(gameObject);
    }



}
