using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCounter : BaseCounter {


    public static event EventHandler OnTrashEvent;

    public override void Interact(Player player) {
        // ClearCounter has NO kitchen object on top
        if (!HasKitchenObject()) {
            // player Has a Kitchen Object
            if (player.HasKitchenObject()) {
                // place the Kitchen Object on top of the counter.
                player.GetKitchenObject().DestroySelf();
                // trigger event to play sound
                OnTrashEvent?.Invoke(this, EventArgs.Empty);

            } else {
                // player is not holding anything
            }
        }
    }



}
