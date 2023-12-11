using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {


    public static OptionsUI Instance { get; private set; }

    [SerializeField] private GameObject pressToRebindObject;

    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button closeOptionButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI soundText;

    [Header("Button Binding")]
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactCuttingButton;
    [SerializeField] private Button pauseButton;

    [Header("Key Binding Text")]
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactCuttingText;
    [SerializeField] private TextMeshProUGUI pauseText;


    private Action _onButtonCloseAction;

    private void Awake() {

        if (Instance != null) {
            Debug.Log("more than 1 optionsUI");
        }

        Instance = this;


        musicButton.onClick.AddListener(() => {
            BackgroundMusic.Instance.ChangeMusicVolume();
            UpdateVisual();
        });

        soundEffectButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });


        closeOptionButton.onClick.AddListener(() => {
            Hide();
            UpdateVisual();
            _onButtonCloseAction();
        });

        //**
        //
        //
        //
        //
        //================================ Key Binding Listeners ================================
        //
        //
        //
        //**// 

        moveUpButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.Move_up);
        });

        moveDownButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.Move_down);
        });

        moveRightButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.Move_right);
        });

        moveLeftButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.Move_left);
        });

        interactButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.Interact);
        });

        interactCuttingButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.InteractCutting);
        });

        pauseButton.onClick.AddListener(() => {
            RebiddingBiding(GameInputManager.Binding.Pause);
        });

    }

    private void Start() {

        GameManager.Instance.HidePausedUI += Instance_HidePausedUI;

        UpdateVisual();
        HidePressToRebind();
        Hide();
    }

    private void Instance_HidePausedUI(object sender, System.EventArgs e) {
        // hide the options when the game is resumed;
        Hide();
    }

    private void UpdateVisual() {
        soundText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(BackgroundMusic.Instance.GetVolume() * 10f);


        moveUpText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_up);
        moveDownText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_down);
        moveLeftText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_left);
        moveRightText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Move_right);


        interactText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Interact);
        interactCuttingText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.InteractCutting);
        pauseText.text = GameInputManager.Instance.GetBindingText(GameInputManager.Binding.Pause);



    }


    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show(Action onButtonCloseAction) {
        _onButtonCloseAction = onButtonCloseAction;

        gameObject.SetActive(true);
        soundEffectButton.Select();
    }



    private void ShowPressToRebind() {
        pressToRebindObject.gameObject.SetActive(true);
    }

    private void HidePressToRebind() {
        pressToRebindObject.gameObject.SetActive(false);
    }


    private void RebiddingBiding(GameInputManager.Binding binding) {
        ShowPressToRebind();
        GameInputManager.Instance.SetBinding(binding, () => {
            HidePressToRebind();
            UpdateVisual();
        });
    }

}
