using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HermitSlider : MonoBehaviour
{
    public HermitCrabDropTarget crab;     // Link to this hermit's behaviour
    public Slider happinessSlider;        // The visual UI slider

    void Start()
    {
        if (happinessSlider != null && crab != null)
        {
            happinessSlider.minValue = 0;
            happinessSlider.maxValue = 100;
            happinessSlider.value = crab.happiness;
        }
    }

    void Update()
    {
        if (happinessSlider != null && crab != null)
        {
            happinessSlider.value = crab.happiness;
        }
    }
}
