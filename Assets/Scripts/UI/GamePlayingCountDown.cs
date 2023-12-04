using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingCountDown : MonoBehaviour {


    [SerializeField] private Image _playingTimeCounterImage;
    [SerializeField] private GameObject _counterContainer;

    private void Awake() {
        _playingTimeCounterImage.fillAmount = 0f;
    }

    private void Start() {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        Hide();
    }

    private void Update() {
        _playingTimeCounterImage.fillAmount = GameManager.Instance.GetGamePlayingCountDown();
    }

    private void Instance_OnGameStateChange(object sender, System.EventArgs e) {

        if (GameManager.Instance.IsPlayingTimeState()) {
            Show();
        } else {
            Hide();
        }

    }

    private void Show() {
        _counterContainer.gameObject.SetActive(true);
    }

    private void Hide() {
        _counterContainer.gameObject.SetActive(false);
    }




}
