using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneTransition : MonoBehaviour
{
    public Transform bubbleSprite;
    public float moveSpeed = 500f;
    public float riseDistance = 1500f;
    public float delayBeforeLoad = 1.5f;

    private Vector3 startPosition; 
    private string targetSceneName;

    public AudioSource bubbleAudioSource;

    private void Start()
    {
        if (bubbleSprite != null)
        {
            startPosition = bubbleSprite.position; 
        }
    }

    public void TransitionToScene(string sceneName)
    {
        targetSceneName = sceneName;

        if (bubbleAudioSource != null)
        {
            bubbleAudioSource.Play();
        }

        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        float movedDistance = 0f;

        while (movedDistance < riseDistance)
        {
            float moveStep = moveSpeed * Time.deltaTime;
            bubbleSprite.position += new Vector3(0, moveStep, 0); 
            movedDistance += moveStep;
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeLoad - (riseDistance / moveSpeed));
        SceneManager.LoadScene(targetSceneName);
    }

    public void ResetBubble()
    {
        if (bubbleSprite != null)
        {
            bubbleSprite.position = startPosition; 
        }
    }
}
