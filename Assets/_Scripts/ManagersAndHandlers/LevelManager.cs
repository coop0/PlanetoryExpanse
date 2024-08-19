using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject currentLevel;
    private string currentLevelName;
    public static LevelManager Instance { get; private set; }
    public void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    private void Start()
    {
        // Retrieve the level name from PlayerPrefs or another source
        string levelName = PlayerPrefs.GetString("SelectedLevel", null);

        if (!string.IsNullOrEmpty(levelName))
        {
            LoadLevel(levelName);
        }
    }

    public void LoadLevel(string levelName)
    {
        currentLevelName = levelName;
        // Load the level prefab from Resources
        GameObject levelPrefab = Resources.Load<GameObject>("Levels/" + levelName);

        if (levelPrefab != null)
        {
            if (currentLevel != null)
            {
                Destroy(currentLevel); // Destroy the previous level to free resources
            }

            currentLevel = Instantiate(levelPrefab);
            currentLevel.SetActive(true);

            // Notify game manager
            GameManager.Instance.LoadLevel(currentLevel);
        }
        else
        {
            Debug.LogWarning("Level prefab not found: " + levelName);
        }
    }

    public void ReloadCurrentLevel() {
        if (currentLevel != null)
        {
            LoadLevel(currentLevelName);
        }
        else
        {
            Debug.LogWarning("No current level to reload.");
        }
    }
}