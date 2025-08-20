using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassAnimationController : MonoBehaviour
{
    [Header("Animation States")]
    public GameObject CompassAnimation_Low;
    public GameObject CompassAnimation_LowMedium;
    public GameObject CompassAnimation_Medium;
    public GameObject CompassAnimation_MediumFull;
    public GameObject CompassAnimation_Full;

    [Tooltip("Duration of the scale transition in seconds")]
    public float transitionDuration = 0.3f;

    private GameObject currentVisual;
    private Coroutine transitionCoroutine;

    // Cache original (intended) scales so we can always snap back cleanly
    private readonly Dictionary<GameObject, Vector3> baseScale = new Dictionary<GameObject, Vector3>();
    private GameObject[] visuals;

    void Awake()
    {
        visuals = new[]
        {
            CompassAnimation_Low,
            CompassAnimation_LowMedium,
            CompassAnimation_Medium,
            CompassAnimation_MediumFull,
            CompassAnimation_Full
        };

        // Cache scales and ensure a clean starting state
        foreach (var v in visuals)
        {
            if (!v) continue;
            if (!baseScale.ContainsKey(v)) baseScale[v] = v.transform.localScale;
            v.transform.localScale = baseScale[v];
            v.SetActive(false);
        }
        currentVisual = null; // nothing shown until first UpdateCompassVisual call
    }

    public void UpdateCompassVisual(float normalisedValue)
    {
        GameObject nextVisual = GetTargetVisual(normalisedValue);
        if (currentVisual == nextVisual) return;

        // If we interrupt a transition, normalize everything so no state is left tiny
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
            transitionCoroutine = null;
            NormalizeAll(exceptA: currentVisual, exceptB: nextVisual);
        }

        transitionCoroutine = StartCoroutine(TransitionVisual(currentVisual, nextVisual));
        currentVisual = nextVisual;
    }

    GameObject GetTargetVisual(float value)
    {
        float percent = value * 100f;

        if (percent < 20f) return CompassAnimation_Low;
        if (percent < 40f) return CompassAnimation_LowMedium;
        if (percent < 60f) return CompassAnimation_Medium;
        if (percent < 80f) return CompassAnimation_MediumFull;
        return CompassAnimation_Full;
    }

    IEnumerator TransitionVisual(GameObject fromObj, GameObject toObj)
    {
        // Prep targets
        if (fromObj && !baseScale.ContainsKey(fromObj)) baseScale[fromObj] = fromObj.transform.localScale;
        if (toObj && !baseScale.ContainsKey(toObj)) baseScale[toObj] = toObj.transform.localScale;

        if (toObj)
        {
            toObj.SetActive(true);
            toObj.transform.localScale = Vector3.zero; // grow from 0 to base
        }

        float t = 0f;
        while (t < transitionDuration)
        {
            t += Time.unscaledDeltaTime; // UI feels better unscaled
            float a = Mathf.Clamp01(t / transitionDuration);

            if (fromObj) fromObj.transform.localScale = Vector3.Lerp(baseScale[fromObj], Vector3.zero, a);
            if (toObj) toObj.transform.localScale = Vector3.Lerp(Vector3.zero, baseScale[toObj], a);

            yield return null;
        }

        // Snap to final states
        if (fromObj)
        {
            fromObj.transform.localScale = baseScale[fromObj];
            fromObj.SetActive(false);
        }
        if (toObj)
        {
            toObj.transform.localScale = baseScale[toObj];
        }

        transitionCoroutine = null;
    }

    // Make sure everything (except the two we’re animating) is disabled and at base scale
    private void NormalizeAll(GameObject exceptA = null, GameObject exceptB = null)
    {
        foreach (var v in visuals)
        {
            if (!v) continue;
            if (v == exceptA || v == exceptB) continue;
            if (baseScale.TryGetValue(v, out var s)) v.transform.localScale = s;
            v.SetActive(false);
        }
        if (exceptA && baseScale.TryGetValue(exceptA, out var sa)) exceptA.transform.localScale = sa;
        if (exceptB && baseScale.TryGetValue(exceptB, out var sb)) exceptB.transform.localScale = sb;
    }
}
