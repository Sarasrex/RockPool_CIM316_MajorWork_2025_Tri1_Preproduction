using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform SunPivot;
    [SerializeField] private Light DirectionalLight;

    [SerializeField] private Transform MoonPivot;
    [SerializeField] private Light MoonLight;

    [Header("Lighting Presets")]
    [SerializeField] private LightingPreset SunPreset;
    [SerializeField] private LightingPreset MoonPreset;

    [Header("Time Settings")]
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    [Header("Light Intensity Settings")]
    [SerializeField] private float MaxSunIntensity = 1f;
    [SerializeField] private float MaxMoonIntensity = 0.3f;

    private void Update()
    {
        if (SunPreset == null || MoonPreset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; // Clamp between 0–24
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        // Create a smooth arc for both lights
        float sunAngle = Mathf.Lerp(-90f, 90f, timePercent); // arcs across the sky
        float moonPercent = (timePercent + 0.5f) % 1f;
        float moonAngle = Mathf.Lerp(-90f, 90f, moonPercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.transform.localRotation = Quaternion.Euler(sunAngle, 180f, 0f); // 180 Y to shine forward
            DirectionalLight.color = SunPreset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.intensity = MaxSunIntensity * Mathf.Clamp01(Mathf.Cos(timePercent * Mathf.PI * 2f));
        }

        if (MoonLight != null)
        {
            MoonLight.transform.localRotation = Quaternion.Euler(moonAngle, 180f, 0f); // 180 Y to shine forward
            MoonLight.color = MoonPreset.DirectionalColor.Evaluate(moonPercent);
            MoonLight.intensity = MaxMoonIntensity * Mathf.Clamp01(Mathf.Cos(moonPercent * Mathf.PI * 2f));
        }

        RenderSettings.ambientLight = SunPreset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = SunPreset.FogColor.Evaluate(timePercent);
    }

}
