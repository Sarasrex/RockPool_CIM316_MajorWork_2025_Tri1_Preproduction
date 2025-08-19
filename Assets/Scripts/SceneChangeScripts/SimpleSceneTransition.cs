using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneTransition : MonoBehaviour
{
    [Header("Visual")]
    public Transform bubbleSprite;
    public float moveSpeed = 500f;
    public float riseDistance = 1500f;

    [Header("Timing")]
    [Tooltip("Target total time from trigger to load. If <= (riseDistance/moveSpeed), I skip extra waiting.")]
    public float delayBeforeLoad = 1f;

    [Header("Audio")]
    public AudioSource bubbleAudioSource;

    private Vector3 startPosition;
    private string targetSceneName;
    private bool isTransitioning;
    private Coroutine transitionCo;

    // Safety: if something goes wrong, we won’t be stuck forever
    private const float HARD_TIMEOUT = 6f;

    private void Awake()
    {
        if (bubbleSprite != null)
            startPosition = bubbleSprite.position;

        //DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        // Safety reset in case we re-enable a stale instance
        isTransitioning = false;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetBubble();
        isTransitioning = false;
        transitionCo = null;
    }

    public void TransitionToScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("[SimpleSceneTransition] Empty scene name.");
            return;
        }

        if (isTransitioning)
        {
            Debug.LogWarning("[SimpleSceneTransition] Transition already in progress; forcing direct load fallback.");
            ForceDirectLoad(sceneName);
            return;
        }

        targetSceneName = sceneName;
        isTransitioning = true;

        if (bubbleAudioSource) bubbleAudioSource.Play();

        transitionCo = StartCoroutine(PlayTransition());
        StartCoroutine(HardTimeoutWatchdog());
    }

    private IEnumerator PlayTransition()
    {
        float moved = 0f;
        float riseTime = (moveSpeed > 0f) ? (riseDistance / moveSpeed) : 0f;

        while (moved < riseDistance)
        {
            float step = moveSpeed * Time.unscaledDeltaTime;
            if (bubbleSprite) bubbleSprite.position += new Vector3(0f, step, 0f);
            moved += step;
            yield return null;
        }

        float extraWait = Mathf.Max(0f, delayBeforeLoad - riseTime);
        if (extraWait > 0f) yield return new WaitForSecondsRealtime(extraWait);

        // Normalise time, just in case
        Time.timeScale = 1f;
        SceneManager.LoadScene(targetSceneName);
    }

    private IEnumerator HardTimeoutWatchdog()
    {
        float t = 0f;
        while (t < HARD_TIMEOUT && isTransitioning) { t += Time.unscaledDeltaTime; yield return null; }
        if (isTransitioning)
        {
            Debug.LogWarning("[SimpleSceneTransition] Timeout; forcing direct load.");
            ForceDirectLoad(targetSceneName);
        }
    }

    public void ResetBubble()
    {
        if (bubbleSprite) bubbleSprite.position = startPosition;
    }

    private void ForceDirectLoad(string sceneName)
    {
        isTransitioning = false;
        transitionCo = null;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
