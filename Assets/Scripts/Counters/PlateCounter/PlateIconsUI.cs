using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour {

    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake() {
        _iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        _plateKitchenObject.KitchenObjectAddedToPlateEvent += _plateKitchenObject_KitchenObjectAddedToPlateEvent;
    }

    private void _plateKitchenObject_KitchenObjectAddedToPlateEvent(object sender, PlateKitchenObject.KitchenObjectAddedToPlateEventArgs e) {
        UpdatePlateIcon();
    }

    private void UpdatePlateIcon() {

        foreach (Transform child in transform) {
            Debug.Log(child.name);
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectScriptable item in _plateKitchenObject.GetKitchenObjectList()) {
            Transform iconTransform = Instantiate(_iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleTemplateUI>().SetKitchenObjectScriptable(item);
        }
    }
}

