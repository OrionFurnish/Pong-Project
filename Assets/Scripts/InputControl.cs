using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {
    public Rigidbody player1, player2;
    public float speed;

    void Update() {
        CheckPlayerInput("P1", player1);
        CheckPlayerInput("P2", player2);
    }

    void CheckPlayerInput(string axis, Rigidbody rb) {
        if (Input.GetAxis(axis) != 0) {
            rb.AddForce(0, Input.GetAxis(axis)*Time.deltaTime*speed, 0);
        }
    }
}
