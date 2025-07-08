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
    [Tooltip("Sound played when this item is collected (optional - will auto-assign based on category if left blank)")]
    public AudioClip collectSound;

    [Tooltip("Sound to use if itemCategory is 'Food' and no collectSound is assigned")]
    public AudioClip foodSound;

    [Tooltip("Sound to use if itemCategory is 'Home' and no collectSound is assigned")]
    public AudioClip homeSound;

    private AudioSource audioSource;
    private bool isCollected = false;

    void Awake()
    {
        // Add AudioSource if not already present
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
    }

    void Start()
    {
        // Auto-assign sound if none manually set
        if (collectSound == null)
        {
            if (itemCategory == "Food" && foodSound != null)
            {
                collectSound = foodSound;
            }
            else if (itemCategory == "Home" && homeSound != null)
            {
                collectSound = homeSound;
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
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
            StartCoroutine(DestroyAfterSound(collectSound.length));
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
