using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI countDownText;


    private void Start() {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        HideCountDown();
    }

    private void Instance_OnGameStateChange(object sender, EventArgs e) {
        if (GameManager.Instance.GetGameStateIsCountDown()) {
            ShowCountDown();
        } else {
            HideCountDown();
        }
    }

    private void Update() {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetGameCountDownTimer()).ToString();
    }

    private void ShowCountDown() {
        countDownText.gameObject.SetActive(true);
    }

    private void HideCountDown() {
        countDownText.gameObject.SetActive(false);
    }

}
