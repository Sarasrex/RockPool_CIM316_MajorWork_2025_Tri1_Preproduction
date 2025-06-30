using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoyerTutorialController : MonoBehaviour
{
    [Header("Arrows")]
    public GameObject arrowInventory;
    public GameObject arrowCompass;
    public GameObject arrowMap;

    [Header("Speech Bubbles (Octavius Children)")]
    public GameObject canvasHelloYouInstructions;
    public GameObject canvasInventoryInstructions;
    public GameObject canvasOrbComment;
    public GameObject canvasCompassInstructions;
    public GameObject canvasMapInstructions;

    [Header("Panels")]
    public GameObject inventoryPanel;
    public GameObject compassPanel;

    private enum TutorialStep { Inventory, Compass, Map, Done }
    private TutorialStep currentStep = TutorialStep.Inventory;

    void Start()
    {
        // Initial tutorial setup
        arrowInventory.SetActive(true);
        canvasHelloYouInstructions.SetActive(true);
        canvasInventoryInstructions.SetActive(false);
        canvasOrbComment.SetActive(false);
        canvasCompassInstructions.SetActive(false);
        canvasMapInstructions.SetActive(false);
        arrowCompass.SetActive(false);
        arrowMap.SetActive(false);
    }

    public void OnInventoryOpened()
    {
        Debug.Log("Inventory Opened - Tutorial triggered.");

        if (currentStep != TutorialStep.Inventory) return;

        arrowInventory.SetActive(false);
        canvasHelloYouInstructions.SetActive(false);
        canvasInventoryInstructions.SetActive(true);
    }

    public void OnInventoryClosed()
    {
        Debug.Log("Inventory Closed - Tutorial continues.");

        if (currentStep != TutorialStep.Inventory) return;

        canvasInventoryInstructions.SetActive(false);

        // Show orb comment after closing inventory
        canvasOrbComment.SetActive(true);

        arrowCompass.SetActive(true);
        currentStep = TutorialStep.Compass;
    }

    public void OnCompassOpened()
    {
        if (currentStep != TutorialStep.Compass) return;

        // Hide orb comment and show compass instructions
        canvasOrbComment.SetActive(false);
        arrowCompass.SetActive(false);
        canvasCompassInstructions.SetActive(true);
    }

    public void OnCompassClosed()
    {
        if (currentStep != TutorialStep.Compass) return;

        canvasCompassInstructions.SetActive(false);
        arrowMap.SetActive(true);
        canvasMapInstructions.SetActive(true);
        currentStep = TutorialStep.Map;
    }

    public void OnMapClicked()
    {
        if (currentStep != TutorialStep.Map) return;

        arrowMap.SetActive(false);
        canvasMapInstructions.SetActive(false);
        currentStep = TutorialStep.Done;

        // SceneManager.LoadScene("MapScene");
    }
}
