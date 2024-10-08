using UnityEngine;
using UnityEngine.UI;

public class RocketFuel : MonoBehaviour
{
    [SerializeField] private float initialFuel = 500f;
    private float currentFuel;
    private Vector2 lastPosition;
    private float fuelAmountOnPickup = 150f;

    [SerializeField] private Image fuelFillImage;  // Reference to the image with fill
    private bool isAlertAllowed = true;

    private void Start()
    {
        currentFuel = initialFuel;
        lastPosition = transform.position;

        UpdateFuelUI();  // Update the fuel UI at the start
    }

    private void Update()
    {
        if(isAlertAllowed)
        {
            if (currentFuel < 50f)
            {
                //AudioManager.Instance.PlayFuelAlertSFX();
                isAlertAllowed = false;
            }
        }
        ConsumeFuelBasedOnMovement();
    }

    private void ConsumeFuelBasedOnMovement()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);
        if (distanceMoved > 0)
        {
            currentFuel -= distanceMoved;
            if (currentFuel <= 0)
            {
                currentFuel = 0;
                HandleOutOfFuel();
            }
            lastPosition = transform.position;

            UpdateFuelUI();  
        }
    }

    public void AddFuel()
    {
        isAlertAllowed = true;
        currentFuel += fuelAmountOnPickup;
        if (currentFuel > initialFuel)
        {
            currentFuel = initialFuel;
        }
        UpdateFuelUI(); 
    }

    private void HandleOutOfFuel()
    {
        Debug.Log("Out of Fuel! You lose.");
        RocketController rocketController = GetComponent<RocketController>();
        if (rocketController != null)
        {
            rocketController.OnFuelDepleted();
        }
    }

    private void UpdateFuelUI()
    {
        if (fuelFillImage != null)
        {
            fuelFillImage.fillAmount = currentFuel / initialFuel; 
        }
    }

    public float GetCurrentFuel()
    {
        return currentFuel;
    }
}
