using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI _countDownText;


    private void Start() {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        Hide();
    }

    private void Instance_OnGameStateChange(object sender, EventArgs e) {

        if (GameManager.Instance.IsWaitingToPlayCountDown()) {
            Show();
        } else {
            Hide();
        }

    }

    private void Update() {
        _countDownText.text = Mathf.Ceil(GameManager.Instance.GetWaitingToPlayCountDown()).ToString();
    }


    private void Show() {
        _countDownText.gameObject.SetActive(true);
    }

    private void Hide() {
        _countDownText.gameObject.SetActive(false);
    }

}
