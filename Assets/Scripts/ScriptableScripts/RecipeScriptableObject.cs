using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeScriptableObject : ScriptableObject {


    [SerializeField] private List<KitchenObjectScriptable> kitchenScriptableObject;
    [SerializeField] private string recipeName;


    public List<KitchenObjectScriptable> GetRecipeScriptableObject() {
        return kitchenScriptableObject;
    }

    public string GetRecipeName() {
        return recipeName;
    }
}
