using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectScriptable> _validKitchenObjectToAdd;
    private List<KitchenObjectScriptable> _kitchenObjectScriptableList;


    private void Awake() {
        _kitchenObjectScriptableList = new List<KitchenObjectScriptable>();
    }

    public bool TryAddIngredient(KitchenObjectScriptable kitchenObjectScriptable) {
        // check if the kitchen object scriptable is indeed valid to add to the list
        if (_validKitchenObjectToAdd.Contains(kitchenObjectScriptable)) {
            // now check if the kitchen object exist in the list or not.
            if (!_kitchenObjectScriptableList.Contains(kitchenObjectScriptable)) {
                // add the kitchen object to the plate kitchen object array to display it later on top of it.
                _kitchenObjectScriptableList.Add(kitchenObjectScriptable);
                return true;
            }
        }
        return false;
    }


}
