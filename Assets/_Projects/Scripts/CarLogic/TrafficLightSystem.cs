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
    public float greenLightDuration = 20f;

    private Coroutine trafficLightCoroutine;

    void Start()
    {
        deactivateGreen();
    }
    public void activateTrafficLightSystem()
    {
        if (trafficLightCoroutine != null)
        {
            return;
            // StopCoroutine(trafficLightCoroutine);
        }
        activateGreen();
        trafficLightCoroutine = StartCoroutine(trafficLightTimer());
    }

    private IEnumerator trafficLightTimer()
    {
        yield return new WaitForSeconds(greenLightDuration); // Wait for the specified duration
        deactivateGreen();
        trafficLightCoroutine = null; // Clear the coroutine reference
        Debug.Log("Traffic light turned red for pedestrians.");
    }

    private void activateGreen()
    {
        isGreenForPedestrian = true;
        greenLightObjectCar.SetActive(false);
        redLightObjectCar.SetActive(true);
        greenLightObjectPedestrian.SetActive(true);
        redLightObjectPedestrian.SetActive(false);
        trafficButton.PressButton();
    }

    private void deactivateGreen()
    {
        isGreenForPedestrian = false;
        greenLightObjectCar.SetActive(true);
        redLightObjectCar.SetActive(false);
        greenLightObjectPedestrian.SetActive(false);
        redLightObjectPedestrian.SetActive(true);
        trafficButton.ResetButton();
    }

    // GameEvents.OnLose?.Invoke();
}