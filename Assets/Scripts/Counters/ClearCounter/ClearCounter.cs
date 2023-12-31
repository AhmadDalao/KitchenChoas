using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {


    public override void Interact(Player player) {
        //
        // ClearCounter has NO kitchen object on top
        //
        if (!HasKitchenObject()) {
            // player Has a Kitchen Object
            if (player.HasKitchenObject()) {
                // place the Kitchen Object on top of the counter.
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            //
            // ClearCounter Has Kitchen Object on Top !!
            //
            // check if player is holding anything 
            if (player.HasKitchenObject()) {
                // player is holding something 
                // check if it is a plate kitchen object
                if (player.GetKitchenObject() is PlateKitchenObject plate) {
                    // try to add the kitchen object from counter to plate
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectScriptable())) {
                        // destroy the kitchen object visual from the counter.
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    // player is  holding something that is not a plate get it from him
                    PlateKitchenObject _plate = GetKitchenObject() as PlateKitchenObject;
                    // check if player holding a kitchen object you can add to the plate.
                    if (_plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectScriptable())) {
                        // Destroy the kitchen object that the plater had
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                // give the player the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }


}
