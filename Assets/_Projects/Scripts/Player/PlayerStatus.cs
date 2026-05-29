using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isOnZebraCross;
    public bool giveSignal;

    void Awake()
    {
        transform.position = new Vector3(
            transform.position.x,
            1.25f,
            transform.position.z
        );
    }

    public void updatePlayerStatus(bool onZebraCross, bool signal)
    {
        isOnZebraCross = onZebraCross;
        giveSignal = signal;
    }
}