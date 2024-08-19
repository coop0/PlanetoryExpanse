using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private static Rock rockPrefab;
    [SerializeField] private static Missile missilePrefab;
    [SerializeField] private List<Attractable> _inventory;
    [SerializeField] private List<Transform> nodes;
    private Coroutine _shortPause;
    bool _canFire = true;

    private void Awake()
    {
        _inventory = new();
        rockPrefab = LoadCelestial("Rock").GetComponent<Rock>();
        missilePrefab = LoadCelestial("Missile").GetComponent<Missile>();
        // Load other prefabs

        // Spawn inventory
        AddRock();
        AddRock();
        AddMissile();
        AddMissile();
        AddRock();
        AddRock();
        AddRock();       
    }
    private void Start()
    {
        foreach (Attractable item in _inventory)
        {
            GameManager.RemoveAttractable(item);
        }
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
    [ContextMenu("Add Missile")]
    public void AddMissile()
    {
        // make a rock
        Missile missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        _inventory.Add(missile);
        missile.transform.parent = transform;
        UpdateInventory();
    }

    [ContextMenu("Try Fire")]
    public void TryFire()
    {
        if (_inventory.Count == 0 || !_canFire) return;
        _canFire = false;
        // Get next missile
        Attractable projectile = _inventory[0];
        _inventory.RemoveAt(0);

        // Move to worldspace
        projectile.transform.SetParent(transform.parent);
        GameManager.AddAttractable(projectile);

        // Get Rotation for force
        Vector3 angleVector;
        angleVector.x = projectile.transform.position.x - transform.position.x;
        angleVector.y = projectile.transform.position.y - transform.position.y;
        angleVector.z = 0;
        angleVector.Normalize();
        
        // Apply physics
        projectile.GetComponent<Rigidbody2D>().velocity = (new Vector3(angleVector.x, angleVector.y) * projectile.FireMagnitude);
        
        // Load next missile
        StartCoroutine(DelayedUpdateInventory());
    }
    /// <summary>
    /// If you update immediately, the gun will fire two at once. This adds a .5s pause.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayedUpdateInventory()
    {
        yield return new WaitForSeconds(0.5f);
        _canFire = true;
        UpdateInventory();
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
