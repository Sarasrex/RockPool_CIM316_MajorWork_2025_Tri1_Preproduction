using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script handles what happens when a hermit crab receives a dropped item.
// It adjusts happiness, plays audio feedback, shows dialogue, updates the community compass,
// swaps home sprites, and triggers visual reaction animations.

public class HermitCrabDropTarget : MonoBehaviour
{
    [Header("Speech Bubble References")]
    public GameObject speechBubble;
    public TMP_Text bubbleText;
    public AudioSource audioSource;

    public AudioClip positiveAudioClip;
    public AudioClip negativeAudioClip;

    [Header("Hermit Info")]
    public string hermitName;
    public string acceptedCategory;

    [Header("Preferences")]
    public string[] likedFoods;
    public string[] dislikedFoods;
    public string[] likedHomes;
    public string[] dislikedHomes;

    [Header("Happiness")]
    [Range(0, 100)] public float happiness = 0f;

    [Header("Sprite Swaps")]
    public SpriteRenderer spriteRenderer;

    [System.Serializable]
    public class HomeSprite
    {
        public string homeName;
        public Sprite newSprite;
    }

    public List<HomeSprite> homeSprites = new List<HomeSprite>();

    [Header("Reaction Controller")]
    public HermitReactionController reactionController;

    void Awake()
    {
        // Optional: Load saved happiness here if using PlayerPrefs later
    }

    public void ReceiveItem(string itemName, string itemCategory)
    {
        if (!string.IsNullOrEmpty(acceptedCategory) && acceptedCategory != itemCategory)
            return;

        Debug.Log("[" + hermitName + "] received '" + itemName + "' (" + itemCategory + ")");

        int delta = 0;
        bool liked = false;
        bool disliked = false;

        // Spend the item from inventory
        InventoryManager.Instance.UseItem(itemName, itemCategory);

        // Preference checks
        if (itemCategory == "Food")
        {
            if (System.Array.Exists(likedFoods, item => item == itemName)) { delta = 15; liked = true; }
            else if (System.Array.Exists(dislikedFoods, item => item == itemName)) { delta = -15; disliked = true; }
        }
        else if (itemCategory == "Home")
        {
            if (System.Array.Exists(likedHomes, item => item == itemName)) { delta = 20; liked = true; }
            else if (System.Array.Exists(dislikedHomes, item => item == itemName)) { delta = -20; disliked = true; }
        }

        happiness = Mathf.Clamp(happiness + delta, 0, 100);
        Debug.Log("[" + hermitName + "] happiness changed by " + delta);

        // Play appropriate sound
        if (audioSource != null)
        {
            if (liked && positiveAudioClip != null) audioSource.PlayOneShot(positiveAudioClip);
            else if (disliked && negativeAudioClip != null) audioSource.PlayOneShot(negativeAudioClip);
        }

        // Play Munch Reaction when liked food is given
        if (liked && itemCategory == "Food" && reactionController != null)
        {
            reactionController.PlayReaction("Munch");
        }

        // Update community happiness
        if (CommunityCompassManager.Instance != null)
            CommunityCompassManager.Instance.UpdateCommunityHappiness();

        // Sprite Change if liked home
        if (itemCategory == "Home")
        {
            if (liked)
            {
                foreach (HomeSprite hs in homeSprites)
                {
                    if (hs.homeName == itemName && hs.newSprite != null)
                    {
                        spriteRenderer.sprite = hs.newSprite;
                        Debug.Log("[" + hermitName + "] Sprite changed to: " + hs.newSprite.name);
                        break;
                    }
                }
            }
        }

        // Determine dialogue trigger
        DialogueTriggerType trigger = DialogueTriggerType.Munching;
        if (liked) trigger = DialogueTriggerType.InAgreement;
        else if (disliked) trigger = DialogueTriggerType.Disapproves;

        // Show Dialogue
        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue == null)
        {
            Debug.LogError("[" + hermitName + "] Missing HermitDialogue component.");
            return;
        }

        DialogueLine line = dialogue.GetRandomLineByTrigger(trigger);
        if (line == null || string.IsNullOrEmpty(line.text))
        {
            Debug.LogError("[" + hermitName + "] DialogueLine is null or empty.");
            return;
        }

        if (bubbleText == null || speechBubble == null)
        {
            Debug.LogError("[" + hermitName + "] bubbleText or speechBubble not assigned.");
            return;
        }

        bubbleText.text = line.text;
        speechBubble.SetActive(true);
        Debug.Log("[" + hermitName + "] Showing speech bubble: " + line.text);

        if (audioSource != null && line.audioClip != null)
            audioSource.PlayOneShot(line.audioClip);

        CancelInvoke(nameof(HideBubble));
        Invoke(nameof(HideBubble), 8f);
    }

    void HideBubble()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }
}
