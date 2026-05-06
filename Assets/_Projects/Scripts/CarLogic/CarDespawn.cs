using UnityEngine;
using System.Collections;

public class CarDespawn : MonoBehaviour
{
    [SerializeField] private float despawnTime = 30f;
    [SerializeField] private Transform playerCamera;
    private float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        if (!isPlayerLooking() && !isPlayerClose())
        {
            timer += Time.deltaTime;
        }

        if (timer >= despawnTime)
        {
            despawn();
        }
    }

    public void setPlayerCamera(Transform cameraTransform)
    {
        playerCamera = cameraTransform;
    }

    bool isPlayerLooking()
    {
        Vector3 toCar = (transform.position - playerCamera.position).normalized;
        float angle = Vector3.Angle(playerCamera.forward, toCar);
        return angle < 30f; // Consider looking if within 30 degrees
    }

    bool isPlayerClose()
    {
        float distance = Vector3.Distance(transform.position, playerCamera.position);
        return distance < 15f; // Consider close if within 15 units
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}