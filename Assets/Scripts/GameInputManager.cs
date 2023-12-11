using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {


    [SerializeField] private const string PLAYER_PREF_BINDING = "InputBinding";

    public static GameInputManager Instance { get; private set; }

    public event EventHandler InteractEvent;
    public event EventHandler InteractCuttingEvent;
    public event EventHandler GamePauseEvent;
    public event EventHandler OnKeyBinding;


    private InputActionManager _inputActions;



    public enum Binding {
        Move_up,
        Move_down,
        Move_left,
        Move_right,
        Interact,
        InteractCutting,
        Pause
    }

    private void Awake() {
        if (Instance != null) {
            Debug.Log("there is more than 1 game input manager");
        }
        Instance = this;

        _inputActions = new InputActionManager();


        if (PlayerPrefs.HasKey(PLAYER_PREF_BINDING)) {
            _inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREF_BINDING));
        }


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


    public string GetBindingText(Binding binding) {
        switch (binding) {
            default:
            case Binding.Move_up:
                return _inputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_down:
                return _inputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_left:
                return _inputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_right:
                return _inputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return _inputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractCutting:
                return _inputActions.Player.InteractCutting.bindings[0].ToDisplayString();
            case Binding.Pause:
                return _inputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }


    public void SetBinding(Binding binding, Action onAction) {
        _inputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding) {
            default:
            case Binding.Move_up:
                inputAction = _inputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_down:
                inputAction = _inputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_left:
                inputAction = _inputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_right:
                inputAction = _inputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = _inputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractCutting:
                inputAction = _inputActions.Player.InteractCutting;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = _inputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }


        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete((callback) => {

            callback.Dispose();

            _inputActions.Player.Enable();

            onAction();

            PlayerPrefs.SetString(PLAYER_PREF_BINDING, _inputActions.SaveBindingOverridesAsJson());

            PlayerPrefs.Save();

            OnKeyBinding?.Invoke(this, EventArgs.Empty);


        }).Start();
    }

}
