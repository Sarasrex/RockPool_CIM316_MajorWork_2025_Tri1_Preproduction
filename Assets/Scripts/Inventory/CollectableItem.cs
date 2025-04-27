using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Tooltip("Exact name of the item (must match the constants in InventoryManager and the icons in UIManager)")]
    public string itemName;  // e.g., "SeaweedSnarlRoll"

    [Tooltip("Category of the item: 'Food' or 'Home'")]
    public string itemCategory;  // e.g., "Food" or "Home"

    // This method is triggered when the player clicks the object
    void OnMouseDown()
    {
        // Add the item to the inventory system
        InventoryManager.Instance.CollectItem(itemName, itemCategory);

        // Hide the item after collection
        gameObject.SetActive(false);
    }
}
