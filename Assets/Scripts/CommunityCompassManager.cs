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
        if (hermits == null || hermits.Length == 0)
        {
            Debug.LogWarning("No hermits found in CommunityCompassManager!");
            return;
        }

        float total = 0f;
        foreach (var crab in hermits)
        {
            Debug.Log(crab.hermitName + " happiness: " + crab.happiness);
            total += crab.happiness;
        }

        float average = total / hermits.Length;
        Debug.Log("Updated community average: " + average);

        if (communitySlider != null)
        {
            communitySlider.value = average;
        }
        else
        {
            Debug.LogWarning("Community Slider not assigned!");
        }
    }
}
