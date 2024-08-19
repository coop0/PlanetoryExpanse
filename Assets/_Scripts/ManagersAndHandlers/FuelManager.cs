using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private Image resourceBarFill; // Reference to the Image component used for the fill

    [SerializeField] private float AvailableFuel;
    [SerializeField] private float MaxFuel;
    [Range(100, 1000)]
    [SerializeField] private float DrainSpeed;
    [Range(100, 1000)]
    [SerializeField] private float FillSpeed;

    private void Start()
    {
        AvailableFuel = MaxFuel/2;
        UpdateResourceBar();
    }

    public void UseFuel(bool sizeIncrease, float amount, Scaler m) {
        if (sizeIncrease) //We need fuel
        {
            float f = amount * FillSpeed;
            if (AvailableFuel - f < 0)
            {
                return;
            }
            if (m.AddMass(f))
            {
                AvailableFuel -= f; // inverting f preserves the total fuel in the system.
                UpdateResourceBar();
                return;
            }
            else
            {
                //Case when planet too big
                return;
            }
        }
        else //Otherwise, we gain fuel
        {
            float f = amount * DrainSpeed;
            if (AvailableFuel - f > MaxFuel) // Too much fuel
            {
                return;
            }
            if (m.AddMass(-f))
            {
                AvailableFuel += f; // Opposite to decrease when filling a star.
                UpdateResourceBar();
                return;
            }
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