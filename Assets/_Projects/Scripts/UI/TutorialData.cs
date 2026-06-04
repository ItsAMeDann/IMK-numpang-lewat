using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "Scriptable Objects/TutorialData")]
public class TutorialData : ScriptableObject
{
    [Header("Tutorial Settings")]
    public string tutorialTitle;
    public string[] tutorialText;
}
