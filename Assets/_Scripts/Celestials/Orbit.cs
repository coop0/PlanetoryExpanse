using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;  // The object to orbit around
    public float orbitSpeed = 500f;  // Speed of the orbit
    public float orbitRadius = 3000.0f;  // Radius of the orbit
    public float direction = 1f;
    private float angle = 0f;

    private void Update()
    {
        if (target == null) return;

        // Increment the angle over time
        angle += orbitSpeed * Time.deltaTime * direction;

        // Calculate the new position using trigonometry
        float x = Mathf.Cos(angle) * orbitRadius * direction;
        float y = Mathf.Sin(angle) * orbitRadius * direction;

        // Apply the new position to the GameObject
        transform.position = new Vector3(x, y, 0) + target.position;
    }
}