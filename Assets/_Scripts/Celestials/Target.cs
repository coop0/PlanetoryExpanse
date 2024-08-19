using UnityEngine;

public class Target : MonoBehaviour
{
    private int n;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            print("No rigidbody");
            return;
        }
        else
        {
            ScoreHandler.Instance.AddHit (rb.mass, rb.velocity.magnitude, n);
            n+=1;
        }
    }
}
