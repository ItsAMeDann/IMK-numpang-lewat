using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Win, Lose }
    public static GameState CurrentState { get; private set; }

    public GameObject winUI;
    public GameObject loseUI;
    public CarSpawner carSpawner;
    public UISpawner uISpawner;

    private void OnEnable()
    {
        GameEvents.OnWin += Win;
        GameEvents.OnLose += Lose;
    }

    private void OnDisable()
    {
        GameEvents.OnWin -= Win;
        GameEvents.OnLose -= Lose;
    }

    private void Start()
    {
        CurrentState = GameState.Playing;
        AudioManager.Instance.Play("CityBGM");
    }

    private void Win()
    {
        CurrentState = GameState.Win;
        if (winUI == null)
        {
            Debug.Log("Activating win end UI.");
            return;
        }
        winUI.SetActive(true);
        loseUI.SetActive(false);
        carSpawner.StopSpawning();
        uISpawner.BringUIToCamera(winUI);
    }

    private void Lose()
    {
        CurrentState = GameState.Lose;
        if (loseUI == null)
        {
            Debug.Log("Activating lose end UI.");
            return;
        }
        loseUI.SetActive(true);
        winUI.SetActive(false);
        carSpawner.StopSpawning();
        uISpawner.BringUIToCamera(loseUI);
    }

    public void RestartGame()
    {
        AudioManager.Instance.Play("Interaction_negative");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        AudioManager.Instance.Play("Interaction_negative");
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        AudioManager.Instance.Play("Interaction_positive");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void LoadNextLevel()
    {
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        AudioManager.Instance.Play("Interaction_positive");
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load.");
        }
    }
}