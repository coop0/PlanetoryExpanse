using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    public UnityEvent OnClickEvent;
    private void Awake()
    {
        gameObject.tag = "Clickable";
        gameObject.TryGetComponent<Collider2D>(out var temp);
        if (temp == null)
        {
            throw new System.Exception($"ClickHandler attached to {gameObject.name} that lacks a Collider2D");
        }
    }
}
