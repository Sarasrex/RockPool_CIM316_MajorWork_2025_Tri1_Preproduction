using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitRoaming : MonoBehaviour
{
    [Header("Roaming Settings")]
    [SerializeField] private float moveRadius = 5f;       // How far it can roam
    [SerializeField] private float moveSpeed = 1.5f;      // Crab walk speed
    [SerializeField] private float waitTime = 2f;         // Pause time between moves

    [Header("Scene Settings")]
    [SerializeField] private Vector2 roamCenter = Vector2.zero; // Central point to roam around
    [SerializeField] private float groundY = 1f;                // Y position to stay on

    private Vector3 targetPos;
    private bool isMoving = false;

    void Start()
    {
        StartCoroutine(RoamRoutine());
    }

    IEnumerator RoamRoutine()
    {
        while (true)
        {
            // Pick a new random position within range
            Vector2 randomOffset = Random.insideUnitCircle * moveRadius;
            targetPos = new Vector3(roamCenter.x + randomOffset.x, groundY, roamCenter.y + randomOffset.y);

            isMoving = true;

            // Move toward the target position
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                // Optional: Flip crab based on direction (for 2.5D)
                Vector3 scale = transform.localScale;
                if (targetPos.x < transform.position.x) scale.x = -Mathf.Abs(scale.x);
                else scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;

                yield return null;
            }

            isMoving = false;

            // Wait before next move
            yield return new WaitForSeconds(waitTime);
        }
    }
}
