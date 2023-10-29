using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public override void Interact(Player player) {
        // ClearCounter has NO kitchen object on top
        if (!HasKitchenObject()) {
            // player Has a Kitchen Object
            if (player.HasKitchenObject()) {
                // place the Kitchen Object on top of the counter.
                player.GetKitchenObject().DestroySelf();
            } else {
                // player is not holding anything
            }
        }
    }



}
