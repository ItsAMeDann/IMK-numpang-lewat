using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, Win, Lose }
    public static GameState CurrentState { get; private set; }

    public GameObject winUI;
    public GameObject loseUI;

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
    }

    private void Win()
    {
        CurrentState = GameState.Win;
        winUI.SetActive(true);
        loseUI.SetActive(false);
    }

    private void Lose()
    {
        CurrentState = GameState.Lose;
        loseUI.SetActive(true);
        winUI.SetActive(false);
    }
}