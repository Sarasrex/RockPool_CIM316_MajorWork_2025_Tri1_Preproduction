using UnityEngine;
using UnityEngine.UI;

public class ImageFlipper : MonoBehaviour
{
    public bool flipX = false;
    public bool flipY = false;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplyFlip();
    }

    void OnValidate()
    {
        // Apply changes immediately in the Editor when values change
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        ApplyFlip();
    }

    void ApplyFlip()
    {
        Vector3 scale = rectTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * (flipX ? -1 : 1);
        scale.y = Mathf.Abs(scale.y) * (flipY ? -1 : 1);
        rectTransform.localScale = scale;
    }
}
