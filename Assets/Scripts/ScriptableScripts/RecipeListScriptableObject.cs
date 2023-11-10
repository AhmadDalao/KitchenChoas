using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// comment create asset menu to prevent creating more of this list as we only need it once.
// [CreateAssetMenu()]
public class RecipeListScriptableObject : ScriptableObject {

    [SerializeField] private List<RecipeScriptableObject> recipeScriptableLists;


    // get random recipe to use 
    public RecipeScriptableObject GetRecipeListRandom() {
        return recipeScriptableLists[Random.Range(0, recipeScriptableLists.Count)];
    }


}
