using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public TMP_Text goldCoinHomeCountText;

    public GameObject mirrorShellHomeIcon;
    public TMP_Text mirrorShellHomeCountText;

    public GameObject boardNoseHomeIcon;
    public TMP_Text boardNoseHomeCountText;

    public GameObject rockBowlHomeIcon;
    public TMP_Text rockBowlHomeCountText;

    public GameObject snorkelHomeIcon;
    public TMP_Text snorkelHomeCountText;

    public GameObject shellPurseHomeIcon;
    public TMP_Text shellPurseHomeCountText;

    public GameObject lostCompassHomeIcon;
    public TMP_Text lostCompassHomeCountText;

    public GameObject charStickHomeIcon;
    public TMP_Text charStickHomeCountText;

    public GameObject coralSpoonHomeIcon;
    public TMP_Text coralSpoonHomeCountText;

    public GameObject stoneStackHomeIcon;
    public TMP_Text stoneStackHomeCountText;

    public GameObject glassHeartHomeIcon;
    public TMP_Text glassHeartHomeCountText;

    public GameObject usbStickHomeIcon;
    public TMP_Text usbStickHomeCountText;

    [Header("Food Icons")]
    public GameObject seaweedSnarlRollIcon;
    public TMP_Text seaweedSnarlRollCountText;

    public GameObject squidlyChewIcon;
    public TMP_Text squidlyChewCountText;

    public GameObject driftChipDipIcon;
    public TMP_Text driftChipDipCountText;

    public GameObject seaSaladIcon;
    public TMP_Text seaSaladCountText;

    public GameObject jellyWiggleIcon;
    public TMP_Text jellyWiggleCountText;

    public GameObject bubbleBurstIcon;
    public TMP_Text bubbleBurstCountText;

    public GameObject beachBananaIcon;
    public TMP_Text beachBananaCountText;

    public GameObject crunchMunchIcon;
    public TMP_Text crunchMunchCountText;

    public GameObject bubbleBounceIcon;
    public TMP_Text bubbleBounceCountText;

    public GameObject noriNibbleIcon;
    public TMP_Text noriNibbleCountText;

    public GameObject snailSlurpIcon;
    public TMP_Text snailSlurpCountText;

    public GameObject plumDropIcon;
    public TMP_Text plumDropCountText;

    public GameObject starsIcon;
    public TMP_Text starsCountText;

    [Header("Initial UI Panels")]
    public GameObject inventoryPanel;
    public GameObject compassPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (compassPanel != null) compassPanel.SetActive(false);
    }

    // Unified method to show and manage item state
    public void ShowItem(string itemName, string itemCategory, int count)
    {
        GameObject itemIcon = null;
        TMP_Text countText = null;

        switch (itemCategory)
        {
            case "Home":
                if (itemName == "GoldCoinHome") { itemIcon = goldCoinHomeIcon; countText = goldCoinHomeCountText; }
                if (itemName == "MirrorShellHome") { itemIcon = mirrorShellHomeIcon; countText = mirrorShellHomeCountText; }
                if (itemName == "BoardNoseHome") { itemIcon = boardNoseHomeIcon; countText = boardNoseHomeCountText; }
                if (itemName == "RockBowlHome") { itemIcon = rockBowlHomeIcon; countText = rockBowlHomeCountText; }
                if (itemName == "SnorkelHome") { itemIcon = snorkelHomeIcon; countText = snorkelHomeCountText; }
                if (itemName == "ShellPurseHome") { itemIcon = shellPurseHomeIcon; countText = shellPurseHomeCountText; }
                if (itemName == "LostCompassHome") { itemIcon = lostCompassHomeIcon; countText = lostCompassHomeCountText; }
                if (itemName == "CharStickHome") { itemIcon = charStickHomeIcon; countText = charStickHomeCountText; }
                if (itemName == "CoralSpoonHome") { itemIcon = coralSpoonHomeIcon; countText = coralSpoonHomeCountText; }
                if (itemName == "StoneStackHome") { itemIcon = stoneStackHomeIcon; countText = stoneStackHomeCountText; }
                if (itemName == "GlassHeartHome") { itemIcon = glassHeartHomeIcon; countText = glassHeartHomeCountText; }
                if (itemName == "UsbStickHome") { itemIcon = usbStickHomeIcon; countText = usbStickHomeCountText; }
                break;

            case "Food":
                if (itemName == "SeaweedSnarlRoll") { itemIcon = seaweedSnarlRollIcon; countText = seaweedSnarlRollCountText; }
                if (itemName == "SquidlyChew") { itemIcon = squidlyChewIcon; countText = squidlyChewCountText; }
                if (itemName == "DriftChipDip") { itemIcon = driftChipDipIcon; countText = driftChipDipCountText; }
                if (itemName == "SeaSalad") { itemIcon = seaSaladIcon; countText = seaSaladCountText; }
                if (itemName == "JellyWiggle") { itemIcon = jellyWiggleIcon; countText = jellyWiggleCountText; }
                if (itemName == "BubbleBurst") { itemIcon = bubbleBurstIcon; countText = bubbleBurstCountText; }
                if (itemName == "BeachBanana") { itemIcon = beachBananaIcon; countText = beachBananaCountText; }
                if (itemName == "CrunchMunch") { itemIcon = crunchMunchIcon; countText = crunchMunchCountText; }
                if (itemName == "BubbleBounce") { itemIcon = bubbleBounceIcon; countText = bubbleBounceCountText; }
                if (itemName == "NoriNibble") { itemIcon = noriNibbleIcon; countText = noriNibbleCountText; }
                if (itemName == "SnailSlurp") { itemIcon = snailSlurpIcon; countText = snailSlurpCountText; }
                if (itemName == "PlumDrop") { itemIcon = plumDropIcon; countText = plumDropCountText; }
                if (itemName == "Stars") { itemIcon = starsIcon; countText = starsCountText; }
                break;
        }

        if (itemIcon != null)
        {
            itemIcon.SetActive(true);
            if (countText != null)
                countText.text = count.ToString();

            // Apply visual and interaction feedback
            Image iconImage = itemIcon.GetComponent<Image>();
            DraggableItem draggable = itemIcon.GetComponent<DraggableItem>();

            if (iconImage != null)
                iconImage.color = new Color(1f, 1f, 1f, count <= 0 ? 0.4f : 1f);

            if (draggable != null)
                draggable.enabled = count > 0;
        }
    }

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
