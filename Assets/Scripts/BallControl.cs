using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
    public float startSpeed;
    public float addedSpeedOnPaddle;
    public Transform sparks;

    private float totalSpeed;
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
        } totalSpeed = startSpeed;
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Paddle")) { totalSpeed += addedSpeedOnPaddle; }

        if (collision.collider.gameObject.CompareTag("Goal")) { ScoreTracker.Score(collision.collider.gameObject.name, this); }
        else { Bounce(collision); }
    }

    void Bounce(Collision collision) {
        Vector3 newVelocity = collision.relativeVelocity;
        newVelocity.x = totalSpeed * Mathf.Sign(newVelocity.x);
        // Determine bounce direction
        ContactPoint contactPoint = collision.contacts[0];
        if (contactPoint.normal.x != 0) { newVelocity = new Vector3(newVelocity.x, -newVelocity.y, 0); }
        if (contactPoint.normal.y != 0) { newVelocity = new Vector3(-newVelocity.x, newVelocity.y, 0); }
        // Set new velocity
        rb.velocity = newVelocity;
        // Sparks effect
        if(collision.relativeVelocity.magnitude > 12) {
            Instantiate(sparks, transform.position, Quaternion.identity);
        }
    }
}
