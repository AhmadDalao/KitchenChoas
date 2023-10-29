using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingScriptable : ScriptableObject {

    [Header("Kitchen Object Input")]
    [SerializeField] private KitchenObjectScriptable input;
    [Header("Kitchen Object output")]
    [SerializeField] private KitchenObjectScriptable output;
    [Header("Number Of Cuts Needed")]
    [SerializeField] private int cuts;


    public KitchenObjectScriptable GetCuttingInput() {
        return input;
    }

    public KitchenObjectScriptable GetCuttingOutput() {
        return output;
    }

    public int GetCuttingCount() {
        return cuts;
    }



}
