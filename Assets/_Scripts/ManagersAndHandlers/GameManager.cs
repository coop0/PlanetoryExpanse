using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Attractor> _attractors = new List<Attractor>();
    [SerializeField] private static List<Attractable> _attractables = new List<Attractable>();
    [SerializeField] private List<Launcher> _launchers = new List<Launcher>();
    [SerializeField] private float G; // Gravitational constant
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject pauseMenuUI; // Reference to the Pause Menu UI
    [SerializeField] private FuelManager fuelManager;
    [SerializeField] private ScoreHandler scoreHandler;
    private bool isPaused = false; // Track if the game is paused
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        //Time.timeScale = 1f; // Resume the game

    }
    private void ResetGame() {
        _attractables.Clear();
        _attractors.Clear();
        fuelManager.ResetFuel();
        scoreHandler.ResetScore();

    }
    public void LoadLevel(GameObject level)
    {
        ResetGame();
        GetAttractorsForLevel(level);
        GetLaunchersForLevel(level);
    }

    private void GetAttractorsForLevel(GameObject level)
    {
        // Get all Star components in the children of the level GameObject
        Attractor[] attractorArray = level.GetComponentsInChildren<Attractor>(true);

        // Clear the existing list and add the new items
        _attractors.Clear();
        _attractors.AddRange(attractorArray);
    }

    private void GetLaunchersForLevel(GameObject level) {
        Launcher[] launchers = level.GetComponentsInChildren<Launcher>(true);
        _launchers.Clear();
        _launchers.AddRange(launchers);
    }

    private void GetAttractablesForLevel(GameObject level)
    {
        // Get all Rock components in the children of the level GameObject
        Attractable[] attractableArray = level.GetComponentsInChildren<Attractable>(true);

        // Clear the existing list and add the new items
        _attractables.Clear();
        _attractables.AddRange(attractableArray);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) // Press 'Escape' to pause/unpause
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }
    private void FixedUpdate()
    {
        if (!isPaused)
        {
            Gravity();
        }
        bool OutOfObjects = true;
        if (_attractables.Count == 0) {
            foreach(Launcher launcher in _launchers) {
                if (!launcher.IsEmpty()) {
                    OutOfObjects = false;
                }
            }
        }
        else {
            OutOfObjects = false;
        }

        if (OutOfObjects) {
            Time.timeScale = 0f; // Freeze the game
            ScoreHandler.Instance.ShowEndGameUi(true);
        }
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
       public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
        ScoreHandler.Instance.ShowEndGameUi(false);

    }
    
    public void RestartLevel() {
        levelManager.ReloadCurrentLevel();
        ResumeGame();
        ScoreHandler.Instance.ShowEndGameUi(false);
    }

    public void NextLevel() {
        levelManager.LoadNextLevel();
        ResumeGame();
        ScoreHandler.Instance.ShowEndGameUi(false);

    }
}