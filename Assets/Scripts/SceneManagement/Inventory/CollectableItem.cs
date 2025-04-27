using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Tooltip("Exact name of the item (must match constants in InventoryManager)")]
    public string itemName;

    [Tooltip("Category of the item: 'Home' or 'Food'")]
    public string itemCategory;

    void OnMouseDown()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.CollectItem(itemName, itemCategory);
        }
        else
        {
            Debug.LogWarning("InventoryManager not found!");
        }

        gameObject.SetActive(false);
    }
}
