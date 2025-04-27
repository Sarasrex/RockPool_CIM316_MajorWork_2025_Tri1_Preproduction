using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCycleArc : MonoBehaviour
{
    [Header("Cycle Timing (in seconds)")]
    [SerializeField] private float dayDuration = 600f;
    [SerializeField] private float nightDuration = 300f;

    [Header("Light References")]
    [SerializeField] private Light sunLight;
    [SerializeField] private Light moonLight;

    [Header("Light Intensity Curve")]
    [SerializeField] private AnimationCurve lightCurve;

    [Header("Debug / Dev Tools")]
    [SerializeField] private bool speedUpTime = false;
    [SerializeField] private float timeMultiplier = 5f;

    [Header("Manual Time Override")]
    [SerializeField, Range(0f, 1f)] private float timeOfDay = 0f;
    [SerializeField] private bool overrideTimeManually = false;

    private float fullCycleDuration;

    void Start()
    {
        fullCycleDuration = dayDuration + nightDuration;
    }

    void Update()
    {
        if (!overrideTimeManually)
        {
            float delta = Time.deltaTime;
            if (speedUpTime)
            {
                delta *= timeMultiplier;
            }

            timeOfDay += delta / fullCycleDuration;
            if (timeOfDay > 1f)
                timeOfDay -= 1f;
        }

        float angle = Mathf.Lerp(0f, 360f, timeOfDay);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        sunLight.intensity = lightCurve.Evaluate(timeOfDay);
        moonLight.intensity = lightCurve.Evaluate((timeOfDay + 0.5f) % 1f);
    }
}

