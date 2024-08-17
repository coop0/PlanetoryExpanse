using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Star> _stars = new List<Star>();
    [SerializeField] private List<Rock> _rocks = new List<Rock>();

    [SerializeField] private PlayerController player;

    public void LoadLevel(GameObject level)
    {
        GetStarsForLevel(level);
        GetRocksForLevel(level);
    }

    private void GetStarsForLevel(GameObject level)
    {
        // Get all Star components in the children of the level GameObject
        Star[] starsArray = level.GetComponentsInChildren<Star>(true);

        // Clear the existing list and add the new items
        _stars.Clear();
        _stars.AddRange(starsArray);
    }

    private void GetRocksForLevel(GameObject level)
    {
        // Get all Rock components in the children of the level GameObject
        Rock[] rocksArray = level.GetComponentsInChildren<Rock>(true);

        // Clear the existing list and add the new items
        _rocks.Clear();
        _rocks.AddRange(rocksArray);
    }
}