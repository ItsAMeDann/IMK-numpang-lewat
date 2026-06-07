using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI titleLabel;
    public TextMeshProUGUI textLabel;
    private string[] tutorialTexts;
    public void SetupTutorial(string title, string[] text)
    {
        titleLabel.text = title;
        textLabel.text = text[0];
        tutorialTexts = text;
    }
    public void NextPage()
    {
        // Get the current page number from the text label
        string currentText = textLabel.text;
        int currentPage = System.Array.IndexOf(tutorialTexts, currentText);
        if (currentPage < tutorialTexts.Length - 1)
        {
            textLabel.text = tutorialTexts[currentPage + 1];
            AudioManager.Instance.Play("Interaction_positive");
        }
        else
        {
            gameObject.SetActive(false);
            AudioManager.Instance.Play("Interaction_negative");
        }
    }
}
