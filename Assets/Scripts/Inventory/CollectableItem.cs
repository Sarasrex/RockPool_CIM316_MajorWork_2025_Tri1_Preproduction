using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [Tooltip("Exact name of the item (must match the constants in InventoryManager and the icons in UIManager)")]
    public string itemName;  // e.g., "SeaweedSnarlRoll"

    [Tooltip("Category of the item: 'Food' or 'Home'")]
    public string itemCategory;  // e.g., "Food" or "Home"

    [Header("Audio Settings")]
    [Tooltip("AudioSource played when this item is collected (optional - will auto-assign based on category if left blank)")]
    public AudioSource collectAudioSource;

    [Tooltip("AudioSource to use if itemCategory is 'Food' and no collectAudioSource is assigned")]
    public AudioSource foodAudioSource;

    [Tooltip("AudioSource to use if itemCategory is 'Home' and no collectAudioSource is assigned")]
    public AudioSource homeAudioSource;

    private bool isCollected = false;

    void Start()
    {
        // Auto-assign audio source if none manually set
        if (collectAudioSource == null)
        {
            if (itemCategory == "Food" && foodAudioSource != null)
            {
                collectAudioSource = foodAudioSource;
            }
            else if (itemCategory == "Home" && homeAudioSource != null)
            {
                collectAudioSource = homeAudioSource;
            }
        }
    }

    void OnMouseDown()
    {
        if (isCollected) return;
        isCollected = true;

        // Add the item to the inventory system
        InventoryManager.Instance.CollectItem(itemName, itemCategory);

        // Play sound and then destroy
        if (collectAudioSource != null)
        {
            collectAudioSource.Play();
            StartCoroutine(DestroyAfterSound(collectAudioSource.clip.length));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSound(float delay)
    {
        // Disable interaction and visuals immediately
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = false;
        }

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
