using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private static int _points;
    [SerializeField] private TextMeshProUGUI _display;
    [SerializeField] private GameObject levelEndUi;
    [SerializeField] private TextMeshProUGUI successHeading;
    private List<List<float>> _massVelocity = new List<List<float>>();
    public static ScoreHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    private void Start()
    {
        _points = 0;
        UpdateDisplay();
    }
    public void ResetScore() {
        _points = 0;
        _massVelocity.Clear();
        UpdateDisplay();
    }
    public void AddHit(float mass, float velocity, int n) 
    {
        List<float> hit = new List<float>();
        hit.Add(mass);
        hit.Add(velocity);
        hit.Add(n);
        _massVelocity.Add(hit);
        AddPoints(Mathf.RoundToInt(velocity));
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

    public void ShowEndGameUi(bool active) {
        string successDefeat = "";
        if (_massVelocity.Count >= 3) {
            successDefeat = "Sucess!";
        }
        else {
            successDefeat = "Failure! :(";
        }
        levelEndUi.SetActive(active);
        successHeading.text = successDefeat;
    }
}
