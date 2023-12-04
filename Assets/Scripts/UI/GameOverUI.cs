using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _recipeDeliveredNumber;

    private void Start() {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        Hide();
    }


    private void Instance_OnGameStateChange(object sender, System.EventArgs e) {

        if (GameManager.Instance.IsGameOver()) {
            _recipeDeliveredNumber.text = DeliveryManager.Instance.GetRecipeDeliveredCount().ToString();
            Show();
        } else {
            Hide();
        }

    }

    private void Show() {
        this.gameObject.SetActive(true);
    }

    private void Hide() {
        this.gameObject.SetActive(false);
    }



}
