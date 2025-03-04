using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float floatStrength = 0.1f; // How much it moves up and down
    public float floatSpeed = 1f; // Speed of the movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        // Apply floating effect using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
