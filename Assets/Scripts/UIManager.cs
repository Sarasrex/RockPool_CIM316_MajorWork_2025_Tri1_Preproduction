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

    // Upgraded to support item counts
    public void ShowItem(string itemName, string itemCategory, int count)
    {
        switch (itemCategory)
        {
            case "Home":
                if (itemName == "GoldCoinHome") { goldCoinHomeIcon.SetActive(true); goldCoinHomeCountText.text = "" + count; }
                if (itemName == "MirrorShellHome") { mirrorShellHomeIcon.SetActive(true); mirrorShellHomeCountText.text = "" + count; }
                if (itemName == "BoardNoseHome") { boardNoseHomeIcon.SetActive(true); boardNoseHomeCountText.text = "" + count; }
                if (itemName == "RockBowlHome") { rockBowlHomeIcon.SetActive(true); rockBowlHomeCountText.text = "" + count; }
                if (itemName == "SnorkelHome") { snorkelHomeIcon.SetActive(true); snorkelHomeCountText.text = "" + count; }
                if (itemName == "ShellPurseHome") { shellPurseHomeIcon.SetActive(true); shellPurseHomeCountText.text = "" + count; }
                if (itemName == "LostCompassHome") { lostCompassHomeIcon.SetActive(true); lostCompassHomeCountText.text = "" + count; }
                if (itemName == "CharStickHome") { charStickHomeIcon.SetActive(true); charStickHomeCountText.text = "" + count; }
                if (itemName == "CoralSpoonHome") { coralSpoonHomeIcon.SetActive(true); coralSpoonHomeCountText.text = "" + count; }
                if (itemName == "StoneStackHome") { stoneStackHomeIcon.SetActive(true); stoneStackHomeCountText.text = "" + count; }
                if (itemName == "GlassHeartHome") { glassHeartHomeIcon.SetActive(true); glassHeartHomeCountText.text = "" + count; }
                if (itemName == "UsbStickHome") { usbStickHomeIcon.SetActive(true); usbStickHomeCountText.text = "" + count; }
                break;

            case "Food":
                if (itemName == "SeaweedSnarlRoll") { seaweedSnarlRollIcon.SetActive(true); seaweedSnarlRollCountText.text = "" + count; }
                if (itemName == "SquidlyChew") { squidlyChewIcon.SetActive(true); squidlyChewCountText.text = "" + count; }
                if (itemName == "DriftChipDip") { driftChipDipIcon.SetActive(true); driftChipDipCountText.text = "" + count; }
                if (itemName == "SeaSalad") { seaSaladIcon.SetActive(true); seaSaladCountText.text = "" + count; }
                if (itemName == "JellyWiggle") { jellyWiggleIcon.SetActive(true); jellyWiggleCountText.text = "" + count; }
                if (itemName == "BubbleBurst") { bubbleBurstIcon.SetActive(true); bubbleBurstCountText.text = "" + count; }
                if (itemName == "BeachBanana") { beachBananaIcon.SetActive(true); beachBananaCountText.text = "" + count; }
                if (itemName == "CrunchMunch") { crunchMunchIcon.SetActive(true); crunchMunchCountText.text = "" + count; }
                if (itemName == "BubbleBounce") { bubbleBounceIcon.SetActive(true); bubbleBounceCountText.text = "" + count; }
                if (itemName == "NoriNibble") { noriNibbleIcon.SetActive(true); noriNibbleCountText.text = "" + count; }
                if (itemName == "SnailSlurp") { snailSlurpIcon.SetActive(true); snailSlurpCountText.text = "" + count; }
                if (itemName == "PlumDrop") { plumDropIcon.SetActive(true); plumDropCountText.text = "" + count; }
                if (itemName == "Stars") { starsIcon.SetActive(true); starsCountText.text = "" + count; }
                break;
        }

        // Left text option in case tean want to add anything there
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
