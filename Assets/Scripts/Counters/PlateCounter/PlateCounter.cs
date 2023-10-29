using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter {

    public event EventHandler SpawnPlateOnCounterEvent;
    public event EventHandler PlatePickedUpEvent;


    [SerializeField] private KitchenObjectScriptable _kitchenObjectScriptable;

    private float _plateSpawnTimer;
    private float _plateSpawnLimitMax = 4f;
    private int _plateSpawnMax = 4;
    private int _plateSpawnCount;

    private void Update() {
        HandleSpawnPlate();
    }

    private void HandleSpawnPlate() {
        // get time to spawn a plate.
        _plateSpawnTimer += Time.deltaTime;
        // check if the timer is larger than the limit specified
        if (_plateSpawnTimer > _plateSpawnLimitMax) {
            // reset the timer for the next spawn
            _plateSpawnTimer = 0;
            // check if you can spawn a plate
            if (_plateSpawnCount < _plateSpawnMax) {
                // increase the plate count
                _plateSpawnCount++;
                // trigger the event spawn a prefab visual 
                SpawnPlateOnCounterEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }



    public override void Interact(Player player) {
        // player has NO kitchen object on hands
        if (!player.HasKitchenObject()) {
            // check if plate spawn count is greater than 0 . which means there is a plate on top of counter
            if (_plateSpawnCount > 0) {
                // detect from the spawn count
                _plateSpawnCount--;
                // spawn a kitchen object scriptable and give it to player
                KitchenObject.SpawnKitchenObject(_kitchenObjectScriptable, player);
                // trigger the event of picking up a plate.
                PlatePickedUpEvent?.Invoke(this, EventArgs.Empty);
            }
        }
    }



}
