using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central reaction controller to trigger Animator-based reactions (like munching, sleeping, etc).
/// Starts the snore loop on "Sleeping" and stops it on "WakeUp".
/// </summary>
public class HermitReactionController : MonoBehaviour
{
    [Header("Core Visuals")]
    public GameObject bodySprite; // Optional: used if you want to swap visuals manually later

    [Header("Animator")]
    public Animator hermitAnimator;  // Reference to the crab's Animator

    [Header("Snore Audio")]
    [Tooltip("Assign the looping snore AudioSource here (the one that's currently looping).")]
    public AudioSource snoreLoopSource;

    [Header("Animator Trigger Names")]
    [Tooltip("Trigger name used in the Animator when the crab goes to sleep.")]
    public string sleepTriggerName = "Sleeping";

    [Tooltip("Trigger name used in the Animator when the crab wakes up (stops snoring).")]
    public string wakeTriggerName = "WakeUp";

    public void PlayReaction(string reactionName)
    {
        if (hermitAnimator == null)
        {
            Debug.LogWarning($"[HermitReactionController] No Animator assigned on {gameObject.name}.");
            return;
        }

        // Fire the animator trigger
        hermitAnimator.SetTrigger(reactionName);
        Debug.Log($"[HermitReactionController] Triggered animation: {reactionName}");

        // Start snore when entering Sleeping
        if (!string.IsNullOrEmpty(sleepTriggerName) &&
            reactionName == sleepTriggerName &&
            snoreLoopSource != null &&
            !snoreLoopSource.isPlaying)
        {
            snoreLoopSource.loop = true; // ensure loop is on
            if (snoreLoopSource.clip == null)
                Debug.LogWarning("[HermitReactionController] Snore AudioSource has no clip assigned.");
            snoreLoopSource.Play();
            Debug.Log("[HermitReactionController] Started snore loop.");
        }

        // Stop snore when waking up
        if (!string.IsNullOrEmpty(wakeTriggerName) &&
            reactionName == wakeTriggerName &&
            snoreLoopSource != null)
        {
            snoreLoopSource.Stop();
            Debug.Log("[HermitReactionController] Stopped snore loop.");
        }
    }
}
