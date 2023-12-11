using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {

    private const string POP_UP = "CountDownPopUp";

    [SerializeField] private TextMeshProUGUI _countDownText;
    private Animator _animator;
    private int _previousCountDownNumber;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

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
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetWaitingToPlayCountDown());
        _countDownText.text = countDownNumber.ToString();

        if (_previousCountDownNumber != countDownNumber) {
            _previousCountDownNumber = countDownNumber;
            _animator.SetTrigger(POP_UP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }


    private void Show() {
        _countDownText.gameObject.SetActive(true);
    }

    private void Hide() {
        _countDownText.gameObject.SetActive(false);
    }

}
