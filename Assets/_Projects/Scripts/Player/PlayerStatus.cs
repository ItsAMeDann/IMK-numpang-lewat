using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isOnZebraCross;
    public bool giveSignal;

    public void updatePlayerStatus(bool onZebraCross, bool signal)
    {
        isOnZebraCross = onZebraCross;
        giveSignal = signal;
    }
}