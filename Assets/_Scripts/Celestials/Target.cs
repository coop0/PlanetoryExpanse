using UnityEngine;

public class Target : MonoBehaviour
{
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
            ScoreHandler.Instance.AddPoints(Mathf.RoundToInt(rb.velocity.magnitude));
        }
    }
}
