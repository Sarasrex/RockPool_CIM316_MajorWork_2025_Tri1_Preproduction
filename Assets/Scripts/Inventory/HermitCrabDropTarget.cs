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

    [Header("Audio Sources")]
    public AudioSource positiveAudioSource;
    public AudioSource negativeAudioSource;
    public AudioSource munchAudioSource;
    public AudioSource sleepAudioSource;
    public AudioSource helloAudioSource;

    [Header("Hermit Info")]
    public string hermitName;
    public string acceptedCategory;

    [Header("Preferences")]
    public string[] likedFoods;
    public string[] dislikedFoods;
    public string[] likedHomes;
    public string[] dislikedHomes;

    [Header("Sleep Settings")]
    [SerializeField] private float sleepTextInterval = 20f;
    private Coroutine sleepDialogueCoroutine;

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

    private float lastInteractionTime;
    private bool isSleeping = false;

    void Awake()
    {
        lastInteractionTime = Time.time;
    }

    void Update()
    {
        if (!isSleeping && Time.time - lastInteractionTime > 60f)
        {
            TriggerSleepingReaction();
            isSleeping = true;
        }
    }

    public void ReceiveItem(string itemName, string itemCategory)
    {
        if (!string.IsNullOrEmpty(acceptedCategory) && acceptedCategory != itemCategory)
            return;

        Debug.Log("[" + hermitName + "] received '" + itemName + "' (" + itemCategory + ")");

        int delta = 0;
        bool liked = false;
        bool disliked = false;

        InventoryManager.Instance.UseItem(itemName, itemCategory);

        lastInteractionTime = Time.time;
        SetCrabSleepState(false);


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

        if (sleepDialogueCoroutine != null)
        {
            StopCoroutine(sleepDialogueCoroutine);
            sleepDialogueCoroutine = null;
        }



        if (liked && positiveAudioSource != null)
        {
            Debug.Log("Playing positive sound for: " + hermitName);
            positiveAudioSource.Play();
        }
        else if (disliked && negativeAudioSource != null)
        {
            Debug.Log("Playing negative sound for: " + hermitName);
            negativeAudioSource.Play();
        }

        if (liked && itemCategory == "Food")
        {
            if (reactionController != null)
                reactionController.PlayReaction("Munch");

            if (munchAudioSource != null)
                munchAudioSource.Play();

            HermitDialogue dialogue = GetComponent<HermitDialogue>();
            if (dialogue != null)
            {
                DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Munching);
                if (line != null && speechBubble != null && bubbleText != null)
                {
                    bubbleText.text = line.text;
                    speechBubble.SetActive(true);

                    if (line.audioSource != null)
                        line.audioSource.Play();

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }
        }

        if (liked && itemCategory == "Home")
        {
            if (reactionController != null)
                reactionController.PlayReaction("Excited"); // <-- Replace with your actual reaction name, like "Cheer", "HomeSwap", etc.

            HermitDialogue dialogue = GetComponent<HermitDialogue>();
            if (dialogue != null)
            {
                DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.InAgreement);
                if (line != null && speechBubble != null && bubbleText != null)
                {
                    bubbleText.text = line.text;
                    speechBubble.SetActive(true);

                    if (line.audioSource != null)
                        line.audioSource.Play();

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }
        }

        if (disliked && itemCategory == "Food")
        {
            HermitDialogue dialogue = GetComponent<HermitDialogue>();
            if (dialogue != null)
            {
                DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Disapproves);
                if (line != null && speechBubble != null && bubbleText != null)
                {
                    bubbleText.text = line.text;
                    speechBubble.SetActive(true);

                    if (line.audioSource != null)
                        line.audioSource.Play();

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }
        }

        if (CommunityCompassManager.Instance != null)
            CommunityCompassManager.Instance.UpdateCommunityHappiness();

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

                    if (line.audioSource != null)
                        line.audioSource.Play();

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }
        }
    }

    void TriggerSleepingReaction()
    {
        if (reactionController != null)
            reactionController.PlayReaction("Sleeping");

        if (sleepAudioSource != null)
            sleepAudioSource.Play();

        if (sleepDialogueCoroutine == null)
            sleepDialogueCoroutine = StartCoroutine(SleepDialogueLoop());

        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue != null)
        {
            DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Sleeping);
            if (line != null && speechBubble != null && bubbleText != null)
            {
                bubbleText.text = line.text;
                speechBubble.SetActive(true);

                if (line.audioSource != null)
                    line.audioSource.Play();

                CancelInvoke(nameof(HideBubble));
                Invoke(nameof(HideBubble), 8f);
                SetCrabSleepState(true);
            }
        }

        Debug.Log($"[{hermitName}] fell asleep due to inactivity.");
    }

    private IEnumerator SleepDialogueLoop()
    {
        yield return new WaitForSeconds(sleepTextInterval);

        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        while (isSleeping)
        {
            if (dialogue != null)
            {
                DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Sleeping);
                if (line != null && speechBubble != null && bubbleText != null)
                {
                    bubbleText.text = line.text;
                    speechBubble.SetActive(true);

                    if (line.audioSource != null)
                        line.audioSource.Play();

                    CancelInvoke(nameof(HideBubble));
                    Invoke(nameof(HideBubble), 8f);
                }
            }

            yield return new WaitForSeconds(sleepTextInterval);
        }
    }

    public void TriggerHello()
    {
        if (reactionController != null)
            reactionController.PlayReaction("Hello");

        if (helloAudioSource != null)
            helloAudioSource.Play();

        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue != null)
        {
            DialogueLine line = dialogue.GetRandomLineByTrigger(DialogueTriggerType.Hello);
            if (line != null && speechBubble != null && bubbleText != null)
            {
                bubbleText.text = line.text;
                speechBubble.SetActive(true);

                if (line.audioSource != null)
                    line.audioSource.Play();

                CancelInvoke(nameof(HideBubble));
                Invoke(nameof(HideBubble), 8f);
            }
        }
    }

    void HideBubble()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
    }

    void SetCrabSleepState(bool asleep)
    {
        isSleeping = asleep;

        CharacterRoaming roaming = GetComponent<CharacterRoaming>();
        if (roaming != null)
            roaming.isAsleep = asleep;

        if (!asleep && reactionController != null)
        {
            // Tell animator to play WakeUp
            reactionController.PlayReaction("WakeUp");
        }
    }

}
