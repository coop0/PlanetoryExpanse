using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float AvailableFuel;
    [SerializeField] private float MaxFuel;
    [Range(0, -1)]
    [SerializeField] private float DrainSpeed;
    [Range(0, 1)]
    [SerializeField] private float FillSpeed;

    private void Awake()
    {
        AvailableFuel = MaxFuel / 2;
    }

    public void Update()
    {
        bool leftMouse = Input.GetKey(KeyCode.Mouse0);
        bool rightMouse = Input.GetKey(KeyCode.Mouse1);
        if (leftMouse ^ rightMouse) // ^ is XOR
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            print("casting");
            if (hit.collider != null)
            {
                print($"hit {hit.collider}");
                if (hit.collider.CompareTag("Celestial"))
                {
                    var celestial = hit.collider.GetComponent<Scaler>();
                    if (leftMouse) UseFuel(FillSpeed * Time.deltaTime, celestial);
                    else UseFuel(DrainSpeed * Time.deltaTime, celestial);
                }
            }
        }
    }
    private void UseFuel(float f, Scaler m)
    {
        if(AvailableFuel - f < 0)
        {
            // No fuel
            return;
        }
        if (AvailableFuel - f > MaxFuel)
        {
            // Too much fuel
            return;
        }
        AvailableFuel -= f; // Opposite to decrease when filling a star.
        m.AddMass(f);
    }
}
