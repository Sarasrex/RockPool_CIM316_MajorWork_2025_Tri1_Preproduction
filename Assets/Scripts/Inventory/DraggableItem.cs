using UnityEngine;
using UnityEngine.EventSystems;

// This script allows a UI item (like food or home) to be dragged around the screen
// and dropped onto a hermit crab to trigger interactions.

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemName;            // Name of the item (used in ReceiveItem logic)
    public string itemCategory;        // "Food" or "Home" — determines preference matching

    private RectTransform rectTransform;   // Cached reference to the item's RectTransform
    private CanvasGroup canvasGroup;       // Used to disable raycasts while dragging
    private Transform originalParent;      // Stores the parent so we can reset it
    private Vector2 originalPosition;      // Stores the anchored position to reset after drag

    // Set up cached references on awake
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Called when dragging starts
    public void OnBeginDrag(PointerEventData eventData)
    {
        int count = InventoryManager.Instance.itemCounts.ContainsKey(itemName)
        ? InventoryManager.Instance.itemCounts[itemName]
    :   0;

        if (count <= 0)
        {
            Debug.Log($" Cannot drag '{itemName}' because count is 0.");
            return;
        }

        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        // Reparent to root canvas so it renders above other UI elements
        transform.SetParent(transform.root, true);

        // Disable raycasts so drop targets can receive input
        canvasGroup.blocksRaycasts = false;

        // Make sure the item renders above everything else
        transform.SetAsLastSibling();
    }

    // Called continuously while dragging
    public void OnDrag(PointerEventData eventData)
    {
        // Move the item based on mouse delta
        rectTransform.anchoredPosition += eventData.delta;
    }

    // Called when dragging ends
    public void OnEndDrag(PointerEventData eventData)
    {
        // Re-enable raycasts
        canvasGroup.blocksRaycasts = true;

        // Cast a 3D ray from the camera to detect a hermit crab in world space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int crabLayerMask = Physics.DefaultRaycastLayers;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, crabLayerMask))
        {
            // Check if the hit object has a HermitCrabDropTarget script
            HermitCrabDropTarget crab = hit.collider.GetComponent<HermitCrabDropTarget>();
            if (crab != null)
            {
                Debug.Log("Hit crab: " + crab.hermitName);
                crab.ReceiveItem(itemName, itemCategory); // Give the item to the crab
            }
            else
            {
                Debug.Log("Hit something, but no HermitCrabDropTarget found.");
            }
        }
        else
        {
            Debug.Log("No 3D collider hit at drop point.");
        }

        // Reset the UI item back to where it started
        rectTransform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }
}
