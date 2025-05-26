using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideManager : MonoBehaviour
{
    public Transform waterPlane;
    public float lowY = 3f;
    public float highY = 8f;
    public float tideSpeed = 2f;
    public float waitTime = 10f;

    public GameObject[] barnaclePrefabs; // Prefabs to instantiate
    public Transform[] spawnPoints; // Set these manually in the Inspector

    private bool isHighTide = true;

    public Transform[] spawnWave1;
    public Transform[] spawnWave2;
    public Transform[] spawnWave3;

    void Start()
    {
        StartCoroutine(TideCycle());
    }

    IEnumerator TideCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            float targetY = isHighTide ? lowY : highY;

            if (!isHighTide)
            {
                // Start barnacles shortly after tide starts rising
                StartCoroutine(ReplaceBarnaclesDelayed(3f));
            }

            yield return StartCoroutine(MoveWater(targetY));

            if (isHighTide)
            {
                DestroyBarnacles();
            }

            isHighTide = !isHighTide;
        }
    }


    IEnumerator MoveWater(float targetY)
    {
        float elapsed = 0f;
        Vector3 startPos = waterPlane.position;
        Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);

        while (elapsed < tideSpeed)
        {
            elapsed += Time.deltaTime;
            waterPlane.position = Vector3.Lerp(startPos, targetPos, elapsed / tideSpeed);
            yield return null;
        }
    }

    IEnumerator ReplaceBarnaclesDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ReplaceBarnacles());
    }


    IEnumerator ReplaceBarnacles()
    {
        Transform[] chosenWave = GetRandomWave();

        foreach (Transform point in chosenWave)
        {
            int rand = Random.Range(0, barnaclePrefabs.Length);
            Instantiate(barnaclePrefabs[rand], point.position, Quaternion.identity);

            yield return new WaitForSeconds(1f); // Delay between spawns
        }
    }


    Transform[] GetRandomWave()
    {
        int choice = Random.Range(0, 3);
        switch (choice)
        {
            case 0: return spawnWave1;
            case 1: return spawnWave2;
            case 2: return spawnWave3;
            default: return spawnWave1;
        }
    }


    void DestroyBarnacles()
    {
        GameObject[] oldBarnacles = GameObject.FindGameObjectsWithTag("Barnacle");
        foreach (GameObject b in oldBarnacles)
        {
            Destroy(b);
        }
    }
}
