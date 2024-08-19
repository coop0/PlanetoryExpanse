using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Attractor> _attractors = new List<Attractor>();
    [SerializeField] private static List<Attractable> _attractables = new List<Attractable>();

    [SerializeField] private float G; // Gravitational constant
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelManager levelManager;

    [SerializeField] public Camera mainCamera;
    [SerializeField] public Rock rockPrefab;

    [SerializeField] private List<Spawner> _spawners = new List<Spawner>();

    public GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public void LoadLevel(GameObject level)
    {
        GetAttractorsForLevel(level);
        GetAttractablesForLevel(level);
        GetSpawnersForLevel(level);
    }
    private void GetAttractorsForLevel(GameObject level)
    {
        // Get all Star components in the children of the level GameObject
        Attractor[] attractorArray = level.GetComponentsInChildren<Attractor>(true);

        // Clear the existing list and add the new items
        _attractors.Clear();
        _attractors.AddRange(attractorArray);
    }

    private void GetAttractablesForLevel(GameObject level)
    {
        // Get all Rock components in the children of the level GameObject
        Attractable[] attractableArray = level.GetComponentsInChildren<Attractable>(true);

        // Clear the existing list and add the new items
        _attractables.Clear();
        _attractables.AddRange(attractableArray);
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
        foreach (Attractor attractor in _attractors)
        {
            foreach (Attractable attractable in _attractables)
            {
                var a = attractor.gameObject;
                var b = attractable.gameObject;
                float m1 = a.GetComponent<Rigidbody2D>().mass;
                float m2 = b.GetComponent<Rigidbody2D>().mass;
                float r = Vector2.Distance(a.transform.position, b.transform.position);
                var dir = (b.transform.position - a.transform.position).normalized;
                var force = dir * (G * (m1 * m2) / (r * r));
                var rb = b.GetComponent<Rigidbody2D>();
                rb.AddForce(force);
                //Rotation
                //var velDir = rb.velocity.normalized;
                //var angleDir = Mathf.Atan2(velDir.y, velDir.x) * Mathf.Rad2Deg;
            }
        }
    }
    public static void RemoveAttractable(Attractable attractable)
    {
        if (_attractables.Contains(attractable))
        {
            _attractables.Remove(attractable);
        }
        else
        {
            print("Not in List.");
        }
    }
    public static void AddAttractable(Attractable attractable)
    {
        if (attractable == null)
        {
            print("Tried to add null to _attractables list");
            return;
        }
        _attractables.Add(attractable);
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

    //     Rock attractable = Instantiate(rockPrefab, leftEdgePosition, Quaternion.identity);
    //     _attractables.Add(attractable);
    //     // Add Rigidbody2D and set velocity
    //     Rigidbody2D rb = attractable.GetComponent<Rigidbody2D>();

    //     rb.velocity = new Vector2(3f, 0f);
    // }

    // private Vector3 GetCameraEdgePosition(Camera camera, float xViewPortPosition)
    // {
    //     // Calculate the viewport position with a Z value large enough to ensure the object is in front of the camera
    //     Vector3 viewportPosition = new Vector3(xViewPortPosition, 0.5f, Mathf.Abs(camera.transform.position.z - camera.nearClipPlane) + 1f);
    //     Vector3 worldPosition = camera.ViewportToWorldPoint(viewportPosition)/2;
    //     return worldPosition;
    // }

    // TODO: Move this to Spawner.
    public void SpawnRockFromSpawners() {
        foreach(Spawner spawner in _spawners) {
            Rock rock = spawner.SpawnRock();
            _attractables.Add(rock);
        }
    }
}