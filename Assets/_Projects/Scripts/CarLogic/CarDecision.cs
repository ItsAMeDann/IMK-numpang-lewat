using UnityEngine;

public class CarDecision : MonoBehaviour
{
    public enum CarState { NormalDrive, SlowDown, FullStop }

    public CarMovementData movementData;
    public CarMove carMove;
    [Range(0, 1)] public float slow_factor = 0.5f;
    [Range(0, 1)] public float hard_slow_factor = 0.2f;

    public CarState currentState = CarState.NormalDrive;
    private float target_velocity;

    private bool playerInRange, playerGiveSignal, playerOnZebraCross, isWalkerLightOn, zebraCrossInRange, isCarAhead;

    void Update()
    {
        DetermineStateAndTarget();
    }

    private void DetermineStateAndTarget()
    {
        if (isCarAhead)
        {
            currentState = CarState.FullStop;
            target_velocity = 0f;
        }
        else if (isWalkerLightOn || (playerInRange && playerOnZebraCross) || (playerInRange && playerGiveSignal))
        {
            currentState = CarState.FullStop;
            target_velocity = 0f;
        }
        else if (zebraCrossInRange && playerInRange)
        {
            currentState = CarState.SlowDown;

            target_velocity = playerOnZebraCross
                ? movementData.maxVelocity * movementData.hardSlowFactor
                : movementData.maxVelocity * movementData.slowFactor;
        }
        else
        {
            currentState = CarState.NormalDrive;
            target_velocity = movementData.maxVelocity;
        }
        // Debug.Log($"State: {currentState}, Target Velocity: {target_velocity}");
        carMove.SetTargetVelocity(target_velocity);
    }

    public void UpdateScannerData(bool inRange, bool giveSignal, bool onZebraCross, bool walkerLight, bool zebraCrossRange, bool carAhead)
    {
        playerInRange = inRange;
        playerGiveSignal = giveSignal;
        playerOnZebraCross = onZebraCross;
        isWalkerLightOn = walkerLight;
        zebraCrossInRange = zebraCrossRange;
        isCarAhead = carAhead;
    }
}