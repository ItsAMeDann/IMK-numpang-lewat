using UnityEngine;

public class CarCrashDetection : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Player hit by car, triggering lose event.");
            AudioManager.Instance.Play("Car_tabrak", transform);
            GameEvents.TriggerLose();
        }
    }
}