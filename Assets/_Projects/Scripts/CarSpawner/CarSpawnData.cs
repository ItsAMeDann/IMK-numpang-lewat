using UnityEngine;

[CreateAssetMenu(fileName = "CarSpawnData", menuName = "Traffic/Car Spawn Data")]
public class CarSpawnData : ScriptableObject
{
    [Header("Spawn Timing")]
    public float spawnInterval = 2f;
    public bool useRandomInterval = false;
    public float minInterval = 1f;
    public float maxInterval = 3f;

    [Header("Car Settings")]
    public GameObject[] carPrefabs;
    public int maxCarsAlive = 10;
}