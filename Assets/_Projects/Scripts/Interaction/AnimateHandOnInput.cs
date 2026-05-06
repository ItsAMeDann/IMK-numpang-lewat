using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty triggerValue;
    public InputActionProperty actionValue;
    public Animator handAnimator;
    void Start()
    {
        if (handAnimator == null)
        {
            handAnimator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        float trigger = triggerValue.action.ReadValue<float>();
        float action = actionValue.action.ReadValue<float>();

        handAnimator.SetFloat("Grip", trigger);
        handAnimator.SetFloat("Trigger", action);
    }

}
