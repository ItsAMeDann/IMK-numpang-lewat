using UnityEngine;
using UnityEngine.InputSystem;

public class TrafficLightSystem : MonoBehaviour
{
    public bool isGreenForPedestrian;
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isGreenForPedestrian = !isGreenForPedestrian;
            Debug.Log($"Traffic light changed. Green for pedestrian: {isGreenForPedestrian}");
            testWin();
        }
    }
    private void testWin()
    {
        Debug.Log("Player WIN! Pindah ke scene berikutnya...");
        GameEvents.OnLose?.Invoke();
    }
}