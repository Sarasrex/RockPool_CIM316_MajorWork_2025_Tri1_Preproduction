using UnityEngine;
using UnityEngine.SceneManagement;

/// Centralised scene loader. Uses my SimpleSceneTransition if it exists.
/// I keep one of these alive across scenes so anything can call SceneSwitcher.Load("SceneName").
public class SceneSwitcher : MonoBehaviour
{
    private static SceneSwitcher _instance;
    private SimpleSceneTransition transitionManager;

    private void Awake()
    {
        // Ensure only one lives across scenes.
        if (_instance != null && _instance != this) { Destroy(gameObject); return; }
        _instance = this;
        //DontDestroyOnLoad(gameObject);

        // Try finding the transition manager once at boot; if I add one later, I'll refresh it on demand.
        transitionManager = FindObjectOfType<SimpleSceneTransition>();
    }

    /// For UI OnClick: I can drag-drop this and pass a scene name in the event.
    public void LoadSceneByName(string sceneName)
    {
        Load(sceneName);
    }

    /// Static convenience: from any script I can call SceneSwitcher.Load("Map")
    public static void Load(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("[SceneSwitcher] Scene name was empty.");
            return;
        }

        // Try to use the global instance + transition if present.
        if (_instance != null)
        {
            // If I didn't have a transition at Awake, try to find one now (e.g., if I spawned it later).
            if (_instance.transitionManager == null)
                _instance.transitionManager = FindObjectOfType<SimpleSceneTransition>();

            if (_instance.transitionManager != null)
            {
                _instance.transitionManager.TransitionToScene(sceneName);
                return;
            }
        }

        // Fallback: direct load if no transition exists.
        SceneManager.LoadScene(sceneName);
    }
}
