using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {


    public static GameInputManager Instance { get; private set; }

    public event EventHandler InteractEvent;
    public event EventHandler InteractCuttingEvent;
    public event EventHandler GamePauseEvent;
    private InputActionManager _inputActions;


    private void Awake() {
        if (Instance != null) {
            Debug.Log("there is more than 1 game input manager");
        }
        Instance = this;

        _inputActions = new InputActionManager();
        _inputActions.Player.Enable();


        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Player.InteractCutting.performed += InteractCutting_performed;
        _inputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Player.InteractCutting.performed += InteractCutting_performed;
        _inputActions.Player.Pause.performed += Pause_performed;

        _inputActions.Dispose();
    }


    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        GamePauseEvent?.Invoke(this, EventArgs.Empty);
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
