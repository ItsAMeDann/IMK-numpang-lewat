using UnityEngine;

// Pasang script ini pada GameObject Area Collider untuk WIN
// Pastikan Layer Player sudah di-set, dan Collider di-set sebagai "Is Trigger"
public class WinZone : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager tidak ditemukan di scene!");
        }
    }

    // Ketika Player masuk ke area WIN
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player masuk WinZone!");
            gameManager.winTriggered.Invoke();
        }
    }
}