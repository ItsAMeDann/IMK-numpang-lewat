using UnityEngine;

public class TrafficButton : MonoBehaviour
{
    public Material offMaterial;
    public Material onMaterial;

    public void PressButton()
    {
        GetComponent<MeshRenderer>().material = onMaterial;
        AudioManager.Instance.Play("Interaction_bel");
    }
    public void ResetButton()
    {
        GetComponent<MeshRenderer>().material = offMaterial;
        AudioManager.Instance.Play("Interaction_bel");
    }

}
