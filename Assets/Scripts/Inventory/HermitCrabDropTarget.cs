using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script handles what happens when a hermit crab receives a dropped item.
// It adjusts happiness, plays audio feedback, shows dialogue, and updates the community compass.

public class HermitCrabDropTarget : MonoBehaviour
{
    [Header("Speech Bubble References")]
    public GameObject speechBubble;       // Speech bubble GameObject shown on interaction
    public TMP_Text bubbleText;           // Text component for dialogue
    public AudioSource audioSource;       // Audio source for dialogue and feedback sounds

    public AudioClip positiveAudioClip;   // Audio clip when hermit likes the item
    public AudioClip negativeAudioClip;   // Audio clip when hermit dislikes the item

    public string hermitName;             // Used for debugging and logs
    public string acceptedCategory;       // Restricts item type (e.g., "Food", "Home") if set

    [Header("Preferences")]
    public string[] likedFoods;
    public string[] dislikedFoods;
    public string[] likedHomes;
    public string[] dislikedHomes;

    [Header("Happiness")]
    [Range(0, 100)] public float happiness = 0f; // Current happiness (0–100 range)

    // Sets default happiness (can be replaced with saved data later)
    public void Awake()
    {
        happiness = 0f;
    }

    // Called when an item is dropped on this hermit
    public void ReceiveItem(string itemName, string itemCategory)
    {
        // Reject items that don't match this hermit's category preference
        if (!string.IsNullOrEmpty(acceptedCategory) && acceptedCategory != itemCategory)
            return;

        // Log preferences and item for debugging
        Debug.Log("[" + hermitName + "] received '" + itemName + "' (" + itemCategory + ")");
        Debug.Log("[" + hermitName + "] Liked Foods: " + string.Join(", ", likedFoods));
        Debug.Log("[" + hermitName + "] Disliked Foods: " + string.Join(", ", dislikedFoods));
        Debug.Log("[" + hermitName + "] Liked Homes: " + string.Join(", ", likedHomes));
        Debug.Log("[" + hermitName + "] Disliked Homes: " + string.Join(", ", dislikedHomes));

        int delta = 0;
        bool liked = false;
        bool disliked = false;

        // Check food preferences
        if (itemCategory == "Food")
        {
            if (System.Array.Exists(likedFoods, item => item == itemName))
            {
                delta = 15;
                liked = true;
            }
            else if (System.Array.Exists(dislikedFoods, item => item == itemName))
            {
                delta = -15;
                disliked = true;
            }
        }
        // Check home preferences
        else if (itemCategory == "Home")
        {
            if (System.Array.Exists(likedHomes, item => item == itemName))
            {
                delta = 20;
                liked = true;
            }
            else if (System.Array.Exists(dislikedHomes, item => item == itemName))
            {
                delta = -20;
                disliked = true;
            }
        }

        // Apply the change and clamp within range
        Debug.Log("[" + hermitName + "] happiness changed by " + delta);
        happiness = Mathf.Clamp(happiness + delta, 0, 100);

        // Play feedback audio
        if (audioSource != null)
        {
            if (liked && positiveAudioClip != null)
                audioSource.PlayOneShot(positiveAudioClip);
            else if (disliked && negativeAudioClip != null)
                audioSource.PlayOneShot(negativeAudioClip);
        }

        // IMPORTANT: Update the community compass after happiness changes
        if (CommunityCompassManager.Instance != null)
        {
            CommunityCompassManager.Instance.UpdateCommunityHappiness();
        }

        // Determine emotional response type
        DialogueTriggerType trigger;
        if (System.Array.Exists(likedFoods, item => item == itemName) ||
            System.Array.Exists(likedHomes, item => item == itemName))
        {
            trigger = DialogueTriggerType.InAgreement;
        }
        else if (System.Array.Exists(dislikedFoods, item => item == itemName) ||
                 System.Array.Exists(dislikedHomes, item => item == itemName))
        {
            trigger = DialogueTriggerType.Disapproves;
        }
        else
        {
            trigger = DialogueTriggerType.Munching;
        }

        // Retrieve matching dialogue line
        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue == null)
        {
            Debug.LogError("[" + hermitName + "] Missing HermitDialogue component.");
            return;
        }

        DialogueLine line = dialogue.GetRandomLineByTrigger(trigger);
        if (line == null)
        {
            Debug.LogError("[" + hermitName + "] DialogueLine is null for trigger: " + trigger);
            return;
        }

        if (string.IsNullOrEmpty(line.text))
        {
            Debug.LogError("[" + hermitName + "] DialogueLine text is null or empty.");
            return;
        }

        if (bubbleText == null || speechBubble == null)
        {
            Debug.LogError("[" + hermitName + "] bubbleText or speechBubble is not assigned.");
            return;
        }

        // Show speech bubble with chosen line
        Debug.Log("[" + hermitName + "] Showing speech bubble: " + line.text);
        bubbleText.text = line.text;
        speechBubble.SetActive(true);

        // Play line-specific audio clip if available
        if (audioSource != null && line.audioClip != null)
        {
            audioSource.PlayOneShot(line.audioClip);
        }

        // Hide the speech bubble after a few seconds
        CancelInvoke(nameof(HideBubble));
        Invoke(nameof(HideBubble), 3.5f);
    }

    // Hides the speech bubble
    private void HideBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }
    }
}
