using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [Header("Scriptable Reference")]
    [SerializeField] private KitchenObjectScriptable _kitchenObjectScriptable;


    private BaseCounter _currentBaseCounter;

    public KitchenObjectScriptable GetKitchenObjectScriptable() {
        return _kitchenObjectScriptable;
    }


    public void SetKitchenObjectParent(BaseCounter newBaseCounter) {
        if (_currentBaseCounter != null) {
            _currentBaseCounter.ClearKitchenObject();
        }

        _currentBaseCounter = newBaseCounter;

        if (newBaseCounter.HasKitchenObject()) {
            Debug.LogError("Counter already has a kitchen object on top");
        }

        newBaseCounter.SetKitchenObject(this);

        transform.parent = newBaseCounter.GetTransformToFollowParent();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf() {
        _currentBaseCounter.ClearKitchenObject();
        Destroy(this.gameObject);
    }


    public static void SpawnKitchenObject(KitchenObjectScriptable kitchenObjectScriptable, BaseCounter parent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectScriptable.GetKitchenObject());
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(parent);
    }

}


