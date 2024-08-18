using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private Image resourceBarFill; // Reference to the Image component used for the fill

    [SerializeField] private float AvailableFuel;
    [SerializeField] private float MaxFuel;
    [Range(0, -1)]
    [SerializeField] private float DrainSpeed;
    [Range(0, 1)]
    [SerializeField] private float FillSpeed;

    private void Awake()
    {
        AvailableFuel = MaxFuel/2;
        UpdateResourceBar();
    }

    public void UseFuel(bool sizeIncrease, float f, Scaler m) {
        if (sizeIncrease) {
            //We need fuel
            if(AvailableFuel - f < 0) {
                Debug.Log("no fuel");
                return;
            }
            if (m.AddMass(f)) {
                AvailableFuel -= f; // Opposite to decrease when filling a star.
                UpdateResourceBar();
                return;
            }
            else {
                //Case when planet too big
                return;
            }
        }
        //Otherwise, we will gain fuel
        if (AvailableFuel - f > MaxFuel) {
            // Too much fuel
            Debug.Log("too much fuel");
            return;
        }
        if (m.AddMass(-f)) {
            AvailableFuel += f; // Opposite to decrease when filling a star.
            UpdateResourceBar();
            return;
        }
        
    }

    private void UpdateResourceBar()
    {
        // Update the fill amount based on the current resource value
        if (resourceBarFill != null)
        {
            resourceBarFill.fillAmount = AvailableFuel / MaxFuel;
        }
    }
}