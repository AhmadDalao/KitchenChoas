using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectScriptable : ScriptableObject {
    [Header("Kitchen Object")]
    [SerializeField] private Transform KitchenObject;

    [Header("Kitchen Object Sprite")]
    [SerializeField] private Sprite KitchenObjectSprite;

    [Header("Kitchen Object Name")]
    [SerializeField] private string KitchenObjectName;


    public Transform GetKitchenObject() {
        return KitchenObject;
    }

    public Sprite GetKitchenObjectSprite() { return KitchenObjectSprite; }

}
