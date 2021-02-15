using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
    public static Rigidbody lastPaddleHit;
    public float startSpeed;
    public float addedSpeedOnPaddle;
    public Transform sparks;
    public ScoreTracker scoreTracker;

    private Vector3 totalSpeed;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        LaunchBall(0);
    }

    public void LaunchBall(int side) {
        // Reset values
        transform.position = new Vector3(0, 0, 0);

        if(side == 0) { side = Random.Range(1, 3); } // Randomly select side if no side is chosen
        float ySpeed = Random.Range(-startSpeed, startSpeed);

        if(side == 1) {
            rb.velocity = new Vector3(startSpeed*-1, ySpeed, 0);
        } else {
            rb.velocity = new Vector3(startSpeed, ySpeed, 0);
        } totalSpeed = rb.velocity;
    }

    public void PauseBall() {

        transform.position = new Vector3(0, 0, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

    public void UpdatePowerUpVelocity() {
        rb.velocity = totalSpeed * PowerUpManager.GetBallSpeedMult();
    }

    public void OnCollisionEnter(Collision collision) {
        // Play Collision Audio
        AudioSource audioSource = collision.collider.gameObject.GetComponent<AudioSource>();
        if(audioSource != null) {
            audioSource.pitch = .5f + collision.relativeVelocity.magnitude/20f;
            audioSource.Play();
        } 
        if (collision.collider.gameObject.CompareTag("Goal")) { scoreTracker.Score(collision.collider.gameObject.name); }
        else { Bounce(collision); }
    }

    public void OnCollisionExit(Collision collision) {
        if(!collision.collider.gameObject.CompareTag("Goal")) {
            rb.velocity = totalSpeed;
        }
    }

    void Bounce(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Paddle")) {
            lastPaddleHit = collision.collider.GetComponent<Rigidbody>(); // Used for power up
            PowerUpManager.PaddleReset();
            totalSpeed += new Vector3(addedSpeedOnPaddle*Mathf.Sign(totalSpeed.x), 0, 0); // Add speed
            // Determine contact point
            float relativeContact = collision.GetContact(0).point.y - collision.collider.transform.position.y;
            float ySpeed = relativeContact*Mathf.Abs(totalSpeed.x/3f);
            // Update total speed
            totalSpeed = new Vector3(-totalSpeed.x, ySpeed, 0);
        } else {
            totalSpeed = new Vector3(totalSpeed.x, -totalSpeed.y, 0);
        }
        // Set new velocity
        rb.velocity = totalSpeed;
        // Sparks effect
        if (collision.relativeVelocity.magnitude > 12) {
            Instantiate(sparks, transform.position, Quaternion.identity);
        }
    }
}
