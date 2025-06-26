using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelToggler : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject compassPanel;
    public GameObject inventoryPanel;
    public GameObject mapPanel;

    [Header("Tutorial (Optional)")]
    public FoyerTutorialController tutorialController;

    public void ToggleCompassPanel()
    {
        bool isActive = TogglePanel(compassPanel);

        if (tutorialController != null)
        {
            if (isActive)
                tutorialController.OnCompassOpened();
            else
                tutorialController.OnCompassClosed();
        }
    }

    public void ToggleInventoryPanel()
    {
        bool isActive = TogglePanel(inventoryPanel);

        if (tutorialController != null)
        {
            if (isActive)
                tutorialController.OnInventoryOpened();
            else
                tutorialController.OnInventoryClosed();
        }
    }

    public void ToggleMapPanel()
    {
        bool isActive = TogglePanel(mapPanel);

        if (tutorialController != null && !isActive) // Assuming scene load happens elsewhere
        {
            tutorialController.OnMapClicked();
        }
    }

    private bool TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
            return !isActive;
        }
        else
        {
            Debug.LogWarning("Panel not assigned.");
            return false;
        }
    }
}
