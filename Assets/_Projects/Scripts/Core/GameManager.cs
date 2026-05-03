using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Events")]
    public UnityEvent winTriggered;
    public UnityEvent loseTriggered;

    private void Start()
    {
        // Connect signals ke fungsi yang sesuai
        winTriggered.AddListener(hasWin);
        loseTriggered.AddListener(hasLose);
    }

    // Dipanggil ketika pemain menang → pindah ke scene berikutnya
    public void hasWin()
    {
        Debug.Log("Player WIN! Pindah ke scene berikutnya...");
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            // Kalau sudah scene terakhir, kembali ke scene 0 (menu utama)
            SceneManager.LoadScene(0);
        }
    }

    // Dipanggil ketika pemain kalah → reload scene yang sekarang
    public void hasLose()
    {
        Debug.Log("Player LOSE! Reload scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}