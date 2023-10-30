using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {


    public event EventHandler<KitchenObjectAddedToPlateEventArgs> KitchenObjectAddedToPlateEvent;
    public class KitchenObjectAddedToPlateEventArgs : EventArgs {
        public KitchenObjectScriptable kitchenObjectAddedToPlate;
    }


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
                // event to trigger the visual of this kitchen object which is added to the list.
                KitchenObjectAddedToPlateEvent?.Invoke(this, new KitchenObjectAddedToPlateEventArgs {
                    kitchenObjectAddedToPlate = kitchenObjectScriptable
                });
                return true;
            }
        }
        return false;
    }

    public List<KitchenObjectScriptable> GetKitchenObjectList() {
        return _kitchenObjectScriptableList;
    }


}
