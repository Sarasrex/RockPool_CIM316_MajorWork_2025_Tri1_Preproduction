using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnaclePulse : MonoBehaviour
{
    public float pulseSpeed = 2f;       // Speed of the pulse
    public float pulseStrength = 0.1f;  // How much it scales up/down
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseStrength;
        transform.localScale = originalScale + Vector3.one * pulse;
    }
}
