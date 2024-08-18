using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FuelManager fuelManager;
    [SerializeField] private GameManager gameManager;
    private bool _leftMouse;
    private bool _rightMouse;
    private bool _leftRising;
    private bool _leftFalling;
    private bool _rightRising;
    private bool _rightFalling;
    private Collider2D _dragging;


    public void Update()
    {
        FindEdges();
        // Falling and held interactions - Shouldn't be checked while something is being dragged.
        if (_dragging == null && (_leftMouse || _rightMouse))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Collider2D hit = Physics2D.Raycast(mousePos, Vector2.zero).collider;
            if (hit != null)
            {
                string tag = hit.tag;
                //print("Hit a " + tag);
                if (tag == "Scaler")
                {
                    var celestial = hit.GetComponent<Scaler>();
                    if (_leftMouse && !_rightMouse) fuelManager.UseFuel(true, Time.deltaTime, celestial);
                    if (_rightMouse && !_leftMouse) fuelManager.UseFuel(false, Time.deltaTime, celestial);
                }
                if(tag == "Draggable")
                {
                    if(_leftMouse) hit.GetComponent<DragHandler>().OnDragEvent?.Invoke();
                    _dragging = hit;
                }
                if(tag == "Clickable")
                {
                    if(_leftFalling) hit.GetComponent<ClickHandler>().OnClickEvent?.Invoke();
                }
            }
        }
        // Rising interactions - use lastHit
        if(_leftRising || _rightRising)
        {
            if (_leftRising)
            {
                if(_dragging != null)
                {
                    if (_dragging.CompareTag("Draggable"))
                    {
                        _dragging.GetComponent<DragHandler>().OnReleaseEvent?.Invoke();
                    }
                    _dragging = null; // mark as released.
                }                
            }
        }

        if (Input.GetKeyDown("w") == true) {
            Debug.Log("key down w");
            gameManager.SpawnRockFromSpawners();
        }
        ClearEdges();
    }

    /// <summary>
    /// Finds the Rising and falling edges of LeftMouse and RightMouse.
    /// </summary>
    private void FindEdges()
    {
        var temp = Input.GetKey(KeyCode.Mouse0);
        // isolate edge frames
        if (temp != _leftMouse)
        {
            _leftMouse = temp;

            if (_leftMouse)
            {
                _leftFalling = true;
            }
            else
            {
                _leftRising = true;
            }
        }
        temp = Input.GetKey(KeyCode.Mouse1);
        // isolate edge frames
        if (temp != _rightMouse)
        {
            _rightMouse = temp;

            if (_rightMouse)
            {
                _rightFalling = true;
            }
            else
            {
                _rightRising = true;
            }
        }
    }
    /// <summary>
    /// Resets the Rising and Falling edge trackers.
    /// </summary>
    private void ClearEdges()
    {
        _leftRising = false;
        _leftFalling = false;
        _rightFalling = false;
        _rightRising = false;
    }
}
