using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// For communication between Manager level inputs and Object level events, via Colliders
/// </summary>
public class DragHandler : MonoBehaviour
{
    public UnityEvent OnDragEvent;
    public UnityEvent OnReleaseEvent;
    private void Awake()
    {
        gameObject.tag = "Draggable";
        gameObject.TryGetComponent<Collider2D>(out var temp);
        if (temp == null)
        {
            throw new System.Exception("DragHandler attached to GameObject that lacks a Collider2D");
        }
    }
}
