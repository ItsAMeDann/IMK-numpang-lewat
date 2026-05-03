using UnityEngine;

[CreateAssetMenu(fileName = "CarMovementData", menuName = "Traffic/Car Movement Data")]
public class CarMovementData : ScriptableObject
{
    [Header("Speed")]
    public float maxVelocity = 10f;

    [Header("Forces")]
    public float accelerationForce = 5f;
    public float brakingForce = 8f;

    [Header("Behavior Factors")]
    [Range(0f, 1f)] public float slowFactor = 0.5f;
    [Range(0f, 1f)] public float hardSlowFactor = 0.2f;

    [Header("Detection")]
    public float detectionDistance = 10f;
}