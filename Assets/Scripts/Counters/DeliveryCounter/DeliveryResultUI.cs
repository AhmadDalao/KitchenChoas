using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour {


    private const string POPUP = "Popup";

    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image IconImage;
    [SerializeField] private TextMeshProUGUI messageResult;
    [SerializeField] private Color failedColor;
    [SerializeField] private Color successColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;


    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        BackgroundImage.color = failedColor;
        messageResult.text = "Delivery\n Failed";
        IconImage.sprite = failedSprite;
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        BackgroundImage.color = successColor;
        messageResult.text = "Delivery\n Success";
        IconImage.sprite = successSprite;
    }
}
