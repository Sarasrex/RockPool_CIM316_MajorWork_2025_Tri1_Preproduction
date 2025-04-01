using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animates a sun and moon along semicircular sky arcs to simulate a simple day-night cycle,
/// including light intensity, colour, and sprite visibility fades.
/// </summary>


public class CelestialCycle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform sunBall;
    [SerializeField] private Light sunLight;
    [SerializeField] private Transform moonBall;
    [SerializeField] private Light moonLight;
    [SerializeField] private Transform skyPivot;

    [Header("Cycle Settings")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private float cycleDuration = 20f; // Full day-night duration in seconds

    [Header("Lighting")]
    [SerializeField] private float sunMaxIntensity = 1.5f;
    [SerializeField] private float moonMaxIntensity = 0.5f;
    [SerializeField] private Color sunColor = Color.yellow;
    [SerializeField] private Color moonColor = new Color(0.6f, 0.7f, 1f);

    private void Update()
    {
        float t = Mathf.Repeat(Time.time / cycleDuration, 1f);

        AnimateCelestialBody(sunBall, sunLight, t, 0f, 180f, sunMaxIntensity, sunColor);
        AnimateCelestialBody(moonBall, moonLight, t, 180f, 360f, moonMaxIntensity, moonColor);
    }

    /// <summary>
    /// Animates a celestial body along a semicircular arc with light and sprite adjustments.
    /// </summary>
    private void AnimateCelestialBody(
        Transform body,
        Light bodyLight,
        float t,
        float startAngle,
        float endAngle,
        float maxIntensity,
        Color lightColor)
    {
        float angleDeg = Mathf.Lerp(startAngle, endAngle, t);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(angleRad) * radius,
            Mathf.Sin(angleRad) * radius,
            0f
        );

        body.position = skyPivot.position + offset;

        float heightFactor = Mathf.Clamp01(offset.y / radius);
        bodyLight.intensity = heightFactor * maxIntensity;
        bodyLight.color = lightColor;

        if (body.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = heightFactor;
            spriteRenderer.color = spriteColor;
        }
    }
}

