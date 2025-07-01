using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// This script tracks how happy all the hermit crabs are,
// updates the community happiness slider on the screen,
// and shows a win screen when the group is happy enough.
public class CommunityCompassManager : MonoBehaviour
{
    public static CommunityCompassManager Instance; // Allows other scripts to call this one easily (like a shared controller)

    private HermitCrabDropTarget[] hermits;  // Automatically filled when the game starts
    public Slider communitySlider;          // The compass UI slider showing overall happiness

    public GameObject winPanel;             // The panel that pops up when the community wins
    private bool hasWon = false;            // Prevents the win screen from showing more than once

    public CompassAnimationController compassAnimationController;

    [Header("Settings")]
    [Range(0f, 1f)] public float winThreshold = 1f; // How full the compass needs to be to win (1 = 100%)

    // This runs early — before anything else. It sets up this script so it's easy to access from other scripts.
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject); // If another one already exists, remove this one
    }

    // This runs at the start of the game
    void Start()
    {
        hermits = FindObjectsOfType<HermitCrabDropTarget>(); // Finds all hermits in the scene
        Debug.Log("Hermit found " + hermits.Length + " hermits in the scene.");
        StartCoroutine(DelayedUpdate());
    }

    // This waits just a moment to let everything load, then checks the initial happiness
    IEnumerator DelayedUpdate()
    {
        yield return null; // Wait one frame
        UpdateCommunityHappiness(); // Now check happiness across the hermits
    }

    // This method adds up all the hermits' happiness levels,
    // finds the average, updates the slider, and checks if you've won
    public void UpdateCommunityHappiness()
    {
        if (hermits == null || hermits.Length == 0)
        {
            Debug.LogWarning("No hermits assigned to the compass manager!");
            return; // Stop here if the list of hermits is empty
        }

        float total = 0f;

        // Go through each hermit and add up their happiness
        foreach (var crab in hermits)
        {
            Debug.Log(" " + crab.hermitName + " happiness: " + crab.happiness);
            total += crab.happiness;
        }

        // Work out the average (total happiness divided by number of hermits)
        float average = total / hermits.Length;
        float normalisedAverage = average / 100f; // Turn it into a value between 0 and 1 (since happiness is out of 100)

        Debug.Log("Updated community average: " + average);
        Debug.Log("Normalised for slider: " + normalisedAverage);

        // Set the slider to match the community's average happiness
        if (communitySlider != null)
        {
            communitySlider.value = normalisedAverage;
        }

        // Update the compass animation based on the value
        if (compassAnimationController != null)
        {
            compassAnimationController.UpdateCompassVisual(normalisedAverage);
        }

        // Check if the average happiness is enough to win
        if (!hasWon && normalisedAverage >= winThreshold)
        {
            hasWon = true;
            TriggerWinCondition(); // Show the win screen
        }
    }

    // This shows the win panel and hides hermit sliders when the group is happy enough
    private void TriggerWinCondition()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("Win condition reached — win panel activated.");

            // Hide all hermit sliders
            HideHermitSliders();
        }
        else
        {
            Debug.LogWarning("Win Panel not assigned in inspector.");
        }
    }

    // This hides the HermitSlider_ GameObjects under each hermit's Canvas
    private void HideHermitSliders()
    {
        foreach (var crab in hermits)
        {
            Transform sliderTransform = crab.transform.Find("Canvas/HermitSlider_" + crab.hermitName);
            if (sliderTransform != null)
            {
                sliderTransform.gameObject.SetActive(false);
                Debug.Log("Hid slider for " + crab.hermitName);
            }
            else
            {
                Debug.LogWarning("Could not find slider for " + crab.hermitName);
            }
        }
    }
}
