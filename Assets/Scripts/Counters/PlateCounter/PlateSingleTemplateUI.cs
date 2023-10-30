using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlateSingleTemplateUI : MonoBehaviour {

    [SerializeField] private Image _icon;

    public void SetKitchenObjectScriptable(KitchenObjectScriptable _kitchenObjectScriptable) {
        _icon.sprite = _kitchenObjectScriptable.GetKitchenObjectSprite();
    }


}
