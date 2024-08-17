using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Star> _stars = new List<Star>();
    [SerializeField] private List<Rock> _rocks = new List<Rock>();

    [SerializeField] private float G; // Gravitational constant
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelManager levelManager;

    public void LoadLevel(GameObject level)
    {
        GetStarsForLevel(level);
        GetRocksForLevel(level);
    }
    private void Start() {
        levelManager.LoadLevel("Level1");
    }
    private void GetStarsForLevel(GameObject level)
    {
        // Get all Star components in the children of the level GameObject
        Star[] starsArray = level.GetComponentsInChildren<Star>(true);

        // Clear the existing list and add the new items
        _stars.Clear();
        _stars.AddRange(starsArray);
    }

    private void GetRocksForLevel(GameObject level)
    {
        // Get all Rock components in the children of the level GameObject
        Rock[] rocksArray = level.GetComponentsInChildren<Rock>(true);

        // Clear the existing list and add the new items
        _rocks.Clear();
        _rocks.AddRange(rocksArray);
    }

    private void Update()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (Star star in _stars)
        {
            foreach (Rock rock in _rocks)
            {
                var a = star.gameObject;
                var b = rock.gameObject;
                float m1 = a.GetComponent<Rigidbody>().mass;
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);

                a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));
            }
        }
    }
}