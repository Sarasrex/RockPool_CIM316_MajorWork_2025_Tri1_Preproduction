using System.Collections;
using UnityEngine;

public class GrowAndPulse : MonoBehaviour
{
    [Header("Grow In Settings")]
    public bool enableGrowIn = true;
    public float growDuration = 1f;

    [Header("Pulse Settings")]
    public bool enablePulse = true;
    public float pulseSpeed = 0.5f;
    public float pulseStrength = 0.1f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool hasGrownIn = false;
    private float pulseOffset;

    void Start()
    {
        // Set a random time offset so each object pulses differently
        pulseOffset = Random.Range(0f, 2f * Mathf.PI);

        originalScale = transform.localScale;
        if (enableGrowIn)
        {
            targetScale = originalScale;
            transform.localScale = Vector3.zero;
            StartCoroutine(Grow());
        }
        else
        {
            hasGrownIn = true;
        }
    }

    void Update()
    {
        if (!enablePulse || !hasGrownIn) return;

        float pulse = Mathf.Sin(Time.time * pulseSpeed + pulseOffset) * pulseStrength;
        transform.localScale = originalScale + Vector3.one * pulse;
    }

    IEnumerator Grow()
    {
        float elapsed = 0f;
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;
            float eased = Mathf.Sin(t * Mathf.PI * 0.5f); // smooth ease-out
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, eased);
            yield return null;
        }

        transform.localScale = targetScale;
        hasGrownIn = true;
    }
}
