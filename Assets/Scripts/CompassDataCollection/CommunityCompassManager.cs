using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Tracks overall hermit happiness and updates the community compass UI.
// Triggers a win condition when the average happiness reaches the defined threshold.
public class CommunityCompassManager : MonoBehaviour
{
    public static CommunityCompassManager Instance; // Singleton reference

    public HermitCrabDropTarget[] hermits;  // Array of all hermits in the scene
    public Slider communitySlider;          // UI slider representing community happiness

    public GameObject winPanel;             // Shown when win condition is met
    private bool hasWon = false;            // Prevents win condition from triggering twice

    [Header("Settings")]
    [Range(0f, 1f)] public float winThreshold = 1f; // 1 means 100 average happiness across all hermits

    // Set up singleton
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start with a slight delay to allow other objects to initialise
    void Start()
    {
        StartCoroutine(DelayedUpdate());
    }

    // Waits one frame before updating, useful if hermits are instantiated at runtime
    IEnumerator DelayedUpdate()
    {
        yield return null;
        UpdateCommunityHappiness();
    }

    /// <summary>
    /// Calculates the average happiness of all hermits and updates the compass UI
    /// </summary>
    /// <param name="delta"> Incoming data from hermit drops </param>
    public void UpdateCommunityHappiness()
    {
        if (hermits == null || hermits.Length == 0)
        {
            Debug.LogWarning("No hermits found in CommunityCompassManager!");
            return;
        }

        float total = 0f;

        // Sum all hermit happiness values
        foreach (var crab in hermits)
        {
            Debug.Log(crab.hermitName + " happiness: " + crab.happiness);
            total += crab.happiness;
        }

        // Calculate average
        float average = total / (float)hermits.Length;
        Debug.Log("Updated community average: " + average);

        // Update the UI slider with the new average
        if (communitySlider != null)
        {
            communitySlider.value = average;
        }

        // Check if the average happiness meets the win threshold
        if (!hasWon && average >= winThreshold * 100f)
        {
            hasWon = true;
            TriggerWinCondition();
        }
    }

    // Activates the win panel and logs win condition
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
