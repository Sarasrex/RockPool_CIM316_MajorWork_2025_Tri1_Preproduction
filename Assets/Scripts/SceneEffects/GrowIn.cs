using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowIn : MonoBehaviour
{
    public float growDuration = 1f;
    private Vector3 targetScale;

    void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(Grow());
    }

    System.Collections.IEnumerator Grow()
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
    }
}
