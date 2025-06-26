using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoaming : MonoBehaviour
{
    [Header("Roaming Settings")]
    public float moveSpeed = 1.5f;
    public float pauseTime = 1.5f;

    [Header("Axis Movement Ranges")]
    public Vector2 xRange = new Vector2(-3f, 3f); // min and max movement from origin
    public Vector2 yRange = new Vector2(0f, 0f);   // lock Y by default (stays on ground)
    public Vector2 zRange = new Vector2(-3f, 3f);

    [Header("Sprite Settings")]
    public Transform spriteTransform;

    private Vector3 originPosition;
    private Vector3 targetPosition;
    private bool isMoving;
    private float pauseTimer;

    void Start()
    {
        originPosition = transform.position;
        SetNewTargetPosition();
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                pauseTimer = pauseTime;
            }

            if (spriteTransform != null && moveDirection.x != 0)
            {
                Vector3 newScale = spriteTransform.localScale;
                newScale.x = Mathf.Sign(moveDirection.x) * Mathf.Abs(newScale.x);
                spriteTransform.localScale = newScale;
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                SetNewTargetPosition();
            }
        }
    }

    void SetNewTargetPosition()
    {
        Vector3 offset = new Vector3(
            Random.Range(xRange.x, xRange.y),
            Random.Range(yRange.x, yRange.y),
            Random.Range(zRange.x, zRange.y)
        );

        targetPosition = originPosition + offset;
        isMoving = true;
    }
}
