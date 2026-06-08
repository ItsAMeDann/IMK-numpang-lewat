using UnityEngine;

public class CarDecision : MonoBehaviour
{
    public enum CarState { NormalDrive, SlowDown, FullStop }

    public CarMovementData movementData;
    public CarMove carMove;
    public string klaksonName = "Klakson_def";
    public float klaksonCooldown = 8f;
    [Range(0, 1)] public float slow_factor = 0.5f;
    [Range(0, 1)] public float hard_slow_factor = 0.2f;

    public CarState currentState = CarState.NormalDrive;
    private float target_velocity;
    private float klaksonTimer = 0f;

    private bool playerInRange, playerGiveSignal, playerOnZebraCross, isWalkerLightOn, zebraCrossInRange, isCarAhead;

    void Start()
    {
        AudioManager.Instance.Play("Car_engine", transform);
    }

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
            HandleKlakson(klaksonCooldown + 5f);
            HandleRem(klaksonCooldown + 5f);
        }
        else if (isWalkerLightOn || (playerInRange && playerOnZebraCross) || (playerInRange && playerGiveSignal))
        {
            currentState = CarState.FullStop;
            target_velocity = 0f;
            HandleKlakson(klaksonCooldown);
            HandleRem(klaksonCooldown);
        }
        else if (zebraCrossInRange && playerInRange)
        {
            currentState = CarState.SlowDown;
            HandleKlakson(klaksonCooldown - 1f); // Slightly shorter cooldown for slowing down

            target_velocity = playerOnZebraCross
                ? movementData.maxVelocity * movementData.hardSlowFactor
                : movementData.maxVelocity * movementData.slowFactor;
        }
        else
        {
            currentState = CarState.NormalDrive;
            target_velocity = movementData.maxVelocity;
            HandleKenceng(klaksonCooldown + 4f);
        }
        // Debug.Log($"State: {currentState}, Target Velocity: {target_velocity}");
        carMove.SetTargetVelocity(target_velocity);
    }

    private void HandleRem(float remCooldown)
    {
        if (klaksonTimer <= 0f)
        {
            AudioManager.Instance.Play("Car_rem", transform);
            klaksonTimer = remCooldown;
        }
        else
        {
            klaksonTimer -= Time.deltaTime;
        }
    }

    private void HandleKenceng(float kencengCooldown)
    {
        if (klaksonTimer <= 0f)
        {
            AudioManager.Instance.Play("Car_kenceng", transform);
            klaksonTimer = kencengCooldown;
        }
        else
        {
            klaksonTimer -= Time.deltaTime;
        }
    }

    private void HandleKlakson(float klaksonCooldown)
    {
        if (klaksonTimer <= 0f)
        {
            AudioManager.Instance.Play(klaksonName, transform);
            klaksonTimer = klaksonCooldown;
        }
        else
        {
            klaksonTimer -= Time.deltaTime;
        }
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