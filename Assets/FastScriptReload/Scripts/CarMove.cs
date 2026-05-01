using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float max_velocity; 

    private float current_velocity; 

    void Start()
    {
        current_velocity = max_velocity; 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * current_velocity * Time.deltaTime);

    }
}