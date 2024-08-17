using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _stars = new List<GameObject>();
    [SerializeField] private List<GameObject> _rocks = new List<GameObject>();

    [SerializeField] private PlayerController player;


}
