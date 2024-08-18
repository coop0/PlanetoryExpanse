using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private static Rock rockPrefab;
    [SerializeField] private List<Attractable> _inventory;
    [SerializeField] private List<Transform> nodes;

    private void Awake()
    {
        _inventory = new();
        rockPrefab = LoadCelestial("Rock").GetComponent<Rock>();
        // Load other prefabs


        // Spawn 
    }

    private void UpdateInventory()
    {
        for(int i = 0; i < _inventory.Count && i < nodes.Count; i++)
        {
            _inventory[i].transform.SetPositionAndRotation(nodes[i].transform.position, nodes[i].transform.rotation);
        }
    }

    [ContextMenu("Add Rock")]
    public void AddRock()
    {
        // make a rock
        Rock rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
        print(_inventory.Count + " - > ");
        _inventory.Add(rock);
        print(_inventory.Count);
        rock.transform.parent = transform;
        UpdateInventory();
    }

    [ContextMenu("Try Fire")]
    public void TryFire()
    {
        if(_inventory.Count == 0)
        {
            print("Empty stack");
            return;
        }
        Attractable projectile = _inventory[0];
        _inventory.RemoveAt(0);
        projectile.transform.SetParent(transform.parent);
        GameManager.AddAttractable(projectile);
        Vector2 direction = Vector2.right; // TODO: Update this logic
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.FireMagnitude;
    }

    public static GameObject LoadCelestial(string prefabName)
    {
        var celestial = Resources.Load<GameObject>("Celestials/" + prefabName);
        if (celestial != null)
        {
            return celestial;
        }
        else throw new System.Exception("Invalid LoadCelestial call: " + prefabName);
    }

    public Rock SpawnRock()
    {
        Vector3 spawnerLocation = transform.position;

        Rock rock = Instantiate(rockPrefab, spawnerLocation, Quaternion.identity);
        // Add Rigidbody2D and set velocity
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;

        return rock;
    }

}
