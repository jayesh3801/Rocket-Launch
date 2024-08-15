using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
  public static PowerUpManager Instance;

  void Awake(){
    Instance = this;
  }
  public RocketController rocket;

  void StartShieldPowerUp(){
    IPowerUp ShieldManager = new ShieldManager(rocket);

    rocket.ApplyPowerUp(ShieldManager);
  }
}
