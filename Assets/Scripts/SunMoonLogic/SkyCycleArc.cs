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

    [Header("Audio")]
    [SerializeField] private AudioSource dayAudioSource;
    [SerializeField] private AudioSource nightAudioSource;
    [SerializeField] private float crossfadeSpeed = 1f; // How fast to fade

    private float fullCycleDuration;
    private bool isCurrentlyDay = true;

    void Start()
    {
        fullCycleDuration = dayDuration + nightDuration;

        dayAudioSource.loop = true;
        nightAudioSource.loop = true;

        dayAudioSource.volume = 1f;
        nightAudioSource.volume = 0f;

        dayAudioSource.Play();
        nightAudioSource.Play();
    }

    void Update()
    {
        if (!overrideTimeManually)
        {
            float delta = Time.deltaTime;
            if (speedUpTime)
                delta *= timeMultiplier;

            timeOfDay += delta / fullCycleDuration;
            if (timeOfDay > 1f)
                timeOfDay -= 1f;
        }

        float angle = Mathf.Lerp(0f, 360f, timeOfDay);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        sunLight.intensity = lightCurve.Evaluate(timeOfDay);
        moonLight.intensity = lightCurve.Evaluate((timeOfDay + 0.5f) % 1f);

        UpdateAudioCrossfade();
    }

    void UpdateAudioCrossfade()
    {
        // Daytime = 0.0 to 0.5; Nighttime = 0.5 to 1.0
        bool isDayNow = timeOfDay < 0.5f;

        if (isDayNow)
        {
            // Fade to day
            dayAudioSource.volume = Mathf.MoveTowards(dayAudioSource.volume, 1f, Time.deltaTime * crossfadeSpeed);
            nightAudioSource.volume = Mathf.MoveTowards(nightAudioSource.volume, 0f, Time.deltaTime * crossfadeSpeed);
        }
        else
        {
            // Fade to night
            nightAudioSource.volume = Mathf.MoveTowards(nightAudioSource.volume, 1f, Time.deltaTime * crossfadeSpeed);
            dayAudioSource.volume = Mathf.MoveTowards(dayAudioSource.volume, 0f, Time.deltaTime * crossfadeSpeed);
        }
    }
}
