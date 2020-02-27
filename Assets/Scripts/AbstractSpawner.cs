using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    public Transform[] SpawnPositions;

    [Range(0.1f, 100f)]
    public float SpawnDelay = 10f;

    [Range(1, 100)]
    public int SpawnQuantity = 1;

    public GameObject SpawnItem;

    private Coroutine spawningCoroutine;
    private bool isSpawning;

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(Spawn());
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private IEnumerator Spawn()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(SpawnDelay);
            Instantiate(SpawnItem, SpawnPositions[0].position, Quaternion.identity);
        }
    }

    private void Start()
    {
        if (SpawnPositions.Length == 0)
        {
            throw new NullReferenceException("At least 1 spawn position is required.");
        }

        if (SpawnItem == null)
        {
            throw new NullReferenceException("Spawn item is not provided.");
        }
    }

    private void Update()
    {

    }
}
