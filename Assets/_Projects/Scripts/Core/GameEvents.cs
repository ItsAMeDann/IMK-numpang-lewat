using System;

public static class GameEvents
{
    public static Action OnWin;
    public static Action OnLose;
    public static void TriggerWin()
    {
        OnWin?.Invoke();
    }
    public static void TriggerLose()
    {
        OnLose?.Invoke();
    }
}