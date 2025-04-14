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
    [Range(0, 100)] public float happiness = 0f;

    public void Awake()
    {
        happiness = 0f;
    }
    
    
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

        // Next part triggers dialgoue / audio / visual feedback

        
        // Determine dialogue category based on item preference
        DialogueTriggerType trigger;

        if (System.Array.Exists(likedFoods, item => item == itemName) ||
            System.Array.Exists(likedHomes, item => item == itemName))
        {
            trigger = DialogueTriggerType.InAgreement;
        }
        else if (System.Array.Exists(dislikedFoods, item => item == itemName) ||
                 System.Array.Exists(dislikedHomes, item => item == itemName))
        {
            trigger = DialogueTriggerType.Disapproves;
        }
        else
        {
            trigger = DialogueTriggerType.Munching;
        }

        // Trigger dialogue
        HermitDialogue dialogue = GetComponent<HermitDialogue>();
        if (dialogue != null)
        {
            DialogueLine line = dialogue.GetRandomLineByTrigger(trigger);
            UIManager.Instance.ShowDialogue(hermitName, line);
        }

    }
}
