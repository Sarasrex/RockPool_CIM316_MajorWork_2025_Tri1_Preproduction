using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// Tracks hermit happiness, updates the compass UI, and triggers the Final cutscene on win.
public class CommunityCompassManager : MonoBehaviour
{
    public static CommunityCompassManager Instance;

    private HermitCrabDropTarget[] hermits;   // Filled at start
    public Slider communitySlider;            // UI compass slider (0..1)

    [Header("Legacy (optional)")]
    public GameObject winPanel;               // Old win panel (kept for safety but not used)

    private bool hasWon = false;

    public CompassAnimationController compassAnimationController;

    [Header("Settings")]
    [Range(0f, 1f)] public float winThreshold = 1f; // 1 = 100%

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) { Destroy(gameObject); return; }
    }

    void Start()
    {
        // Find all hermits in the scene (even inactive), but we’ll only count active ones.
        hermits = FindObjectsOfType<HermitCrabDropTarget>(includeInactive: true);
        Debug.Log($"[CommunityCompass] Found {hermits.Length} hermits in the scene.");
        StartCoroutine(DelayedUpdate());
    }

    IEnumerator DelayedUpdate()
    {
        yield return null; // wait a frame for other scripts to initialise
        UpdateCommunityHappiness();
    }

    /// Call this if you add/remove hermits dynamically and want to rescan.
    public void RefreshHermits()
    {
        hermits = FindObjectsOfType<HermitCrabDropTarget>(includeInactive: true);
    }

    /// Adds up active hermits' happiness, updates the slider/animation, and checks win.
    public void UpdateCommunityHappiness()
    {
        if (hermits == null || hermits.Length == 0)
        {
            Debug.LogWarning("[CommunityCompass] No hermits assigned/found.");
            return;
        }

        float total = 0f;
        int count = 0;

        foreach (var crab in hermits)
        {
            if (crab == null || !crab.gameObject.activeInHierarchy) continue; // only active hermits
            total += crab.happiness;
            count++;
            // Debug.Log($"[CommunityCompass] {crab.hermitName} happiness: {crab.happiness}");
        }

        if (count == 0) return;

        float average = total / count;            // 0..100
        float normalisedAverage = average / 100f; // 0..1

        // Update UI
        if (communitySlider != null) communitySlider.value = normalisedAverage;
        if (compassAnimationController != null) compassAnimationController.UpdateCompassVisual(normalisedAverage);

        // Win?
        if (!hasWon && normalisedAverage >= winThreshold)
        {
            hasWon = true;
            TriggerWinCondition();
        }
    }

    /// On win: hide sliders, and route to the Final cutscene.
    private void TriggerWinCondition()
    {
        // Ensure the old panel doesn't pop
        if (winPanel != null) winPanel.SetActive(false);

        HideHermitSliders();

        string returnScene = SceneManager.GetActiveScene().name;
        bool forcePlay = !CutsceneTracker.HasSeen("Final"); // play once per device/profile

        CutsceneRequest.Set("Final", returnScene, forcePlay);

        // If you’re using additive overlays instead, do:
        // CutsceneRequest.UseAdditive = true;
        // SceneManager.LoadScene("CutSceneLayer", LoadSceneMode.Additive);
        // return;

        // Standard scene swap:
        SceneSwitcher.Load("CutSceneLayer");
        Debug.Log("[CommunityCompass] Win! Triggering Final cutscene.");
    }

    /// Hides the HermitSlider_ GameObjects under each hermit's Canvas
    private void HideHermitSliders()
    {
        if (hermits == null) return;

        foreach (var crab in hermits)
        {
            if (crab == null) continue;
            Transform sliderTransform = crab.transform.Find($"Canvas/HermitSlider_{crab.hermitName}");
            if (sliderTransform != null)
            {
                sliderTransform.gameObject.SetActive(false);
                // Debug.Log($"[CommunityCompass] Hid slider for {crab.hermitName}");
            }
        }
    }
}
