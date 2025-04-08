using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitCrabDropTarget : MonoBehaviour
{
    public string hermitName;
    public string acceptedCategory; // "Food", "Home", or leave blank for any

    public void ReceiveItem(string itemName, string itemCategory)
    {
        if (string.IsNullOrEmpty(acceptedCategory) || acceptedCategory == itemCategory)
        {
            Debug.Log("[" + hermitName + "] received item: " + itemName + " (" + itemCategory + ")");

            // Placeholder for reaction logic
            // You could add animations, dialogue, or compass changes here
        }
    }
}
