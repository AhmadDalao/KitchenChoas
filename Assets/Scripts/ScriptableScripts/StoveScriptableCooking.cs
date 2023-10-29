using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class StoveScriptableCooking : ScriptableObject {



    [Header("Kitchen Object Input")]
    [SerializeField] private KitchenObjectScriptable MeatPattyUncooked;
    [Header("Kitchen Object output")]
    [SerializeField] private KitchenObjectScriptable MeatPattyCooked;
    [Header("Cooking Time Needed")]
    [SerializeField] private float MeatPattyCookingTime;


    public KitchenObjectScriptable GetMeatPattyCookingInput() {
        return MeatPattyUncooked;
    }

    public KitchenObjectScriptable GetMeatPattyCookingOutput() {
        return MeatPattyCooked;
    }

    public float GetCookingTime() {
        return MeatPattyCookingTime;
    }

}