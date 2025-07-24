using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles visual reactions like munching, talking, sleeping, bumping, etc. by toggling body and reaction sprites.
/// </summary>
public class HermitReactionController : MonoBehaviour
{
    [Header("Core Visuals")]
    public GameObject bodySprite; // Default idle sprite GameObject

    [System.Serializable]
    public class Reaction
    {
        public string reactionName;         // e.g. "Munch"
        public GameObject reactionObject;   // GameObject with the reaction animation
        public float duration = 2f;         // Duration to show it
    }

    [Header("Reaction Library")]
    public List<Reaction> reactions = new List<Reaction>();

    private Coroutine currentReactionCoroutine;

    public void PlayReaction(string reactionName)
    {
        // Find the matching reaction
        Reaction match = reactions.Find(r => r.reactionName == reactionName);

        if (match != null && match.reactionObject != null)
        {
            if (currentReactionCoroutine != null)
                StopCoroutine(currentReactionCoroutine);

            currentReactionCoroutine = StartCoroutine(PlayReactionRoutine(match));
        }
        else
        {
            Debug.LogWarning($"[HermitReactionController] Reaction '{reactionName}' not found or not assigned.");
        }
    }

    private IEnumerator PlayReactionRoutine(Reaction reaction)
    {
        Debug.Log($"[HermitReactionController] Playing reaction: {reaction.reactionName}");

        if (bodySprite != null)
        {
            bodySprite.SetActive(false);
        }

        reaction.reactionObject.SetActive(true);

        yield return new WaitForSeconds(reaction.duration);

        reaction.reactionObject.SetActive(false);

        if (bodySprite != null)
        {
            bodySprite.SetActive(true);
        }

        currentReactionCoroutine = null;
    }
}
