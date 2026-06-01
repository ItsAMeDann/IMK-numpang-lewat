using UnityEngine;

public class EndZoneWin : MonoBehaviour
{
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            Debug.Log("Player entered win zone, triggering win event.");
            GameEvents.TriggerWin();
            DataManager.Instance.UnlockLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
