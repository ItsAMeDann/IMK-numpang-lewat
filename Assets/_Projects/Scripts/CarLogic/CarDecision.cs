using UnityEngine;

public class CarDecision : MonoBehaviour
{
    public enum CarState { NormalDrive, SlowDown, FullStop }

    public float max_velocity = 10f;
    public float acceleration_force = 5f;
    public float braking_force = 8f;

    [Range(0, 1)] public float slow_factor = 0.5f;
    [Range(0, 1)] public float hard_slow_factor = 0.2f;

    public CarState currentState = CarState.NormalDrive;
    private float current_velocity;
    private float target_velocity;

    private bool playerInRange, playerOnRoad, isZebraCross, isWalkerLightOn;

    void Update()
    {
        DetermineStateAndTarget();
        ApplyMovement();
    }

    private void DetermineStateAndTarget()
    {
        if (isWalkerLightOn)
        {
            currentState = CarState.FullStop;
            target_velocity = 0;
        }
        else if (playerInRange && playerOnRoad)
        {
            currentState = CarState.SlowDown;
            target_velocity = isZebraCross ? max_velocity * hard_slow_factor : max_velocity * slow_factor;
        }
        else
        {
            currentState = CarState.NormalDrive;
            target_velocity = max_velocity;
        }
    }

    private void ApplyMovement()
    {
        float currentForce = (target_velocity < current_velocity) ? braking_force : acceleration_force;
        current_velocity = Mathf.MoveTowards(current_velocity, target_velocity, currentForce * Time.deltaTime);
        transform.Translate(Vector3.forward * current_velocity * Time.deltaTime);
    }

    public void UpdateScannerData(bool inRange, bool onRoad, bool zebra, bool walkerLight)
    {
        playerInRange = inRange;
        playerOnRoad = onRoad;
        isZebraCross = zebra;
        isWalkerLightOn = walkerLight;
    }
}