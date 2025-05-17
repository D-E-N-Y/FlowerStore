using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour 
{
    [SerializeField] private InputActionReference returnAction;

    void OnEnable()
    {
        returnAction.action.Enable();
        returnAction.action.started += Return;
    }

    void OnDisable()
    {
        returnAction.action.Disable();
        returnAction.action.started -= Return;
    }

    private void Return(InputAction.CallbackContext context)
    {
        if(UISystem.current.HasOpenPanels())
        {
            UISystem.current.CloseCurrentPanel();
        }
        else if(InteractionSystem.current.HasSelectedActor())
        {
            InteractionSystem.current.UnSelectActor();
        }
        else
        {
            UISystem.current.OpenLastMenu();
        }
    }
}