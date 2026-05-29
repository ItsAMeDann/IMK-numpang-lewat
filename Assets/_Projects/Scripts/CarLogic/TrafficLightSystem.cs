using UnityEngine;
using System.Collections;

public class TrafficLightSystem : MonoBehaviour
{
    public bool isGreenForPedestrian;

    private Coroutine trafficLightCoroutine;
    public void activateTrafficLightSystem()
    {
        isGreenForPedestrian = true;
        if (trafficLightCoroutine != null)
        {
            StopCoroutine(trafficLightCoroutine);
        }
        trafficLightCoroutine = StartCoroutine(trafficLightTimer());
        Debug.Log("Traffic Light state changed: Now green");
    }

    private IEnumerator trafficLightTimer()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        isGreenForPedestrian = false; // Set to red for pedestrians
        trafficLightCoroutine = null; // Clear the coroutine reference
        Debug.Log("Traffic light turned red for pedestrians.");
    }

    // GameEvents.OnLose?.Invoke();
}