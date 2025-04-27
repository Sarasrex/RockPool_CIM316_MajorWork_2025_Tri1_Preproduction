using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CommunityCompassManager : MonoBehaviour
{
    public static CommunityCompassManager Instance;

    [Header("Community Settings")]
    public HermitCrabDropTarget[] hermits;
    public Slider communitySlider;
    public GameObject winPanel;
    private bool hasWon = false;
    [Range(0f, 1f)] public float winThreshold = 1f; // 1 = 100% happiness

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCommunityHappiness();
    }

    public void UpdateCommunityHappiness()
    {
        if (hermits == null || hermits.Length == 0)
        {
            Debug.LogWarning("No hermits assigned in CommunityCompassManager!");
            return;
        }

        float totalHappiness = 0f;

        foreach (var hermit in hermits)
        {
            totalHappiness += hermit.happiness;
        }

        float averageHappiness = totalHappiness / hermits.Length;

        if (communitySlider != null)
        {
            communitySlider.value = averageHappiness / 100f;
        }
        else
        {
            Debug.LogWarning("Community slider not assigned!");
        }

        if (!hasWon && averageHappiness >= winThreshold * 100f)
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
            Debug.Log("Community is thriving! Win panel activated.");
        }
        else
        {
            Debug.LogWarning("Win panel not assigned!");
        }
    }
}
