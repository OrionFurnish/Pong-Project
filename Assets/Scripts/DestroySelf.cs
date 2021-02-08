using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {
    public float timer;

    void Start() {
        Invoke("Deconstruct", timer);
    }

    void Deconstruct() {
        Destroy(gameObject);
    }
}
