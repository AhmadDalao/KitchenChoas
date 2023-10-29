using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {

    public event EventHandler InteractEvent;
    public event EventHandler InteractCuttingEvent;
    private InputActionManager _inputActions;

    private void Start() {
        _inputActions = new InputActionManager();
        _inputActions.Player.Enable();
        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Player.InteractCutting.performed += InteractCutting_performed;
    }

    private void InteractCutting_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        InteractCuttingEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        InteractEvent?.Invoke(this, EventArgs.Empty);
    }



    public Vector2 GetPlayerMovementVector2Normalized() {
        Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
        return input.normalized;
    }
}
