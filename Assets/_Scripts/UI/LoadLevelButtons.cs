using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionUI : MonoBehaviour
{
    public GameObject buttonPrefab; // Assign the LevelButton prefab
    public Transform contentPanel;  // Assign the Content GameObject from Scroll View

    private void Start()
    {
        PopulateLevelButtons();
    }

    private void PopulateLevelButtons()
    {
        // Load all prefabs from the "Levels" folder in Resources
        GameObject[] levels = Resources.LoadAll<GameObject>("Levels");

        foreach (GameObject level in levels)
        {
            // Create a new button for each level
            GameObject newButton = Instantiate(buttonPrefab, contentPanel);
            
            // Set the button text
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = level.name;

            // Add an event listener to the button to load the level
            string levelName = level.name; // Copy to local variable for closure
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelName));
        }
    }

    private void LoadLevel(string levelName)
    {
        // Store the selected level name in PlayerPrefs
        PlayerPrefs.SetString("SelectedLevel", levelName);

        // Load the main gameplay scene
        SceneManager.LoadScene("MainGameplay");
    }
}