using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ditambahkan untuk pindah scene

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject levelInfoPanel; // Masukkan Right Panel (Level 1 Info) ke sini
    
    // Nanti kamu bisa tambahkan panel lain di sini
    // public GameObject settingsPanel; 
    // public GameObject tutorialPanel;

    void Start()
    {
        // Pastikan saat game mulai, panel info level disembunyikan
        if (levelInfoPanel != null)
        {
            levelInfoPanel.SetActive(false);
        }
    }

    // --- FUNGSI UNTUK PANEL LEVEL ---

    public void OpenLevelInfo()
    {
        levelInfoPanel.SetActive(true);
    }

    public void CloseLevelInfo()
    {
        levelInfoPanel.SetActive(false);
    }

    // --- FUNGSI UNTUK MENU UTAMA ---

    public void StartGame()
    {
        Debug.Log("Memulai Game! Pindah ke Scene Gameplay...");
        
        // Hapus tanda // di bawah ini kalau scene gameplay kamu udah siap
        // SceneManager.LoadScene("NamaSceneGameplayKamu"); 
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari aplikasi...");
        Application.Quit(); // Catatan: Ini cuma berfungsi saat di-build (.exe / .apk)
    }
}