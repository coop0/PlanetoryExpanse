using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadCampaign()
    {
        // Load the level selection scene
        SceneManager.LoadScene("LevelSelection");
    }

    public void ExitGame()
    {
        // Exit the application
        Application.Quit();
    }
}