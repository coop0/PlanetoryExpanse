using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private GameObject currentLevel;  // Reference to the currently active levelvoid Start()
    private Dictionary<string, GameObject> levelDict = new Dictionary<string, GameObject>();
    public void Awake() {
        LoadAllLevels();
    }
     private void LoadAllLevels() {
        // Load all prefabs from the "Levels" folder
        GameObject[] levels = Resources.LoadAll<GameObject>("Levels");
        foreach (GameObject level in levels)
        {
            if (!levelDict.ContainsKey(level.name))
            {
                GameObject levelInstance = Instantiate(level);
                levelInstance.SetActive(false); // Ensure levels are inactive initially
                levelDict.Add(level.name, levelInstance);
            }
        }
    }
    public void LoadLevel(string levelName) {
        // Find the level GameObject by name
        levelDict.TryGetValue(levelName, out GameObject levelToLoad);

        if (levelToLoad != null)
        {
            // Disable the current level if there is oneif (currentLevel != null)
            {
                if (currentLevel) {
                    currentLevel.SetActive(false);
                }
            }

            // Enable the new level
            levelToLoad.SetActive(true);
            currentLevel = levelToLoad;
            gameManager.LoadLevel(levelToLoad);
        }
        else
        {
            Debug.LogWarning("Level not found: " + levelName);
        }
    }

    public void ReloadCurrentLevel() {
        if (currentLevel != null)
        {
            currentLevel.SetActive(false);
            currentLevel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No current level to reload.");
        }
    }
}