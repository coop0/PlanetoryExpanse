using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] public GameObject tutorialPanel;  // The panel that holds the tutorial pop-up
    [SerializeField] public TextMeshProUGUI tutorialText;         // The text field to display the tutorial message
    [SerializeField] public Button nextButton;         // The button to close the pop-up or proceed to the next step
    [SerializeField] public TextMeshProUGUI buttonText; 
    private Queue<string> tutorialMessages;  // Queue to hold tutorial messages

    void Start()
    {
        // Initialize the tutorial messages queue
        tutorialMessages = new Queue<string>();

        // Add your tutorial messages in the order you want to display them
        tutorialMessages.Enqueue("Welcome to the game! This game is all about changing the size of the stars with your fuel to allow our `rocks` to hit the planets!");
        tutorialMessages.Enqueue("You can make a star bigger by hitting left-click on the star. To make a star smaller, use right click.");
        tutorialMessages.Enqueue("Beware, making stars bigger and smaller uses fuel, if you have too much or no fuel you wont be able to perform your action!");
        tutorialMessages.Enqueue("You can rotate the launcher by clicking on the rotation handle!");
        tutorialMessages.Enqueue("Click on a launcher to fire all launchers, note you can still affect the planets while the projectile is en-route");
        tutorialMessages.Enqueue("Your score will be calculated by the velocity on impact (the harder you can hit it), flight time (the longer the better!)");
        tutorialMessages.Enqueue("Now, lets start your first level!");




        // Start the tutorial
        ShowNextMessage();
    }

    public void ShowNextMessage()
    {
        if (tutorialMessages.Count == 0)
        {
            // No more messages, close the tutorial panel
            tutorialPanel.SetActive(false);
            return;
        }

        // Display the next message
        tutorialText.text = tutorialMessages.Dequeue();

        // If this is the last message in the queue, change the button text to "Start"
        if (tutorialMessages.Count == 0)
        {
            buttonText.text = "Start";
        }
        else
        {
            buttonText.text = "Next";
        }

        tutorialPanel.SetActive(true);
    }

    public void OnNextButtonClicked()
    {
        // Show the next message when the button is clicked
        ShowNextMessage();
    }
}