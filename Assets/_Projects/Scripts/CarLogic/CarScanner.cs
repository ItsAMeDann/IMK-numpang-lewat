using UnityEngine;

public class CarScanner : MonoBehaviour
{
    public CarDecision decisionSystem;
    public LayerMask playerLayer;
    public LayerMask trafficLightLayer;

    private bool playerInRange;
    private bool playerOnRoad;
    private bool isZebraCross;
    private bool isWalkerLightOn;

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = true;
            if (other.TryGetComponent<PlayerStatus>(out var status))
            {
                playerOnRoad = status.isOnRoad;
                isZebraCross = status.isOnZebraCross;
            }
        }

        if (((1 << other.gameObject.layer) & trafficLightLayer) != 0)
        {
            if (other.TryGetComponent<TrafficLightSystem>(out var light))
            {
                isWalkerLightOn = light.isGreenForPedestrian;
            }
        }

        if (decisionSystem != null)
        {
            decisionSystem.UpdateScannerData(playerInRange, playerOnRoad, isZebraCross, isWalkerLightOn);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = false;
            playerOnRoad = false;
            isZebraCross = false;
        }
        
        if (((1 << other.gameObject.layer) & trafficLightLayer) != 0)
        {
            isWalkerLightOn = false;
        }

        if (decisionSystem != null)
        {
            decisionSystem.UpdateScannerData(playerInRange, playerOnRoad, isZebraCross, isWalkerLightOn);
        }
    }
}

