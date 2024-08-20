using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private WaitForSeconds _harpDelay = new WaitForSeconds(1.5f);

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(DelayedLoad(levelName));
    }

    private IEnumerator DelayedLoad(string levelName)
    {
        // Wait for harp
        yield return _harpDelay;

        // Grab level name from serialized prefab
        PlayerPrefs.SetString("SelectedLevel", "Level" + levelName);

        // Load the main gameplay scene
        SceneManager.LoadScene("MainGameplay");
    }
}