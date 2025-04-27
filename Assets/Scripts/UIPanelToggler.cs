using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelToggler : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject compassPanel;
    public GameObject inventoryPanel;
    public GameObject mapPanel;

    public void ToggleCompassPanel()
    {
        TogglePanel(compassPanel);
    }

    public void ToggleInventoryPanel()
    {
        TogglePanel(inventoryPanel);
    }

    public void ToggleMapPanel()
    {
        TogglePanel(mapPanel);
    }

    private void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
        else
        {
            Debug.LogWarning("Panel not assigned.");
        }
    }
}
