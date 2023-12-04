using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IProgressBar {

    public static event EventHandler OnAnyCutSoundEvent;


    new public static void ResetStaticData() {
        OnAnyCutSoundEvent = null;
    }

    public event EventHandler CuttingAnimationEvent;
    public event EventHandler<IProgressBar.CuttingProgressEventArgs> CuttingProgressEvent;

    public class CuttingProgressEventArgs : EventArgs {
        public float ProgressBarAmount;
    }

    [SerializeField] private CuttingScriptable[] _cuttingScriptableArray;

    private int _cuttingCounter;

    public override void Interact(Player player) {
        // ClearCounter has NO kitchen object on top
        if (!HasKitchenObject()) {
            // player Has a Kitchen Object
            if (player.HasKitchenObject()) {
                // check if this Kitchen object is a Cuttable Object
                if (HasCuttableKitchenObject(player.GetKitchenObject().GetKitchenObjectScriptable())) {
                    // reset counter when new object is placed on counter
                    _cuttingCounter = 0;
                    // progress bar event trigger
                    CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                        ProgressBarAmount = (float)_cuttingCounter / GetCuttingCountKitchenObject(player.GetKitchenObject().GetKitchenObjectScriptable())
                    });
                    // place the Kitchen Object on top of the counter.
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            } else {
                // player is not holding anything
            }
        } else {
            //
            // ClearCounter Has Kitchen Object on Top !!
            //
            // check if player is holding anything 
            if (player.HasKitchenObject()) {
                // player is holding something 
                // check if it is a plate kitchen object
                if (player.GetKitchenObject() is PlateKitchenObject plate) {
                    // try to add the kitchen object from counter to plate
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectScriptable())) {
                        // destroy the kitchen object visual from the counter.
                        GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                // give the player the kitchen object
                // reset counter when removing object from counter
                _cuttingCounter = 0;
                // progress bar event trigger
                CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                    ProgressBarAmount = (float)_cuttingCounter
                });
                // give the player the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }

    public override void InteractCutting(Player player) {
        // ClearCounter has NO kitchen object on top
        if (HasKitchenObject()) {
            // player Has a Kitchen Object
            if (!player.HasKitchenObject()) {
                // check if you can cut the scriptable object.
                if (HasCuttableKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())) {
                    _cuttingCounter++;
                    // cutting animation event
                    CuttingAnimationEvent?.Invoke(this, EventArgs.Empty);
                    // play the sound.
                    OnAnyCutSoundEvent?.Invoke(this, EventArgs.Empty);
                    // progress bar event
                    CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                        ProgressBarAmount = (float)_cuttingCounter / GetCuttingCountKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())
                    });
                    // if cutting counter == cutting count for kitchen object cut the object and display the output
                    if (_cuttingCounter == GetCuttingCountKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())) {
                        _cuttingCounter = 0;
                        // save the output 
                        KitchenObjectScriptable cuttingOutput = GetCuttingOutputKitchenObject(GetKitchenObject().GetKitchenObjectScriptable());
                        // destroy the kitchen object on top of table
                        GetKitchenObject().DestroySelf();
                        // spawn the cutting output.
                        KitchenObject.SpawnKitchenObject(cuttingOutput, this);
                        // reset the cutting progress after cutting the object
                        CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                            ProgressBarAmount = (float)_cuttingCounter
                        });
                    }
                }
            }
        }
    }

    // Cutting Check
    private bool HasCuttableKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (CuttingScriptable kitchenObject in _cuttingScriptableArray) {
            if (kitchenObject.GetCuttingInput() == kitchenObjectScriptable) {
                return true;
            }
        }

        return false;
    }

    // Cutting Count
    private int GetCuttingCountKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (CuttingScriptable kitchenObject in _cuttingScriptableArray) {
            if (kitchenObject.GetCuttingInput() == kitchenObjectScriptable) {
                return kitchenObject.GetCuttingCount();
            }
        }
        return 0;
    }

    // Cutting output 
    private KitchenObjectScriptable GetCuttingOutputKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (CuttingScriptable kitchenObject in _cuttingScriptableArray) {
            if (kitchenObject.GetCuttingInput() == kitchenObjectScriptable) {
                return kitchenObject.GetCuttingOutput();
            }
        }
        return null;
    }



}
