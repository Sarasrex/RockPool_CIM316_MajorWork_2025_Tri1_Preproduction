using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemName;
    public string itemCategory; // "Food" or "Home"

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        transform.SetParent(transform.root, true); // Ensure it's under the canvas root

        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling(); // This ensures it renders on top
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // 1. Cast a ray from the camera to the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int crabLayerMask = Physics.DefaultRaycastLayers;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, crabLayerMask))
        {
            HermitCrabDropTarget crab = hit.collider.GetComponent<HermitCrabDropTarget>();
            if (crab != null)
            {
                Debug.Log("Hit crab: " + crab.hermitName);
                crab.ReceiveItem(itemName, itemCategory);
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

        // 2. Reset UI item back to its original position
        rectTransform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }




    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }



}

