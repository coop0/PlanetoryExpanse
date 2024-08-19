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

    [SerializeField] private TextMeshProUGUI totalScoreUI;
    [SerializeField] private TextMeshProUGUI fastestHitUI;
    [SerializeField] private TextMeshProUGUI slowestHitUI;
    [SerializeField] private TextMeshProUGUI longestFlightTimeUI;
    [SerializeField] private TextMeshProUGUI bestHitUI;
    [SerializeField] private TextMeshProUGUI fuelUsedUI;

    private List<List<float>> _hitRecord = new List<List<float>>();
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
        _hitRecord.Clear();
        UpdateDisplay();
    }
    public void AddHit(float mass, float velocity, int n) 
    {
        List<float> hit = new List<float>();
        hit.Add(mass);
        hit.Add(velocity);
        hit.Add(n);
        int score = Mathf.RoundToInt(velocity);
        hit.Add(score);
        _hitRecord.Add(hit);
        AddPoints(score);
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
        if (_hitRecord.Count >= 3) {
            successDefeat = "Sucess!";
        }
        else {
            successDefeat = "Failure! :(";
        }
        levelEndUi.SetActive(active);
        PopulateScoreCard();
        successHeading.text = successDefeat;
    }
    public void PopulateScoreCard() {
        float totalFuelUsed = GameManager.Instance.GetTotalFuelUsed();
        float totalScore = _points;
        float fastestHit = 1111; //_hitRecord[0][1];
        float slowestHit = 1111; // _hitRecord[0][1];
        float longestFlight = 1111; //not implented TODO
        float bestHitScore = 1111; //_hitRecord[0][1];

        foreach(List<float> hit in _hitRecord) {
            float mass = hit[0];
            float velocity = hit[1];
            float number = hit[2];
            float score = hit[3];

            if (velocity > fastestHit) {
                fastestHit = velocity;
            }
            if (velocity < slowestHit) {
                slowestHit = velocity;
            } 
            if (bestHitScore < score) {
                bestHitScore = score;
            }
        }
        totalScoreUI.text = totalScore.ToString();
        fastestHitUI.text = fastestHit.ToString();
        slowestHitUI.text = slowestHit.ToString();
        longestFlightTimeUI.text = longestFlight.ToString();
        bestHitUI.text = bestHitScore.ToString();
        fuelUsedUI.text = totalFuelUsed.ToString();
    }
}
