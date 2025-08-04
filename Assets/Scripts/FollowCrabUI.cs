using UnityEngine;
using UnityEngine.UI;

public class FollowCrabUI : MonoBehaviour
{
    public Transform crabTransform;       // Assign your crab's Transform here
    public Vector3 offset = new Vector3(0, 2f, 0); // World offset above crab
    public Camera mainCamera;            // Assign your main Camera here

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        if (crabTransform == null || mainCamera == null) return;

        Vector3 worldPosition = crabTransform.position + offset;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

        rectTransform.position = screenPosition;
    }
}
