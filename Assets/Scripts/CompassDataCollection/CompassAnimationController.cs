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

    public void UpdateCompassVisual(float normalisedValue)
    {
        GameObject nextVisual = GetTargetVisual(normalisedValue);

        if (currentVisual == nextVisual) return;

        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(TransitionVisual(currentVisual, nextVisual));
        currentVisual = nextVisual;
    }

    GameObject GetTargetVisual(float value)
    {
        float percent = value * 100f;

        if (percent < 20f)
            return CompassAnimation_Low;
        else if (percent < 40f)
            return CompassAnimation_LowMedium;
        else if (percent < 60f)
            return CompassAnimation_Medium;
        else if (percent < 80f)
            return CompassAnimation_MediumFull;
        else
            return CompassAnimation_Full;
    }

    IEnumerator TransitionVisual(GameObject fromObj, GameObject toObj)
    {
        // Step 1: Shrink out the current visual
        if (fromObj != null)
        {
            Vector3 originalScale = fromObj.transform.localScale;
            float t = 0f;
            while (t < transitionDuration)
            {
                t += Time.deltaTime;
                float scale = Mathf.Lerp(1f, 0f, t / transitionDuration);
                fromObj.transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }
            fromObj.SetActive(false);
            fromObj.transform.localScale = originalScale; // reset scale in case it's used later
        }

        // Step 2: Grow in the next visual
        if (toObj != null)
        {
            toObj.SetActive(true);
            Vector3 originalScale = toObj.transform.localScale;
            toObj.transform.localScale = Vector3.zero;

            float t = 0f;
            while (t < transitionDuration)
            {
                t += Time.deltaTime;
                float scale = Mathf.Lerp(0f, 1f, t / transitionDuration);
                toObj.transform.localScale = new Vector3(scale, scale, scale);
                yield return null;
            }

            toObj.transform.localScale = originalScale;
        }
    }
}
