using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private static int _points;
    [SerializeField] private TextMeshProUGUI _display;

    public static ScoreHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
        _points = 0;
        UpdateDisplay();
    }
    public void AddPoints(int points)
    {
        _points += points;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        _display.text = "Score: " + _points.ToString();
    }
}
