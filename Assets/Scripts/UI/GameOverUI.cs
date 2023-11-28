using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI recipeDeliveredCount;


    private void Start() {
        GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        HideGameOver();
    }


    private void Instance_OnGameStateChange(object sender, System.EventArgs e) {
        if (GameManager.Instance.GetIsGameOver()) {
            ShowGameOver();
            recipeDeliveredCount.text = DeliveryManager.Instance.GetRecipeDeliveredCount().ToString();
        } else {
            HideGameOver();
        }
    }


    private void ShowGameOver() {
        gameObject.SetActive(true);
    }

    private void HideGameOver() {
        gameObject.SetActive(false);
    }

}
