using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Handles all major UI updates, including:
// - Dialogue display
// - Item collection icons
// - Panel visibility for inventory and compass
public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton reference to access UIManager globally

    [Header("Shared Dialogue UI")]
    public GameObject dialoguePanel;          // The whole dialogue box
    public TMP_Text nameText;                 // Name of the speaking character
    public TMP_Text dialogueText;             // Their spoken text
    public AudioSource audioSource;           // Audio for dialogue clips
    public Animator characterAnimator;        // Character animation triggers

    [Header("Treasure (Home) Icons")]
    // These icons represent Home items and are activated when collected
    public GameObject goldCoinHomeIcon;
    public GameObject mirrorShellHomeIcon;
    public GameObject boardNoseHomeIcon;
    public GameObject rockBowlHomeIcon;
    public GameObject snorkelHomeIcon;
    public GameObject shellPurseHomeIcon;
    public GameObject lostCompassHomeIcon;
    public GameObject charStickHomeIcon;
    public GameObject coralSpoonHomeIcon;
    public GameObject stoneStackHomeIcon;
    public GameObject glassHeartHomeIcon;
    public GameObject usbStickHomeIcon;

    [Header("Food Icons")]
    // These icons represent Food items and are activated when collected
    public GameObject seaweedSnarlRollIcon;
    public GameObject squidlyChewIcon;
    public GameObject driftChipDipIcon;
    public GameObject seaSaladIcon;
    public GameObject jellyWiggleIcon;
    public GameObject bubbleBurstIcon;
    public GameObject beachBananaIcon;
    public GameObject crunchMunchIcon;
    public GameObject bubbleBounceIcon;
    public GameObject noriNibbleIcon;
    public GameObject snailSlurpIcon;
    public GameObject plumDropIcon;
    public GameObject starsIcon;

    [Header("Initial UI Panels")]
    public GameObject inventoryPanel;         // Inventory UI panel
    public GameObject compassPanel;           // Compass UI panel

    // Initialise singleton and hide default UI panels at game start
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Hide panels on start (assumes editor might have them enabled for layout)
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (compassPanel != null) compassPanel.SetActive(false);
    }

    // Turns on the UI icon matching a collected item
    public void ShowItem(string itemName, string itemCategory)
    {
        switch (itemCategory)
        {
            case "Home":
                if (itemName == "GoldCoinHome") goldCoinHomeIcon.SetActive(true);
                if (itemName == "MirrorShellHome") mirrorShellHomeIcon.SetActive(true);
                if (itemName == "BoardNoseHome") boardNoseHomeIcon.SetActive(true);
                if (itemName == "RockBowlHome") rockBowlHomeIcon.SetActive(true);
                if (itemName == "SnorkelHome") snorkelHomeIcon.SetActive(true);
                if (itemName == "ShellPurseHome") shellPurseHomeIcon.SetActive(true);
                if (itemName == "LostCompassHome") lostCompassHomeIcon.SetActive(true);
                if (itemName == "CharStickHome") charStickHomeIcon.SetActive(true);
                if (itemName == "CoralSpoonHome") coralSpoonHomeIcon.SetActive(true);
                if (itemName == "StoneStackHome") stoneStackHomeIcon.SetActive(true);
                if (itemName == "GlassHeartHome") glassHeartHomeIcon.SetActive(true);
                if (itemName == "UsbStickHome") usbStickHomeIcon.SetActive(true);
                break;

            case "Food":
                if (itemName == "SeaweedSnarlRoll") seaweedSnarlRollIcon.SetActive(true);
                if (itemName == "SquidlyChew") squidlyChewIcon.SetActive(true);
                if (itemName == "DriftChipDip") driftChipDipIcon.SetActive(true);
                if (itemName == "SeaSalad") seaSaladIcon.SetActive(true);
                if (itemName == "JellyWiggle") jellyWiggleIcon.SetActive(true);
                if (itemName == "BubbleBurst") bubbleBurstIcon.SetActive(true);
                if (itemName == "BeachBanana") beachBananaIcon.SetActive(true);
                if (itemName == "CrunchMunch") crunchMunchIcon.SetActive(true);
                if (itemName == "BubbleBounce") bubbleBounceIcon.SetActive(true);
                if (itemName == "NoriNibble") noriNibbleIcon.SetActive(true);
                if (itemName == "SnailSlurp") snailSlurpIcon.SetActive(true);
                if (itemName == "PlumDrop") plumDropIcon.SetActive(true);
                if (itemName == "Stars") starsIcon.SetActive(true);
                break;
        }
    }

    // Optional global method to show dialogue (may be replaced by individual hermit systems)
    public void ShowDialogue(string speakerName, DialogueLine line)
    {
        dialoguePanel.SetActive(true);
        nameText.text = speakerName;
        dialogueText.text = line.text;

        // Play dialogue audio clip
        if (line.audioClip != null && audioSource != null)
            audioSource.PlayOneShot(line.audioClip);

        // Trigger animation if one is defined
        if (!string.IsNullOrEmpty(line.animationTrigger) && characterAnimator != null)
            characterAnimator.SetTrigger(line.animationTrigger);

        // Hide dialogue automatically after a few seconds
        CancelInvoke(nameof(HideDialogue));
        Invoke(nameof(HideDialogue), 3.5f);
    }

    // Hides the dialogue UI panel
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
