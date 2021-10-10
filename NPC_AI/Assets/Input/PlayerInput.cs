using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public InputActionsMaster InputMaster;
    private InputAction MovementAction;
    public InputEvents Events;
    
    void Awake()
    { 
        Events = new InputEvents();
        RegisterEngineEvents();
        SetupInput();
    }

    private void RegisterEngineEvents()
    {
        EngineManager.Current.Events.EveryInputUpdate += UpdateInput;
    }

    private void SetupInput()
    {
        InputMaster = new InputActionsMaster();
        InputMaster.Enable();
        InputMaster.Player.Move.Enable();
    }
    
    public void UpdateInput()
    {
        Events.Move(InputMaster.Player.Move.ReadValue<Vector2>());
    }
}

public sealed class InputEvents
{
    public Action<Vector2> Movement;

    public void Move(Vector2 value)
    {
        if(value != Vector2.zero)
            Movement?.Invoke(value);
    }
}