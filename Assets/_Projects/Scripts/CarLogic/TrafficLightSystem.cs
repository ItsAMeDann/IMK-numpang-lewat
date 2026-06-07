using UnityEngine;
using System.Collections;

public class TrafficLightSystem : MonoBehaviour
{
    public bool isGreenForPedestrian;
    public GameObject greenLightObjectCar;
    public GameObject redLightObjectCar;
    public GameObject greenLightObjectPedestrian;
    public GameObject redLightObjectPedestrian;
    public TrafficButton trafficButton;

    private Coroutine trafficLightCoroutine;

    void Start()
    {
        deactivateGreen();
    }
    public void activateTrafficLightSystem()
    {
        activateGreen();
        if (trafficLightCoroutine != null)
        {
            StopCoroutine(trafficLightCoroutine);
        }
        trafficLightCoroutine = StartCoroutine(trafficLightTimer());
    }

    private IEnumerator trafficLightTimer()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        deactivateGreen();
        trafficLightCoroutine = null; // Clear the coroutine reference
        Debug.Log("Traffic light turned red for pedestrians.");
    }

    private void activateGreen()
    {
        isGreenForPedestrian = true;
        greenLightObjectCar.SetActive(true);
        redLightObjectCar.SetActive(false);
        greenLightObjectPedestrian.SetActive(false);
        redLightObjectPedestrian.SetActive(true);
    }

    private void deactivateGreen()
    {
        isGreenForPedestrian = false;
        greenLightObjectCar.SetActive(false);
        redLightObjectCar.SetActive(true);
        greenLightObjectPedestrian.SetActive(true);
        redLightObjectPedestrian.SetActive(false);
        trafficButton.ResetButton();
    }

    // GameEvents.OnLose?.Invoke();
}