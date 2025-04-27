using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelToggler : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject compassPanel;
    public GameObject inventoryPanel;

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
        FindPanels();
    }

    private void FindPanels()
    {
        if (compassPanel == null)
            compassPanel = GameObject.Find("CompassPanel");

        if (inventoryPanel == null)
            inventoryPanel = GameObject.Find("InventoryPanel");
    }

    public void ToggleCompassPanel()
    {
        TogglePanel(compassPanel);
    }

    public void ToggleInventoryPanel()
    {
        TogglePanel(inventoryPanel);
    }

    private void TogglePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            Debug.LogWarning("Panel not assigned.");
        }
    }
}
