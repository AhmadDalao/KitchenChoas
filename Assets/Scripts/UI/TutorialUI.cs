using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI MoveTopKeyText;
    [SerializeField] private TextMeshProUGUI MoveDownKeyText;
    [SerializeField] private TextMeshProUGUI MoveRightKeyText;
    [SerializeField] private TextMeshProUGUI MoveLeftKeyText;
    [SerializeField] private TextMeshProUGUI MoveInteractKeyText;
    [SerializeField] private TextMeshProUGUI MoveInteractCuttingKeyText;
    [SerializeField] private TextMeshProUGUI PauseKeyText;


    private void Start() {
        GameInputManager.Instance.OnKeyBinding += Instance_OnKeyBinding;
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        UpdateVisual();
        Show();
    }

    private void Instance_OnGameStateChange(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsWaitingToPlayCountDown()) {
            Hide();
        }
    }

    private void Instance_OnKeyBinding(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {

        MoveTopKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_up);
        MoveDownKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_down);
        MoveRightKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_right);
        MoveLeftKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_left);


        MoveInteractKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Interact);
        MoveInteractCuttingKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.InteractCutting);
        PauseKeyText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Pause);

    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}
