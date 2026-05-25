using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDecision : MonoBehaviour
{
    public LayerMask zebraCrossLayer;
    public PlayerStatus playerStatus;
    private bool isPlayerOnZebraCross, isPlayerGivingSignal;

    [Header("VR Input References")]
    public InputActionReference leftGripAction;
    public InputActionReference rightGripAction;

    void Update()
    {
        isPlayerGivingSignal = isOpenPalm();
        if (playerStatus != null)
        {
            playerStatus.updatePlayerStatus(isPlayerOnZebraCross, isPlayerGivingSignal);
        }
    }

    // This runs when the player enters the Zebra Cross collider
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & zebraCrossLayer) != 0)
        {
            isPlayerOnZebraCross = true;
            Debug.Log($"Zebra cross in range: {isPlayerOnZebraCross}");
        }
    }

    // This runs when the player leaves the Zebra Cross collider
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & zebraCrossLayer) != 0)
        {
            isPlayerOnZebraCross = false;
            Debug.Log("Player left the Zebra Crossing.");
        }
    }

    private bool isOpenPalm()
    {
        // Read the "Squeeze" value of both controllers (0 to 1)
        float leftGrip = leftGripAction.action.ReadValue<float>();
        float rightGrip = rightGripAction.action.ReadValue<float>();

        // If either hand is open (not gripping), set giveSignal to true
        // We use < 0.1f to allow for a tiny bit of resting pressure
        if (leftGrip > 0.9f || rightGrip > 0.9f)
        {
            return true;
        }
        else
        {
            return false;
        }
        // Debug.Log($"Player giving signal: {isPlayerGivingSignal}");
    }
}