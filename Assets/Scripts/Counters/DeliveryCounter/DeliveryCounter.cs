using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {


    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {

        if (Instance != null) {
            Debug.Log("There is more than 1 Instance of DeliveryCounter");
        }

        Instance = this;
    }

    public override void Interact(Player player) {
        // player Has a Kitchen Object
        if (player.HasKitchenObject()) {
            // player is holding something 
            // check if it is a plate kitchen object
            if (player.GetKitchenObject() is PlateKitchenObject plate) {
                // use the delivery manager to check if the plate is holding the correct ingredients on the plate.
                DeliveryManager.Instance.DeliveryRecipe(plate);
                // destroy the plate from the player hands.
                player.GetKitchenObject().DestroySelf();
            }
        }
    }


}
