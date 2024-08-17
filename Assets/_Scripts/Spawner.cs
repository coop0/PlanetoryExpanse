using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public Vector2 velocity;
    [SerializeField] public Rock rockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Rock SpawnRock() {
        Vector3 spawnerLocation = transform.position;

        Rock rock = Instantiate(rockPrefab, spawnerLocation, Quaternion.identity);
        // Add Rigidbody2D and set velocity
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();

        rb.velocity = velocity;

        return rock;
    }
}
