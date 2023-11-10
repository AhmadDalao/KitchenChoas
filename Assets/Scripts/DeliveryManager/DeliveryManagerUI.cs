using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {

    [SerializeField] private Transform _deliveryContainer;
    [SerializeField] private Transform _deliveryTemplate;


    private void Awake() {
        // hide the template on awake.
        _deliveryTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawn += Instance_OnRecipeSpawn;
        DeliveryManager.Instance.OnRecipeCompleted += Instance_OnRecipeCompleted;

        // update visual on start to prevent previous template from showing. since its empty nothing will be displayed
        UpdateVisual();
    }

    private void Instance_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void Instance_OnRecipeSpawn(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in _deliveryContainer) {

            if (child == _deliveryTemplate) continue;
            Destroy(child.gameObject);
        }


        foreach (RecipeScriptableObject recipe in DeliveryManager.Instance.GetRecipeScriptableObjectsListFromManager()) {
            Transform recipeTransform = Instantiate(_deliveryTemplate, _deliveryContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeScriptableName(recipe);
        }

    }

}
