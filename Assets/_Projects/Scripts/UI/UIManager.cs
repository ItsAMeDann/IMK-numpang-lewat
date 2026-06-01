using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Wajib ditambahkan untuk pindah scene

public class UIManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    [Header("UI Skippers")]
    [SerializeField] private GameObject lock1;
    [SerializeField] private GameObject lock2;
    [SerializeField] private GameObject lock3;
    [SerializeField] private GameObject lock4;
    [Header("Data")]
    public DataManager dataManager; // Pastikan ini di-assign di Inspector

    private int selectedLevel = 1; // Default level yang dipilih

    void Start()
    {
        DefaultLevelSetup();
        UpdateLevelStates();
    }

    private void DefaultLevelSetup()
    {
        lock1.SetActive(true);
        lock2.SetActive(true);
        lock3.SetActive(true);
        lock4.SetActive(true);
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
    }

    private void UpdateLevelStates()
    {
        if (DataManager.Instance.IsLevelUnlocked(0))
        {
            lock1.SetActive(false);
            button1.interactable = true;
        }
        if (DataManager.Instance.IsLevelUnlocked(1))
        {
            lock2.SetActive(false);
            button2.interactable = true;
        }
        if (DataManager.Instance.IsLevelUnlocked(2))
        {
            lock3.SetActive(false);
            button3.interactable = true;
        }
        if (DataManager.Instance.IsLevelUnlocked(3))
        {
            lock4.SetActive(false);
            button4.interactable = true;
        }
        Debug.Log("Level states updated based on DataManager.");
        Debug.Log("Level 1 unlocked: " + DataManager.Instance.IsLevelUnlocked(0));
        Debug.Log("Level 2 unlocked: " + DataManager.Instance.IsLevelUnlocked(1));
        Debug.Log("Level 3 unlocked: " + DataManager.Instance.IsLevelUnlocked(2));
        Debug.Log("Level 4 unlocked: " + DataManager.Instance.IsLevelUnlocked(3));
    }

    public void ChangeSelectedLevel(int level)
    {
        selectedLevel = level;
    }

    public void StartGame()
    {
        Debug.Log("Memulai Game! Pindah ke Scene " + selectedLevel);
        SceneManager.LoadScene(selectedLevel);
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari aplikasi...");
        Application.Quit(); // Catatan: Ini cuma berfungsi saat di-build (.exe / .apk)
    }
}