using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {

    [Serializable]
    public struct KitchenObjectScriptable_GameObject {
        public KitchenObjectScriptable kitchenObjectScriptable;
        public GameObject plateGameObject;
    }

    [SerializeField] private List<KitchenObjectScriptable_GameObject> _kitchenObjectScriptableList;


    [SerializeField] private PlateKitchenObject _plateKitchenObject;


    private void Start() {
        _plateKitchenObject.KitchenObjectAddedToPlateEvent += _plateKitchenObject_KitchenObjectAddedToPlateEvent;

        foreach (KitchenObjectScriptable_GameObject item in _kitchenObjectScriptableList) {
            item.plateGameObject.gameObject.SetActive(false);
        }
    }

    private void _plateKitchenObject_KitchenObjectAddedToPlateEvent(object sender, PlateKitchenObject.KitchenObjectAddedToPlateEventArgs e) {
        foreach (KitchenObjectScriptable_GameObject item in _kitchenObjectScriptableList) {
            if (e.kitchenObjectAddedToPlate == item.kitchenObjectScriptable) {
                item.plateGameObject.gameObject.SetActive(true);
            }
        }
    }
}
