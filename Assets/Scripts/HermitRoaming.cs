using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitRoaming : MonoBehaviour
{
    
    // I have removed this roaming script from all hermits except for Captain claw due to bug causing them to bunch. I have left it on Captain claw for now. Will fix at a later date. 
    // Just kidding, I have removed it off Captain claw for now as well.


    private SpriteRenderer spriteRenderer;

    [Header("Roaming Settings")]
    [SerializeField] private float moveRadius = 2f;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float waitTime = 5f;

    [Header("Scene Settings")]
    [SerializeField] private Vector2 roamCenter = Vector2.zero;
    [SerializeField] private float groundY = 1f;

    [Header("Anti-Bunching")]
    [SerializeField] private float personalSpace = 5f;
    [SerializeField] private LayerMask hermitLayer;

    private Vector3 targetPos;
    private bool isMoving = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RoamRoutine());
    }

    IEnumerator RoamRoutine()
    {
        while (true)
        {
            // Try up to 10 times to find a valid spot with personal space
            bool foundSpot = false;
            int attempts = 0;

            while (!foundSpot && attempts < 10)
            {
                Vector2 randomOffset = Random.insideUnitCircle * moveRadius;
              //  Vector3 potentialTarget = new Vector3(roamCenter.x + randomOffset.x, groundY, roamCenter.y + randomOffset.y); Removed this due to bug. eplaced with below 
                Vector3 potentialTarget = new Vector3(roamCenter.x + randomOffset.x, transform.position.y, roamCenter.y + randomOffset.y);


                // Check for other hermits nearby
                Collider[] overlaps = Physics.OverlapSphere(potentialTarget, personalSpace, hermitLayer);
                if (overlaps.Length == 0)
                {
                    targetPos = potentialTarget;
                    foundSpot = true;
                }

                attempts++;
            }

            if (!foundSpot)
            {
                yield return new WaitForSeconds(waitTime);
                continue;
            }

            isMoving = true;

            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                // Flip the sprite visually, not the scale!
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = targetPos.x < transform.position.x;
                }

                yield return null;
            }

            isMoving = false;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
