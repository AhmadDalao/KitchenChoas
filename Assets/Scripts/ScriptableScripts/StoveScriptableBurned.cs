using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StoveScriptableBurned : ScriptableObject {



    [Header("Kitchen Object Input")]
    [SerializeField] private KitchenObjectScriptable MeatPattyCooked;
    [Header("Kitchen Object output")]
    [SerializeField] private KitchenObjectScriptable MeatPattyBurned;
    [Header("Cooking Time Needed")]
    [SerializeField] private float MeatPattyBurnedTime;


    public KitchenObjectScriptable GetMeatPattyBurnedInput() {
        return MeatPattyCooked;
    }

    public KitchenObjectScriptable GetMeatPattyBurnedOutput() {
        return MeatPattyBurned;
    }

    public float GetBurnedTime() {
        return MeatPattyBurnedTime;
    }

}