using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldManager : IPowerUp
{
    private RocketController rocket;

    public ShieldManager(RocketController rocket){
    
    this.rocket = rocket;

   }

   public void Execute(){
    // Logic to activate shield // 
    // waiting for visuals from designer //
    rocket.ActivateShield();
   }
}
