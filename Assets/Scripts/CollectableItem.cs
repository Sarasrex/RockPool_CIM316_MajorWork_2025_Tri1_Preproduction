using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    // Declaring item names for homes
    public string[] itemNames = new string[]
    {
        "goldCoinHome",
        "mirrorShellHome",
        "boardNoseHome",
        "rockBowlHome",
        "snorkelHome",
        "shellPurseHome",
        "lostCompassHome",
        "charStickHome",
        "coralSpoonHome",
        "stoneStackHome",
        "glassHeartHome"
    };

    // Declaring item names for food
    public string[] foodItemNames = new string[]
    {
        "seaweedSnarlRoll",
        "squidlyChew",  // Fixed typo from "squidlyShew" to "squidlyChew"
        "driftChipDip",
        "seaSalad",
        "jellyWiggle",
        "bubbleBurst",
        "beachBanana",
        "crunchMunch",
        "bubbleBounce",
        "noriNibble",
        "snailSlurp",
        "plumDrop",
        "stars"
    };

    public string itemCategory;  // e.g., "Home", "Food", "Treasure"

    // OnMouseDown is triggered when the player clicks the object
    void OnMouseDown()
    {
        // Loop through both the home and food item arrays and collect each item
        if (itemCategory == "Home")
        {
            foreach (string itemName in itemNames)
            {
                // Pass each item to InventoryManager for collection
                InventoryManager.Instance.CollectItem(itemName, itemCategory);
            }
        }
        else if (itemCategory == "Food")
        {
            foreach (string itemName in foodItemNames)
            {
                // Pass each food item to InventoryManager for collection
                InventoryManager.Instance.CollectItem(itemName, itemCategory);
            }
        }

        // Hide the collectible item after it's collected
        gameObject.SetActive(false);
    }
}
