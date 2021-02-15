using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    private static float ballSpeedMult = 1f;
    private static float controlMult = 1f;
    private static Rigidbody lastPaddleHit;

    public static PowerUpManager instance;
    public Transform speedPowerupPrefab, controlPowerupPrefab;
    public Vector2 minSpawn, maxSpawn;
    public float spawnChance;
    public BallControl ball;

    private void Start() {
        instance = this;
        StartSpawningPowerups();
    }

    public static void StartSpawningPowerups() {
        instance.StartCoroutine(instance.SpawnPowerups());
    }

    public static void StopSpawningPowerups() {
        instance.StopAllCoroutines();
    }

    public static void DestroyAllPowerups() {
        foreach(PowerUp p in instance.GetComponentsInChildren<PowerUp>()) {
            Destroy(p.gameObject);
        }
    }

    IEnumerator SpawnPowerups() {
        while(true) {
            float r = Random.Range(0f, 100f);
            if(r <= spawnChance) {
                // Select Powerup
                int powerupSpawned = Random.Range(1, 3); // 1 = speed; 2 = control
                Transform chosenPowerup = null;
                switch(powerupSpawned) {
                    case 1:
                        chosenPowerup = speedPowerupPrefab;
                        break;
                    case 2:
                        chosenPowerup = controlPowerupPrefab;
                        break;
                    default:
                        Debug.Log("Error: Invalid powerup chosen");
                        break;
                }
                // Select Position
                Vector3 spawnPos = new Vector3(Random.Range(minSpawn.x, maxSpawn.x), Random.Range(minSpawn.y, maxSpawn.y));
                // Spawn Powerup
                if(chosenPowerup != null) {
                    Transform powerupTransform = Instantiate(chosenPowerup, spawnPos, Quaternion.identity);
                    powerupTransform.SetParent(transform);
                }
            } yield return new WaitForSeconds(1f);
        }
    }

    public static void AddBallSpeedModifer(float newBallSpeedMult) {
        ballSpeedMult += newBallSpeedMult;
        instance.ball.UpdatePowerUpVelocity();
    }

    public static void AddControlModifer(Rigidbody newLastPaddleHit) {
        controlMult = -1f;
        lastPaddleHit = newLastPaddleHit;
    }

    public static void PaddleReset() {
        ballSpeedMult = 1f;
        controlMult = 1f;
    }

    public static float GetBallSpeedMult() {return ballSpeedMult;}

    public static float GetControlMult(Rigidbody controlRigidbody) {
        if(lastPaddleHit != controlRigidbody) {return controlMult;}
        else { return 1f; }
    }
}
