using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverGlowEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private Image image;
    private Color originalColor;

    [SerializeField] private Color glowColor = Color.white;
    [SerializeField] private float scaleMultiplier = 1.1f;
    [SerializeField] private float transitionSpeed = 10f;

    private bool isHovered = false;

    void Start()
    {
        originalScale = transform.localScale;
        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
        }
    }

    void Update()
    {
        Vector3 targetScale = isHovered ? originalScale * scaleMultiplier : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * transitionSpeed);

        if (image != null)
        {
            Color targetColor = isHovered ? glowColor : originalColor;
            image.color = Color.Lerp(image.color, targetColor, Time.deltaTime * transitionSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
