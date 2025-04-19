using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Shared Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public AudioSource audioSource;
    public Animator characterAnimator;

    [Header("Treasure (Home) Icons")]
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
    public GameObject inventoryPanel;
    public GameObject compassPanel;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Turn off UI panels at runtime instead of in editor
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (compassPanel != null) compassPanel.SetActive(false);
    }


    // Show collected item icons based on category
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

    // Legacy fallback (for global use if needed)
    public void ShowDialogue(string speakerName, DialogueLine line)
    {
        dialoguePanel.SetActive(true);
        nameText.text = speakerName;
        dialogueText.text = line.text;

        if (line.audioClip != null && audioSource != null)
            audioSource.PlayOneShot(line.audioClip);

        if (!string.IsNullOrEmpty(line.animationTrigger) && characterAnimator != null)
            characterAnimator.SetTrigger(line.animationTrigger);

        CancelInvoke(nameof(HideDialogue));
        Invoke(nameof(HideDialogue), 3.5f);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
