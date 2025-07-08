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

    public void UpdateCompassVisual(float normalisedValue)
    {
        // Hide all first
        CompassAnimation_Low.SetActive(false);
        CompassAnimation_LowMedium.SetActive(false);
        CompassAnimation_Medium.SetActive(false);
        CompassAnimation_MediumFull.SetActive(false);
        CompassAnimation_Full.SetActive(false);

        // Show the correct one based on percentage
        float percentage = normalisedValue * 100f;

        if (percentage < 20f)
            CompassAnimation_Low.SetActive(true);
        else if (percentage < 40f)
            CompassAnimation_LowMedium.SetActive(true);
        else if (percentage < 60f)
            CompassAnimation_Medium.SetActive(true);
        else if (percentage < 80f)
            CompassAnimation_MediumFull.SetActive(true);
        else
            CompassAnimation_Full.SetActive(true);
    }
}
