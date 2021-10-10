using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionsMaster InputMaster;
    private InputAction MovementAction;
    
    void Awake()
    {
        SetupInput();
    }

    private void SetupInput()
    {
        InputMaster = new InputActionsMaster();
        InputMaster.Enable();
        MovementAction = InputMaster.Player.Move;

    }
    
    
}
