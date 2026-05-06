using UnityEngine;

public class CarScanner : MonoBehaviour
{
    public CarDecision decisionSystem;
    public LayerMask playerLayer;
    public LayerMask trafficLightLayer;
    public LayerMask zebraCrossLayer;
    public LayerMask carLayer;

    private bool playerInRange;
    private bool playerGiveSignal;
    private bool zebraCrossInRange;
    private bool playerOnZebraCross;
    private bool isWalkerLightOn;
    private bool isCarAhead;
    public CarMovementData movementData;

    void Update()
    {
        if (decisionSystem != null)
        {
            Debug.Log($"Scanner Data - PlayerInRange: {playerInRange}, PlayerGiveSignal: {playerGiveSignal}, PlayerOnZebraCross: {playerOnZebraCross}, WalkerLightGreen: {isWalkerLightOn}, ZebraCrossInRange: {zebraCrossInRange}, CarAhead: {isCarAhead}");
            decisionSystem.UpdateScannerData(playerInRange, playerGiveSignal, playerOnZebraCross, isWalkerLightOn, zebraCrossInRange, isCarAhead);
        }
        isCarAhead = IsThereCarAhead();
    }

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log($"Collider detected: {other.gameObject.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = true;
            if (other.TryGetComponent<PlayerStatus>(out var status))
            {
                playerOnZebraCross = status.isOnZebraCross;
                playerGiveSignal = status.giveSignal;
                // Debug.Log($"Player on zebra cross: {playerOnZebraCross}");
            }
            // Debug.Log($"Player in range: {playerInRange}");
        }

        if (((1 << other.gameObject.layer) & zebraCrossLayer) != 0)
        {
            zebraCrossInRange = true;
            // Debug.Log($"Zebra cross in range: {zebraCrossInRange}");
        }

        if (((1 << other.gameObject.layer) & trafficLightLayer) != 0)
        {
            if (other.TryGetComponent<TrafficLightSystem>(out var light))
            {
                isWalkerLightOn = light.isGreenForPedestrian;
                // Debug.Log($"Walker light green: {isWalkerLightOn}");
            }
        }

        // if (((1 << other.gameObject.layer) & carLayer) != 0)
        // {
        //     // Debug.Log($"Car detected ahead: {other.gameObject.name}");
        //     // Check if it has the same rotation or not
        //     if (Vector3.Dot(other.transform.forward, transform.forward) > 0.5f)
        //     {
        //         isCarAhead = true;
        //         // Debug.Log($"Car ahead: {isCarAhead}");
        //     }
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = false;
            playerOnZebraCross = false;
            playerGiveSignal = false;
            // Debug.Log($"Player in range: {playerInRange}, on zebra cross: {playerOnZebraCross}");
        }

        if (((1 << other.gameObject.layer) & trafficLightLayer) != 0)
        {
            isWalkerLightOn = false;
            // Debug.Log($"Walker light green: {isWalkerLightOn}");
        }

        if (((1 << other.gameObject.layer) & zebraCrossLayer) != 0)
        {
            zebraCrossInRange = false;
            // Debug.Log($"Zebra cross in range: {zebraCrossInRange}");
        }

        // if (((1 << other.gameObject.layer) & carLayer) != 0)
        // {
        //     isCarAhead = false;
        //     // Debug.Log($"Car ahead: {isCarAhead}");
        // }
    }

    private bool IsThereCarAhead()
    {
        return Physics.Raycast(transform.position, transform.forward, movementData.detectionDistance, carLayer);
    }
}

