using UnityEngine;

public class CarScanner : MonoBehaviour
{
    public CarDecision decisionSystem;
    public LayerMask playerLayer;
    public LayerMask trafficLightLayer;
    public LayerMask zebraCrossLayer;
    public LayerMask carLayer;

    private bool playerInRange;
    private bool zebraCrossInRange;
    private bool playerOnZebraCross;
    private bool isWalkerLightOn;
    private bool isCarAhead;
    public CarMovementData movementData;

    void Update()
    {
        isCarAhead = IsThereCarAhead();
        if (decisionSystem != null)
        {
            decisionSystem.UpdateScannerData(playerInRange, playerOnZebraCross, isWalkerLightOn, zebraCrossInRange, isCarAhead);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"Collider detected: {other.gameObject.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = true;
            if (other.TryGetComponent<PlayerStatus>(out var status))
            {
                playerOnZebraCross = status.isOnZebraCross;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = false;
            playerOnZebraCross = false;
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
    }

    private bool IsThereCarAhead()
    {
        bool nabrak = Physics.Raycast(transform.position, transform.forward, movementData.detectionDistance, carLayer);
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, movementData.detectionDistance, carLayer);
        Debug.DrawRay(transform.position, transform.forward * movementData.detectionDistance, Color.red);

        if (hit.collider != null && hit.transform != transform)
        {
            nabrak = true;
        }
        else
        {
            nabrak = false;
        }
        return nabrak;
    }
}

