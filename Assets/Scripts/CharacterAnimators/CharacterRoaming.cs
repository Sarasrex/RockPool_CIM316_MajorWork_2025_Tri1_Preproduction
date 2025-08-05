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
    public Transform spriteTransform; // Assign the visual body (e.g. Pearl_Body) here

    [HideInInspector] public bool isAsleep = false;

    private Vector3 originPosition;
    private Vector3 targetPosition;
    private bool isMoving;
    private float pauseTimer;

    private const float stopDistance = 0.05f;

    void Start()
    {
        originPosition = transform.position;
        SetNewTargetPosition();
    }

    void Update()
    {
        if (isAsleep) return;

        if (isMoving)
        {
            Vector3 moveDirection = (targetPosition - transform.position);
            moveDirection.y = 0f; // Prevent vertical movement

            if (moveDirection.magnitude < stopDistance)
            {
                isMoving = false;
                pauseTimer = pauseTime;
                return;
            }

            Vector3 moveStep = moveDirection.normalized * moveSpeed * Time.deltaTime;
            transform.position += moveStep;

            // Flip only the sprite visual (not the whole transform)
            if (spriteTransform != null)
            {
                SpriteRenderer sr = spriteTransform.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.flipX = moveDirection.x < 0;
                }
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
