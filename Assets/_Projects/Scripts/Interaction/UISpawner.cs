using UnityEngine;

public class UISpawner : MonoBehaviour
{
    public Camera PlayerCamera;
    public Canvas TutorialUI;
    public TutorialData tutorialData;

    void Start()
    {
        if (TutorialUI != null && tutorialData != null)
        {
            setupTutorialUI();
        }
    }

    private void setupTutorialUI()
    {
        if (TutorialUI == null || tutorialData == null)
        {
            Debug.LogWarning("Tutorial UI or data is missing.");
            return;
        }
        TutorialUI.GetComponent<TutorialUI>().SetupTutorial(tutorialData.tutorialTitle, tutorialData.tutorialText);
        TutorialUI.gameObject.SetActive(true);
        BringUIToCamera(TutorialUI.gameObject);
    }

    public void BringUIToCamera(GameObject uiCanvas)
    {
        if (uiCanvas == null || PlayerCamera == null)
        {
            Debug.LogWarning("UI Canvas or Player Camera is missing.");
            return;
        }
        // attach the canvas to camera as camera's child
        uiCanvas.transform.SetParent(PlayerCamera.transform, false);
        uiCanvas.transform.localPosition = new Vector3(0f, 0f, 4f);
        uiCanvas.transform.localRotation = Quaternion.identity;
    }
}
