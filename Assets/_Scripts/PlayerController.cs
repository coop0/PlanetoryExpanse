using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FuelManager fuelManager;

    public void Update()
    {
        bool leftMouse = Input.GetKey(KeyCode.Mouse0);
        bool rightMouse = Input.GetKey(KeyCode.Mouse1);
        if (leftMouse ^ rightMouse) // ^ is XOR
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            //print("casting");
            if (hit.collider != null)
            {
                //print($"hit {hit.collider}");
                if (hit.collider.CompareTag("Celestial"))
                {
                    var celestial = hit.collider.GetComponent<Scaler>();
                    if (leftMouse) fuelManager.UseFuel(true, Time.deltaTime, celestial);
                    else fuelManager.UseFuel(false, Time.deltaTime, celestial);
                }
            }
        }
    }

}
