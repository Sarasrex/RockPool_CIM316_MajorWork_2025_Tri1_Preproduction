using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private SimpleSceneTransition transitionManager;

    private void Start()
    {
        transitionManager = FindObjectOfType<SimpleSceneTransition>();
    }

    public void LoadSceneByName(string sceneName)
    {
        if (transitionManager != null)
        {
            transitionManager.TransitionToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
