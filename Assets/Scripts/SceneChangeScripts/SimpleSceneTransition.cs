using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Bubble rise transition I’m using between scenes.
/// Notes:
/// - Prevents double triggers
/// - Resets bubble position after load so it’s reusable
/// - Delay is treated as TOTAL time; if rise already takes longer, no extra wait
public class SimpleSceneTransition : MonoBehaviour
{
    [Header("Visual")]
    public Transform bubbleSprite;       // UI/World transform that rises upward
    public float moveSpeed = 500f;       // units/sec (Canvas pixels if under Screen Space UI)
    public float riseDistance = 1500f;   // how far to move before loading the scene

    [Header("Timing")]
    [Tooltip("Target total time from trigger to load. If <= (riseDistance/moveSpeed), I skip extra waiting.")]
    public float delayBeforeLoad = 1.5f;

    [Header("Audio")]
    public AudioSource bubbleAudioSource; // optional whoosh/pop SFX

    private Vector3 startPosition;
    private string targetSceneName;
    private bool isTransitioning;

    private void Awake()
    {
        if (bubbleSprite != null)
            startPosition = bubbleSprite.position;

        DontDestroyOnLoad(gameObject);

        // After each scene loads, I reset the bubble so it’s ready next time.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetBubble();
        isTransitioning = false;
    }

    public void TransitionToScene(string sceneName)
    {
        if (isTransitioning)
        {
            Debug.Log("[SimpleSceneTransition] Transition already in progress.");
            return;
        }

        targetSceneName = sceneName;
        isTransitioning = true;

        if (bubbleAudioSource != null)
            bubbleAudioSource.Play();

        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        float movedDistance = 0f;
        float riseTime = (moveSpeed > 0f) ? (riseDistance / moveSpeed) : 0f;

        // Move bubble upwards until target distance is reached.
        while (movedDistance < riseDistance)
        {
            float step = moveSpeed * Time.unscaledDeltaTime;
            if (bubbleSprite != null)
            {
                bubbleSprite.position += new Vector3(0f, step, 0f);
            }
            movedDistance += step;
            yield return null;
        }

        // Ensure total delay target is respected (but never negative).
        float extraWait = Mathf.Max(0f, delayBeforeLoad - riseTime);
        if (extraWait > 0f)
            yield return new WaitForSecondsRealtime(extraWait);

        SceneManager.LoadScene(targetSceneName);
    }

    public void ResetBubble()
    {
        if (bubbleSprite != null)
            bubbleSprite.position = startPosition;
    }
}
