using System;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    public RocketController rocket;

    [SerializeField] private GameObject shieldObject;
    [SerializeField] private float shieldDuration = 15f;

    public static Action OnShieldDeactivated;

    private void Awake()
    {
        Instance = this;
        rocket = GetComponent<RocketController>();
    }

    public void StartShieldPowerUp()
    {
        shieldObject.SetActive(true);
        rocket.ActivateShield();
        Invoke(nameof(DeactivateShieldPowerUp), shieldDuration);
    }

    public void DeactivateShieldPowerUp()
    {
        OnShieldDeactivated?.Invoke();
        shieldObject.SetActive(false);
        rocket.DeactivateShield();
    }
}
