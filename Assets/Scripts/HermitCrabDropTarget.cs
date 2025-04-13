using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitCrabDropTarget : MonoBehaviour
{
    public string hermitName;
    public string acceptedCategory; // "Food", "Home", or leave blank for any

    [Header("Preferences")]
    public string[] likedFoods;
    public string[] dislikedFoods;
    public string[] likedHomes;
    public string[] dislikedHomes;

    [Header("Happiness")]
    [Range(0, 100)] public float happiness = 50f;

    public void ReceiveItem(string itemName, string itemCategory)
    {
        if (!string.IsNullOrEmpty(acceptedCategory) && acceptedCategory != itemCategory)
            return;

        Debug.Log($"[{hermitName}] received {itemName} ({itemCategory})");

        // Determine delta based on preference
        int delta = 0;

        if (itemCategory == "Food")
        {
            if (System.Array.Exists(likedFoods, item => item == itemName))
                delta = 15;
            else if (System.Array.Exists(dislikedFoods, item => item == itemName))
                delta = -15;
        }
        else if (itemCategory == "Home")
        {
            if (System.Array.Exists(likedHomes, item => item == itemName))
                delta = 20;
            else if (System.Array.Exists(dislikedHomes, item => item == itemName))
                delta = -20;
        }

        happiness = Mathf.Clamp(happiness + delta, 0, 100);
        CommunityCompassManager.Instance.UpdateCommunityHappiness();

        // TODO: Trigger visual and audio feedback
    }
}
