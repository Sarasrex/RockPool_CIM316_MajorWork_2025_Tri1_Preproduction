using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (compassPanel != null) compassPanel.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedFindUI());
    }

    private IEnumerator DelayedFindUI()
    {
        yield return null;
        FindPanels();
        FindIcons();
        FindButtons();
    }

    private void FindPanels()
    {
        compassPanel = GameObject.Find("CompassPanel");
        inventoryPanel = GameObject.Find("InventoryPanel");
    }

    private void FindIcons()
    {
        goldCoinHomeIcon = GameObject.Find("GoldCoinHomeIcon");
        mirrorShellHomeIcon = GameObject.Find("MirrorShellHomeIcon");
        boardNoseHomeIcon = GameObject.Find("BoardNoseHomeIcon");
        rockBowlHomeIcon = GameObject.Find("RockBowlHomeIcon");
        snorkelHomeIcon = GameObject.Find("SnorkelHomeIcon");
        shellPurseHomeIcon = GameObject.Find("ShellPurseHomeIcon");
        lostCompassHomeIcon = GameObject.Find("LostCompassHomeIcon");
        charStickHomeIcon = GameObject.Find("CharStickHomeIcon");
        coralSpoonHomeIcon = GameObject.Find("CoralSpoonHomeIcon");
        stoneStackHomeIcon = GameObject.Find("StoneStackHomeIcon");
        glassHeartHomeIcon = GameObject.Find("GlassHeartHomeIcon");
        usbStickHomeIcon = GameObject.Find("UsbStickHomeIcon");

        seaweedSnarlRollIcon = GameObject.Find("SeaweedSnarlRollIcon");
        squidlyChewIcon = GameObject.Find("SquidlyChewIcon");
        driftChipDipIcon = GameObject.Find("DriftChipDipIcon");
        seaSaladIcon = GameObject.Find("SeaSaladIcon");
        jellyWiggleIcon = GameObject.Find("JellyWiggleIcon");
        bubbleBurstIcon = GameObject.Find("BubbleBurstIcon");
        beachBananaIcon = GameObject.Find("BeachBananaIcon");
        crunchMunchIcon = GameObject.Find("CrunchMunchIcon");
        bubbleBounceIcon = GameObject.Find("BubbleBounceIcon");
        noriNibbleIcon = GameObject.Find("NoriNibbleIcon");
        snailSlurpIcon = GameObject.Find("SnailSlurpIcon");
        plumDropIcon = GameObject.Find("PlumDropIcon");
        starsIcon = GameObject.Find("StarsIcon");
    }

    private void FindButtons()
    {
        GameObject compassButtonObj = GameObject.Find("CompassButton");
        GameObject inventoryButtonObj = GameObject.Find("InventoryButton");

        if (compassButtonObj != null)
        {
            Button button = compassButtonObj.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ToggleCompass);
        }

        if (inventoryButtonObj != null)
        {
            Button button = inventoryButtonObj.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(ToggleInventory);
        }
    }

    public void ToggleCompass()
    {
        if (compassPanel != null)
            compassPanel.SetActive(!compassPanel.activeSelf);
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void ShowItem(string itemName, string itemCategory)
    {
        if (itemCategory == "Home")
        {
            if (itemName == InventoryManager.GoldCoinHome && goldCoinHomeIcon != null) goldCoinHomeIcon.SetActive(true);
            if (itemName == InventoryManager.MirrorShellHome && mirrorShellHomeIcon != null) mirrorShellHomeIcon.SetActive(true);
            if (itemName == InventoryManager.BoardNoseHome && boardNoseHomeIcon != null) boardNoseHomeIcon.SetActive(true);
            if (itemName == InventoryManager.RockBowlHome && rockBowlHomeIcon != null) rockBowlHomeIcon.SetActive(true);
            if (itemName == InventoryManager.SnorkelHome && snorkelHomeIcon != null) snorkelHomeIcon.SetActive(true);
            if (itemName == InventoryManager.ShellPurseHome && shellPurseHomeIcon != null) shellPurseHomeIcon.SetActive(true);
            if (itemName == InventoryManager.LostCompassHome && lostCompassHomeIcon != null) lostCompassHomeIcon.SetActive(true);
            if (itemName == InventoryManager.CharStickHome && charStickHomeIcon != null) charStickHomeIcon.SetActive(true);
            if (itemName == InventoryManager.CoralSpoonHome && coralSpoonHomeIcon != null) coralSpoonHomeIcon.SetActive(true);
            if (itemName == InventoryManager.StoneStackHome && stoneStackHomeIcon != null) stoneStackHomeIcon.SetActive(true);
            if (itemName == InventoryManager.GlassHeartHome && glassHeartHomeIcon != null) glassHeartHomeIcon.SetActive(true);
            if (itemName == InventoryManager.UsbStickHome && usbStickHomeIcon != null) usbStickHomeIcon.SetActive(true);
        }
        else if (itemCategory == "Food")
        {
            if (itemName == InventoryManager.SeaweedSnarlRoll && seaweedSnarlRollIcon != null) seaweedSnarlRollIcon.SetActive(true);
            if (itemName == InventoryManager.SquidlyChew && squidlyChewIcon != null) squidlyChewIcon.SetActive(true);
            if (itemName == InventoryManager.DriftChipDip && driftChipDipIcon != null) driftChipDipIcon.SetActive(true);
            if (itemName == InventoryManager.SeaSalad && seaSaladIcon != null) seaSaladIcon.SetActive(true);
            if (itemName == InventoryManager.JellyWiggle && jellyWiggleIcon != null) jellyWiggleIcon.SetActive(true);
            if (itemName == InventoryManager.BubbleBurst && bubbleBurstIcon != null) bubbleBurstIcon.SetActive(true);
            if (itemName == InventoryManager.BeachBanana && beachBananaIcon != null) beachBananaIcon.SetActive(true);
            if (itemName == InventoryManager.CrunchMunch && crunchMunchIcon != null) crunchMunchIcon.SetActive(true);
            if (itemName == InventoryManager.BubbleBounce && bubbleBounceIcon != null) bubbleBounceIcon.SetActive(true);
            if (itemName == InventoryManager.NoriNibble && noriNibbleIcon != null) noriNibbleIcon.SetActive(true);
            if (itemName == InventoryManager.SnailSlurp && snailSlurpIcon != null) snailSlurpIcon.SetActive(true);
            if (itemName == InventoryManager.PlumDrop && plumDropIcon != null) plumDropIcon.SetActive(true);
            if (itemName == InventoryManager.Stars && starsIcon != null) starsIcon.SetActive(true);
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
