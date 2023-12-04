using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObject {

    public static event EventHandler OnAnyDropEvent;

    public static void ResetStaticData() {
        OnAnyDropEvent = null;
    }

    [SerializeField] private Transform _spawnLocation;


    private KitchenObject _kitchenObject;


    public virtual void Interact(Player player) {
        Debug.LogError("Interact BaseCounter.cs should not be called");
    }

    public virtual void InteractCutting(Player player) {
        Debug.LogError("InteractCutting BaseCounter.cs should not be called");
    }



    public Transform GetTransformToFollowParent() {
        return _spawnLocation;
    }

    public void ClearKitchenObject() {
        this._kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this._kitchenObject = kitchenObject;

        if (_kitchenObject != null) {
            OnAnyDropEvent?.Invoke(this, EventArgs.Empty);
        }

    }

    public KitchenObject GetKitchenObject() {
        return this._kitchenObject;
    }

    public bool HasKitchenObject() {
        return this._kitchenObject != null;
    }

}
