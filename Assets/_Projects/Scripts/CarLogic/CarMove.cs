using UnityEngine;

public class CarMove : MonoBehaviour
{
    [Header("Data Reference")]
    public CarMovementData data;

    private float currentVelocity;
    private float targetVelocity;

    public void SetTargetVelocity(float v)
    {
        targetVelocity = v;
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        float force = (targetVelocity < currentVelocity)
            ? data.brakingForce
            : data.accelerationForce;

        currentVelocity = Mathf.MoveTowards(
            currentVelocity,
            targetVelocity,
            force * Time.deltaTime
        );

        transform.Translate(Vector3.forward * currentVelocity * Time.deltaTime);
    }
}