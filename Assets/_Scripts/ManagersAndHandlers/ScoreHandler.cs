using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private static int _points;
    [SerializeField] private GameObject levelEndUi;
    [SerializeField] private TextMeshProUGUI totalScoreUI;
    [SerializeField] private TextMeshProUGUI fastestHitUI;
    [SerializeField] private TextMeshProUGUI slowestHitUI;
    [SerializeField] private TextMeshProUGUI longestFlightTimeUI;
    [SerializeField] private TextMeshProUGUI bestHitUI;
    [SerializeField] private TextMeshProUGUI fuelUsedUI;
    [SerializeField] private TextMeshProUGUI shortestFlightUI;

    [SerializeField] public GameObject scoreTextPrefab;  // Reference to the TextMeshPro prefab
    [SerializeField] public GameObject uiCanvas;
    public float textDuration = 2f; 
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
    }
    public void ResetScore() {
        _points = 0;
        _hitRecord.Clear();
    }
    public void AddHit(float mass, float velocity, int n, float flightTime, Vector2 position) 
    {
        List<float> hit = new List<float>();
        hit.Add(mass);
        hit.Add(velocity);
        hit.Add(n);
        int score = Mathf.RoundToInt(velocity * flightTime);
        hit.Add(score);
        hit.Add(flightTime);
        _hitRecord.Add(hit);
        SpawnScoreText(position, score);
        AddPoints(score);
    }
    public void AddPoints(int points)
    {
        _points += points;
    }
 public void SpawnScoreText(Vector2 worldPosition, int score)
    {
        // Convert world position to screen position
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // Instantiate the score text as a child of the Canvas
        GameObject scoreText = Instantiate(scoreTextPrefab, uiCanvas.transform);

        // Set the position of the text on the Canvas
        scoreText.transform.position = screenPosition;

        // Set the text value
        TextMeshProUGUI textMeshPro = scoreText.GetComponent<TextMeshProUGUI>();
        textMeshPro.text = score.ToString();

        // Destroy the text object after a duration
        Destroy(scoreText, textDuration);
    }
    public void ShowEndGameUi(bool active) {
        levelEndUi.SetActive(active);
        PopulateScoreCard();
    }
    public void PopulateScoreCard() {
        float totalFuelUsed = GameManager.Instance.GetTotalFuelUsed();
        float totalScore = _points;
        if (_hitRecord.Count > 0) {
            float fastestHit = _hitRecord[0][1];
            float slowestHit = _hitRecord[0][1];
            float longestFlight = _hitRecord[0][4];
            float bestHitScore = _hitRecord[0][3];
            float shortestFlight = _hitRecord[0][4];
            foreach(List<float> hit in _hitRecord) {
                float mass = hit[0];
                float velocity = hit[1];
                float number = hit[2];
                float score = hit[3];
                float flightTime = hit[4];

                if (velocity > fastestHit) {
                    fastestHit = velocity;
                }
                if (velocity < slowestHit) {
                    slowestHit = velocity;
                } 
                if (bestHitScore < score) {
                    bestHitScore = score;
                }
                if (longestFlight < flightTime) {
                    longestFlight = flightTime;
                }
                if (shortestFlight > flightTime) {
                    shortestFlight = flightTime;
                }
            }
            totalScoreUI.text = System.Math.Round(totalScore, 2).ToString();
            fastestHitUI.text = System.Math.Round(fastestHit, 2).ToString();
            slowestHitUI.text = System.Math.Round(slowestHit, 2).ToString();
            longestFlightTimeUI.text = System.Math.Round(longestFlight, 2).ToString();
            bestHitUI.text = System.Math.Round(bestHitScore, 2).ToString();
            fuelUsedUI.text = System.Math.Round(totalFuelUsed, 2).ToString();
            shortestFlightUI.text = System.Math.Round(shortestFlight, 2).ToString();
        }
    }
}
