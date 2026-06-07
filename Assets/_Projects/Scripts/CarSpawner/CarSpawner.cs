using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    public CarSpawnData spawnData;
    public Transform[] spawnPoints;
    public Transform playerCamera;
    [SerializeField] private float spawnCheckRadius = 6f;
    [SerializeField] private LayerMask carLayer;

    private List<GameObject> activeCars = new List<GameObject>();

    private void Start()
    {
        SpawnCar();
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        foreach (var point in spawnPoints)
        {
            if (point != null)
                Gizmos.DrawWireSphere(point.position, spawnCheckRadius);
        }
    }

    private bool CanSpawnAt(Transform spawnPoint)
    {
        Collider[] hits = Physics.OverlapSphere(
            spawnPoint.position,
            spawnCheckRadius,
            carLayer
        );

        return hits.Length == 0;
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetSpawnInterval());

            if (GameManager.CurrentState != GameManager.GameState.Playing)
                continue;

            Debug.Log($"Attempting to spawn car. Active cars: {activeCars.Count}, Max allowed: {spawnData?.maxCarsAlive ?? 0}");

            if (spawnData == null || spawnPoints.Length == 0)
                continue;

            if (activeCars.Count >= spawnData.maxCarsAlive)
                continue;

            SpawnCar();
        }
    }

    private float GetSpawnInterval()
    {
        if (spawnData.useRandomInterval)
        {
            return Random.Range(spawnData.minInterval, spawnData.maxInterval);
        }

        return spawnData.spawnInterval;
    }

    private void SpawnCar()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = spawnData.carPrefabs[Random.Range(0, spawnData.carPrefabs.Length)];

        if (!CanSpawnAt(spawnPoint))
        {
            Debug.Log($"Spawn point {spawnPoint.name} is occupied. Skipping spawn.");
            return;
        }

        GameObject car = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        activeCars.Add(car);
        car.GetComponent<CarDespawn>()?.setPlayerCamera(playerCamera);
        Debug.Log($"Spawned car: {car.name}. Transform: {car.transform}.");

        // Optional: auto-remove when destroyed
        StartCoroutine(RemoveWhenDestroyed(car));
    }

    private IEnumerator RemoveWhenDestroyed(GameObject car)
    {
        yield return new WaitUntil(() => car == null);
        activeCars.Remove(car);
    }
}