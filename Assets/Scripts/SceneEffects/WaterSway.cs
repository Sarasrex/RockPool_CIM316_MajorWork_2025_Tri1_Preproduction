using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockpoolSway : MonoBehaviour
{
    public float swayAngle = 3f;      // Max angle in degrees
    public float swaySpeed = 1f;      // Speed of the sway

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation; // Store the original rotation
    }

    void Update()
    {
        // Calculate rotation angle using sine wave
        float angle = Mathf.Sin(Time.time * swaySpeed) * swayAngle;

        // Apply rotation on the Z axis (works best for 2D sprites)
        transform.rotation = startRotation * Quaternion.Euler(0f, 0f, angle);
    }
}