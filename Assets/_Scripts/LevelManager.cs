using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private GameObject currentLevel;  // Reference to the currently active levelvoid Start()

    public void LoadLevel(string levelName) {
        // Find the level GameObject by name
        GameObject levelToLoad = GameObject.Find(levelName);

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