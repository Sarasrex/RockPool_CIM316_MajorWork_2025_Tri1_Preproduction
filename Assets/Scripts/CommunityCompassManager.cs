using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityCompassManager : MonoBehaviour
{
    public static CommunityCompassManager Instance;
    public HermitCrabDropTarget[] hermits;
    public Slider communitySlider; // UI element for community compass

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateCommunityHappiness()
    {
        float total = 0;
        foreach (var crab in hermits)
        {
            total += crab.happiness;
        }

        float average = total / hermits.Length;
        communitySlider.value = average;
    }
}
