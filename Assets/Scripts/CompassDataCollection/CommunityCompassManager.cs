using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityCompassManager : MonoBehaviour
{
    public static CommunityCompassManager Instance;
    public HermitCrabDropTarget[] hermits;
    public Slider communitySlider; // UI element for community compass

    public GameObject winPanel;
    private bool hasWon = false;

    [Header("Settings")]
    [Range(0f, 1f)] public float winThreshold = 1f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(DelayedUpdate());
    }

    IEnumerator DelayedUpdate()
    {
        yield return null; // wait one frame
        UpdateCommunityHappiness(); // now everything has loaded
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

        // Normalise to 0–1 range assuming 100 is max happiness
        float normalisedAverage = average / 100f;

        if (communitySlider != null)
        {
            communitySlider.value = normalisedAverage;
        }
        else
        {
            Debug.LogWarning("Community Slider not assigned!");
        }

        // Win condition check (based on slider fill)
        if (!hasWon && Mathf.Approximately(normalisedAverage, winThreshold))
        {
            hasWon = true;
            TriggerWinCondition();
        }
    }

    private void TriggerWinCondition()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("Win condition reached — win panel activated.");
        }
        else
        {
            Debug.LogWarning("Win Panel not assigned in inspector.");
        }
    }
}
