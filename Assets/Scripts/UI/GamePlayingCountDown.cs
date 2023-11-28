using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingCountDown : MonoBehaviour {


    [SerializeField] private Image gameCountDown;


    private void Start() {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        gameCountDown.fillAmount = 0f;
        Hide();
    }


    private void Instance_OnGameStateChange(object sender, System.EventArgs e) {
        if (GameManager.Instance.GetIsPlaying()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        gameCountDown.fillAmount = GameManager.Instance.GetGamePlayingTimer();

    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}
