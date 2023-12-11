using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameUI : MonoBehaviour {


    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button OptionButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            ScenesLoader.Load(ScenesLoader.Scene.MainMenuScene);
        });

        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });

        OptionButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    private void Start() {
        GameManager.Instance.HidePausedUI += Instance_HidePausedUI;
        GameManager.Instance.ShowPausedUI += Instance_ShowPausedUI;

        Hide();
    }

    private void Instance_ShowPausedUI(object sender, System.EventArgs e) {
        Show();
    }

    private void Instance_HidePausedUI(object sender, System.EventArgs e) {
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }



}
