using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    // Example: level unlock data
    public bool[] levelUnlocked = new bool[5];

    private void Awake()
    {
        // Singleton check
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Unlock first level by default
        levelUnlocked[0] = true;
    }

    public bool IsLevelUnlocked(int level)
    {
        return levelUnlocked[level];
    }

    public void UnlockLevel(int level)
    {
        levelUnlocked[level] = true;
    }
}