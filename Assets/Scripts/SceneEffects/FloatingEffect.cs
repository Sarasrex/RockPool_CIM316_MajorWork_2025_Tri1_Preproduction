using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("Floating Strength")]
    public float floatStrengthY = 0.1f; // How much it moves up and down
    public float floatStrengthX = 0f;   // How much it moves left and right
    public float floatStrengthZ = 0f;   // How much it moves forward and backward

    [Header("Floating Speed")]
    public float floatSpeedY = 1f; // Speed of Y movement
    public float floatSpeedX = 1f; // Speed of X movement
    public float floatSpeedZ = 1f; // Speed of Z movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeedY) * floatStrengthY;
        float newX = startPosition.x + Mathf.Sin(Time.time * floatSpeedX) * floatStrengthX;
        float newZ = startPosition.z + Mathf.Sin(Time.time * floatSpeedZ) * floatStrengthZ;

        transform.position = new Vector3(newX, newY, newZ);
    }
}
