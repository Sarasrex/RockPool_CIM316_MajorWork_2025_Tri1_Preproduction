using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central reaction controller to trigger Animator-based reactions (like munching, sleeping, etc).
/// </summary>
public class HermitReactionController : MonoBehaviour
{
    [Header("Core Visuals")]
    public GameObject bodySprite; // Optional: used if you want to swap visuals manually later

    [Header("Animator")]
    public Animator hermitAnimator;  // Reference to the crab's Animator

    public void PlayReaction(string reactionName)
    {
        if (hermitAnimator == null)
        {
            Debug.LogWarning($"[HermitReactionController] No Animator assigned on {gameObject.name}.");
            return;
        }

        // Use Animator trigger
        hermitAnimator.SetTrigger(reactionName);
        Debug.Log($"[HermitReactionController] Triggered animation: {reactionName}");
    }
}
