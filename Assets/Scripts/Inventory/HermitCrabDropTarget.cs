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

    [Header("Audio Clips")]
    public AudioClip positiveAudioClip;
    public AudioClip negativeAudioClip;
    public AudioClip munchAudioClip;
    public AudioClip sleepAudioClip;
    public AudioClip helloAudioClip;

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

    // Inactivity handling
    private float lastInteractionTime;
    private bool isSleeping = false;

    void Awake()
    {
        lastInteractionTime = Time.time;
    }

    void Update()
    {
        // If no interaction for over 60 seconds, trigger sleeping
        if (!isSleeping && Time.time - lastInteractionTime > 60f)
        {
            TriggerSleepingReaction();
            isSleeping = true;
        }
    }

    // Called when an item is dropped onto the hermit crab
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

        // Check preferences and adjust happiness
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

        // Play general sound feedback
        if (audioSource != null)
        {
            if (liked && positiveAudioClip != null) audioSource.PlayOneShot(positiveAudioClip);
            else if (disliked && negativeAudioClip != null) audioSource.PlayOneShot(negativeAudioClip);
        }

        // Reset sleep timer
        lastInteractionTime = Time.time;
        isSleeping = false;

        // If liked food, trigger munch animation, sound, and dialogue
        if (liked && itemCategory == "Food")
        {
            if (reactionController != null)
                reactionController.PlayReaction("Munch");

            if (audioSource != null && munchAudioClip != null)
                audioSource.PlayOneShot(munchAudioClip);

            HermitDialogue dialogue = GetComponent<HermitDialogue>();
            if (dialogue != null)
            {
                DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Munching);
                if (line != null && speechBubble != null && bubbleText != null)
                {
                    bubbleText.text = line.text;
                    speechBubble.SetActive(true);

                    if (audioSource != null && line.audioClip != null)
                        audioSource.PlayOneShot(line.audioClip);

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }
        }

        // Update the community happiness system
        if (CommunityCompassManager.Instance != null)
            CommunityCompassManager.Instance.UpdateCommunityHappiness();

        // Change shell sprite if liked home item
        if (itemCategory == "Home" && liked)
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

        // Handle agreement/disapproval dialogue for non-food items
        if (itemCategory != "Food")
        {
            DialogueTriggerType trigger = liked ? DialogueTriggerType.InAgreement : DialogueTriggerType.Disapproves;

            HermitDialogue dialogue = GetComponent<HermitDialogue>();
            if (dialogue != null)
            {
                DialogueLine line = dialogue.GetRandomLineByTrigger(trigger);
                if (line != null && speechBubble != null && bubbleText != null)
                {
                    bubbleText.text = line.text;
                    speechBubble.SetActive(true);

                    if (audioSource != null && line.audioClip != null)
                        audioSource.PlayOneShot(line.audioClip);

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }
        }
    }

    // Triggered automatically after 60s of inactivity
    void TriggerSleepingReaction()
    {
        if (reactionController != null)
            reactionController.PlayReaction("Sleeping");

        if (audioSource != null && sleepAudioClip != null)
            audioSource.PlayOneShot(sleepAudioClip);

        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue != null)
        {
            DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Sleeping);
            if (line != null && speechBubble != null && bubbleText != null)
            {
                bubbleText.text = line.text;
                speechBubble.SetActive(true);

                if (audioSource != null && line.audioClip != null)
                    audioSource.PlayOneShot(line.audioClip);

                CancelInvoke(nameof(HideBubble));
                Invoke(nameof(HideBubble), 8f);
            }
        }

        Debug.Log($"[{hermitName}] fell asleep due to inactivity.");
    }

    // Optional method to trigger a "Hello" greeting manually
    public void TriggerHello()
    {
        if (reactionController != null)
            reactionController.PlayReaction("Hello");

        if (audioSource != null && helloAudioClip != null)
            audioSource.PlayOneShot(helloAudioClip);

        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue != null)
        {
            DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Hello);
            if (line != null && speechBubble != null && bubbleText != null)
            {
                bubbleText.text = line.text;
                speechBubble.SetActive(true);

                if (audioSource != null && line.audioClip != null)
                    audioSource.PlayOneShot(line.audioClip);

                CancelInvoke(nameof(HideBubble));
                Invoke(nameof(HideBubble), 8f);
            }
        }
    }

    // Hides the dialogue bubble after a short delay
    void HideBubble()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }
}
