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

    [SerializeField] public Camera mainCamera;
    [SerializeField] public Rock rockPrefab;

    [SerializeField] private List<Spawner> _spawners = new List<Spawner>();

    public void LoadLevel(GameObject level)
    {
        GetStarsForLevel(level);
        GetRocksForLevel(level);
        GetSpawnersForLevel(level);
    }
    private void Start() {
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

    private void GetSpawnersForLevel(GameObject level){
        Spawner[] spawnersArray = level.GetComponentsInChildren<Spawner>(true);

        _spawners.Clear();
        _spawners.AddRange(spawnersArray);
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
                float m1 = a.GetComponent<Rigidbody2D>().mass;
                float m2 = b.GetComponent<Rigidbody2D>().mass;
                float r = Vector2.Distance(a.transform.position, b.transform.position);
                var dir = (b.transform.position - a.transform.position).normalized;
                var force = dir * (G * (m1 * m2) / (r * r));
                // print($"star mass: {m1}, rock mass: {m2}. G: {G}, r: {r}, dir: {dir}, force: {force}");
                b.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

    //removing, bring back if later wanted
    // public void SpawnRockAtCameraEdge()
    // {
    //     if (mainCamera == null || rockPrefab == null)
    //     {
    //         Debug.LogError("Camera or Prefab not assigned.");
    //         return;
    //     }

    //     // Calculate the position at the left edge of the camera view
    //     Vector3 leftEdgePosition = GetCameraEdgePosition(mainCamera, -0.5f);

    //     Rock rock = Instantiate(rockPrefab, leftEdgePosition, Quaternion.identity);
    //     _rocks.Add(rock);
    //     // Add Rigidbody2D and set velocity
    //     Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();

    //     rb.velocity = new Vector2(3f, 0f);
    // }

    // private Vector3 GetCameraEdgePosition(Camera camera, float xViewPortPosition)
    // {
    //     // Calculate the viewport position with a Z value large enough to ensure the object is in front of the camera
    //     Vector3 viewportPosition = new Vector3(xViewPortPosition, 0.5f, Mathf.Abs(camera.transform.position.z - camera.nearClipPlane) + 1f);
    //     Vector3 worldPosition = camera.ViewportToWorldPoint(viewportPosition)/2;
    //     return worldPosition;
    // }

    public void SpawnRockFromSpawners() {
        foreach(Spawner spawner in _spawners) {
            Rock rock = spawner.SpawnRock();
            _rocks.Add(rock);

        }
    }
}