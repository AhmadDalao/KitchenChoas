using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI _recipeName;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;


    private void Awake() {
        _iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeScriptableName(RecipeScriptableObject recipeScriptableObject) {
        _recipeName.text = recipeScriptableObject.GetRecipeName();

        // clean the container
        foreach (Transform child in _iconContainer) {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }


        // get the UI icons from the list.
        foreach (KitchenObjectScriptable kitchenObjectScriptable in recipeScriptableObject.GetRecipeScriptableObject()) {
            Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectScriptable.GetKitchenObjectSprite();
        }

    }


}
