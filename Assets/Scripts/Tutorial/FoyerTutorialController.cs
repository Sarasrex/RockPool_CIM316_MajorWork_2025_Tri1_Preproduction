using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Foyer tutorial flow (in-scene, not a separate CutSceneLayer).
/// Shows an ordered set of panels once per save, with arrows pointing to UI.
/// Moves between steps when the player opens/closes Inventory/Compass and clicks Map.
/// Notes:
/// - Uses PlayerPrefs so this only shows the first time (configurable).
/// - Arrows and Octavius reactions match the previous behavior.
/// - Exposes public methods to call from existing buttons.

public class FoyerTutorialController : MonoBehaviour
{
    [Header("Show once")]
    public bool showOnlyOnce = true;
    public string playerPrefsKey = "Tutorial_Foyer_Seen"; // make this unique per tutorial

    [Header("Arrows")]
    public GameObject arrowInventory;
    public GameObject arrowCompass;
    public GameObject arrowMap;

    [Header("Speech/Instruction Panels")]
    // Map your existing canvases here
    public GameObject panelHelloYou;   // was: canvasHelloYouInstructions
    public GameObject panelInventory;  // was: canvasInventoryInstructions
    public GameObject panelOrbComment; // was: canvasOrbComment
    public GameObject panelCompass;    // was: canvasCompassInstructions
    public GameObject panelMap;        // was: canvasMapInstructions

    [Header("Optional: UI panels (for your own toggles)")]
    public GameObject inventoryPanel;  // not controlled here, only referenced if you need it
    public GameObject compassPanel;

    [Header("Octavius Reaction Controller")]
    public HermitReactionController octaviusReactionController;

    // Ordered steps in this tutorial
    private enum TutorialStep { Inventory, Compass, Map, Done }
    private TutorialStep currentStep = TutorialStep.Inventory;

    void Start()
    {
        // If already shown and configured to show once, disable and exit.
        if (showOnlyOnce && PlayerPrefs.GetInt(playerPrefsKey, 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        // Initial state: highlight Inventory step
        HideAllPanels();
        SetArrows(inventory: true, compass: false, map: false);

        SafeSetActive(panelHelloYou, true);   // "Hello, you" opener
        SafeSetActive(panelInventory, false);
        SafeSetActive(panelOrbComment, false);
        SafeSetActive(panelCompass, false);
        SafeSetActive(panelMap, false);

        currentStep = TutorialStep.Inventory;

        // Play Octavius Hello on start
        if (octaviusReactionController != null)
            octaviusReactionController.PlayReaction("Hello");
    }

    // === External hooks called from UI buttons/toggles ===

    // Call when Inventory is opened (alongside your open logic)
    public void OnInventoryOpened()
    {
        if (currentStep != TutorialStep.Inventory) return;

        Debug.Log("[FoyerTutorial] Inventory opened");
        SetArrows(inventory: false, compass: false, map: false);

        SafeSetActive(panelHelloYou, false);
        SafeSetActive(panelInventory, true); // show inventory instructions

        if (octaviusReactionController != null)
            octaviusReactionController.PlayReaction("Positive");
    }

    // Call when Inventory is closed
    public void OnInventoryClosed()
    {
        if (currentStep != TutorialStep.Inventory) return;

        Debug.Log("[FoyerTutorial] Inventory closed -> move to Compass step");
        SafeSetActive(panelInventory, false);

        // Show orb comment, then point to Compass
        SafeSetActive(panelOrbComment, true);
        SetArrows(inventory: false, compass: true, map: false);

        currentStep = TutorialStep.Compass;

        if (octaviusReactionController != null)
            octaviusReactionController.PlayReaction("Positive");
    }

    // Call when Compass is opened
    public void OnCompassOpened()
    {
        if (currentStep != TutorialStep.Compass) return;

        Debug.Log("[FoyerTutorial] Compass opened");
        SafeSetActive(panelOrbComment, false);
        SetArrows(inventory: false, compass: false, map: false);

        SafeSetActive(panelCompass, true);

        if (octaviusReactionController != null)
            octaviusReactionController.PlayReaction("Positive");
    }

    // Call when Compass is closed
    public void OnCompassClosed()
    {
        if (currentStep != TutorialStep.Compass) return;

        Debug.Log("[FoyerTutorial] Compass closed -> move to Map step");
        SafeSetActive(panelCompass, false);

        // Point to Map and show the final prompt
        SetArrows(inventory: false, compass: false, map: true);
        SafeSetActive(panelMap, true);

        currentStep = TutorialStep.Map;
    }

    // Call from the Map button OnClick (alongside your normal scene load)
    public void OnMapClicked()
    {
        if (currentStep != TutorialStep.Map) return;

        Debug.Log("[FoyerTutorial] Map clicked -> tutorial done");
        SetArrows(false, false, false);
        SafeSetActive(panelMap, false);

        currentStep = TutorialStep.Done;

        // Mark as seen so it does not show next time
        if (showOnlyOnce)
        {
            PlayerPrefs.SetInt(playerPrefsKey, 1);
            PlayerPrefs.Save();
        }

        // Disable this tutorial controller. Your normal Map button can still change scene.
        gameObject.SetActive(false);
    }

    // === Helpers ===

    private void HideAllPanels()
    {
        SafeSetActive(panelHelloYou, false);
        SafeSetActive(panelInventory, false);
        SafeSetActive(panelOrbComment, false);
        SafeSetActive(panelCompass, false);
        SafeSetActive(panelMap, false);
    }

    private void SetArrows(bool inventory, bool compass, bool map)
    {
        SafeSetActive(arrowInventory, inventory);
        SafeSetActive(arrowCompass, compass);
        SafeSetActive(arrowMap, map);
    }

    private static void SafeSetActive(GameObject go, bool active)
    {
        if (go != null && go.activeSelf != active) go.SetActive(active);
    }

    // Dev reset helper
    public void ResetSeenFlagForTesting()
    {
        PlayerPrefs.DeleteKey(playerPrefsKey);
        Debug.Log("[FoyerTutorial] Cleared seen flag.");
    }
}
