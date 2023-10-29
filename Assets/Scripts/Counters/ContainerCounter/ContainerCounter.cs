using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler ContainerAnimationEvent;

    [SerializeField] private KitchenObjectScriptable _kitchenObjectScriptable;

    public override void Interact(Player player) {
        // ClearCounter has NO kitchen object on top
        if (!HasKitchenObject()) {
            if (!player.HasKitchenObject()) {
                KitchenObject.SpawnKitchenObject(_kitchenObjectScriptable, player);
                ContainerAnimationEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }



}
