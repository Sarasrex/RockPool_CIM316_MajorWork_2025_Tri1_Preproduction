using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    [Header("Floating Strength")]
    public float floatStrengthY = 0.1f;
    public float floatStrengthX = 0f;
    public float floatStrengthZ = 0f;

    [Header("Floating Speed")]
    public float floatSpeedY = 1f;
    public float floatSpeedX = 1f;
    public float floatSpeedZ = 1f;

    [Header("Rotation Mod (Degrees)")]
    public float rotationStrengthX = 0f;
    public float rotationStrengthY = 0f;
    public float rotationStrengthZ = 0f;

    [Header("Rotation Speed")]
    public float rotationSpeedX = 1f;
    public float rotationSpeedY = 1f;
    public float rotationSpeedZ = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Floating
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeedY) * floatStrengthY;
        float newX = startPosition.x + Mathf.Sin(Time.time * floatSpeedX) * floatStrengthX;
        float newZ = startPosition.z + Mathf.Sin(Time.time * floatSpeedZ) * floatStrengthZ;
        transform.position = new Vector3(newX, newY, newZ);

        // Rotation
        float rotX = Mathf.Sin(Time.time * rotationSpeedX) * rotationStrengthX;
        float rotY = Mathf.Sin(Time.time * rotationSpeedY) * rotationStrengthY;
        float rotZ = Mathf.Sin(Time.time * rotationSpeedZ) * rotationStrengthZ;
        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
    }
}
