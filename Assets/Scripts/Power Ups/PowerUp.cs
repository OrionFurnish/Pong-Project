using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    public float ballSpeedMult;
    public bool controlMult;

    public void Activate() {
        PowerUpManager.AddBallSpeedModifer(ballSpeedMult);
        if(controlMult) {
            PowerUpManager.AddControlModifer(BallControl.lastPaddleHit);
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) {
        Activate();
    }
}
