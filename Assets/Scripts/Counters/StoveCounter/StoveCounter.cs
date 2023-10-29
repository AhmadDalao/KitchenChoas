using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IProgressBar {

    public event EventHandler<StoveOnVisualEventArgs> StoveOnVisualEvent;
    public class StoveOnVisualEventArgs : EventArgs {
        public State stoveState;
    }

    public event EventHandler<IProgressBar.CuttingProgressEventArgs> CuttingProgressEvent;
    public class CuttingProgressEventArgs : EventArgs {
        public float ProgressBarAmount;
    }


    [SerializeField] private StoveScriptableCooking[] _stoveScriptableCookingArray;
    [SerializeField] private StoveScriptableBurned[] _stoveScriptableBurnedArray;

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private float _cookingTime;
    private float _burningTime;
    private KitchenObjectScriptable _cookedMeatPatty;
    private KitchenObjectScriptable _burnedMeatPatty;

    private State _state;
    private void Start() {
        _state = State.Idle;
    }


    private void Update() {
        // Check if there is a kitchen object on top
        if (HasKitchenObject()) {
            // now update the state according to the cooking time using switch
            switch (_state) {
                case State.Idle:
                    break;
                case State.Frying:
                    // keep track of the cooking time
                    _cookingTime += Time.deltaTime;
                    // progress bar event trigger
                    CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                        ProgressBarAmount = (float)_cookingTime / GetCookingTimeKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())
                    });
                    // if the cooking time is over the time allowed spawn the cooked kitchen object
                    if (_cookingTime > GetCookingTimeKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())) {
                        // save a copy of the cooked meat patty
                        _cookedMeatPatty = GetCookingOutPutKitchenObject(GetKitchenObject().GetKitchenObjectScriptable());
                        // Destroy the uncooked meatPatty.
                        GetKitchenObject().DestroySelf();
                        // Spawn the cooked Meat Patty
                        KitchenObject.SpawnKitchenObject(_cookedMeatPatty, this);
                        // reset the timer first
                        _cookingTime = 0f;
                        // change the state to fried 
                        _state = State.Fried;
                        // progress bar event trigger
                        CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                            ProgressBarAmount = (float)_cookingTime
                        });
                    }
                    break;
                case State.Fried:
                    // check if the object is now a burning object.
                    if (HasBurningKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())) {
                        // keep track of the cooking time
                        _burningTime += Time.deltaTime;
                        // progress bar event trigger
                        CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                            ProgressBarAmount = (float)_burningTime / GetBurningTimeKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())
                        });
                        // if the burning time is over the time allowed spawn the burned kitchen object
                        if (_burningTime > GetBurningTimeKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())) {
                            // save a copy of the cooked meat patty
                            _burnedMeatPatty = GetBurningOutPutKitchenObject(GetKitchenObject().GetKitchenObjectScriptable());
                            // Destroy the cooked meatPatty.
                            GetKitchenObject().DestroySelf();
                            // Spawn the burned Meat Patty
                            KitchenObject.SpawnKitchenObject(_burnedMeatPatty, this);
                            // reset the timer first
                            _burningTime = 0f;
                            // change the state to burned 
                            _state = State.Burned;
                            // progress bar event trigger
                            CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                                ProgressBarAmount = (float)_burningTime
                            });
                        }
                    }
                    break;
                case State.Burned:
                    break;
            }

        }
    }

    public override void Interact(Player player) {
        // ClearCounter has NO kitchen object on top
        if (!HasKitchenObject()) {
            // player Has a Kitchen Object
            if (player.HasKitchenObject()) {
                // check if the object the player is holding is a cooking object.
                if (HasCookingKitchenObject(player.GetKitchenObject().GetKitchenObjectScriptable())) {
                    // place the Kitchen Object on top of the counter.
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    // reset the frying time. and burning time just in case
                    _cookingTime = 0f;
                    _burningTime = 0f;
                    // change the state to frying since the object is on top
                    _state = State.Frying;
                    // trun on the stove using the event
                    StoveOnVisualEvent.Invoke(this, new StoveOnVisualEventArgs {
                        stoveState = _state
                    });
                    // progress bar event trigger
                    CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                        ProgressBarAmount = (float)_cookingTime / GetCookingTimeKitchenObject(GetKitchenObject().GetKitchenObjectScriptable())
                    });
                }
            } else {
                // player is not holding anything
            }
        } else {
            // ClearCounter Has Kitchen Object on Top !!
            // check if player is not holding anything 
            if (!player.HasKitchenObject()) {
                // give the player the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);
                // reset the frying time. and burning time just in case
                _cookingTime = 0f;
                _burningTime = 0f;
                // change the state to Idle since the object No Longer on top
                _state = State.Idle;
                // trun off the stove using the event
                StoveOnVisualEvent.Invoke(this, new StoveOnVisualEventArgs {
                    stoveState = _state
                });
                CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                    ProgressBarAmount = 0f
                });
            } else {
                // player is holding something 
                // check if it is a plate kitchen object
                if (player.GetKitchenObject() is PlateKitchenObject plate) {
                    // get the kitchen object from the Counter
                    KitchenObjectScriptable kitchenObjectFromCounter = GetKitchenObject().GetKitchenObjectScriptable();
                    // try to add the kitchen object from counter to plate
                    if (plate.TryAddIngredient(kitchenObjectFromCounter)) {

                        _cookingTime = 0f;
                        _burningTime = 0f;
                        // change the state to Idle since the object No Longer on top
                        _state = State.Idle;
                        // trun off the stove using the event
                        StoveOnVisualEvent.Invoke(this, new StoveOnVisualEventArgs {
                            stoveState = _state
                        });
                        CuttingProgressEvent?.Invoke(this, new IProgressBar.CuttingProgressEventArgs {
                            ProgressBarAmount = 0f
                        });

                        // destroy the kitchen object visual from the counter.
                        GetKitchenObject().DestroySelf();
                    }
                }
            }

        }
    }


    /*
     * 
     *  Cooking Functions
     * 
     * 
     */


    // Cooking Check
    private bool HasCookingKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (StoveScriptableCooking kitchenObject in _stoveScriptableCookingArray) {
            if (kitchenObject.GetMeatPattyCookingInput() == kitchenObjectScriptable) {
                return true;
            }
        }

        return false;
    }
    // Cooking Time
    private float GetCookingTimeKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (StoveScriptableCooking kitchenObject in _stoveScriptableCookingArray) {
            if (kitchenObject.GetMeatPattyCookingInput() == kitchenObjectScriptable) {
                return kitchenObject.GetCookingTime();
            }
        }
        return 0;
    }
    // Cooking output 
    private KitchenObjectScriptable GetCookingOutPutKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (StoveScriptableCooking kitchenObject in _stoveScriptableCookingArray) {
            if (kitchenObject.GetMeatPattyCookingInput() == kitchenObjectScriptable) {
                return kitchenObject.GetMeatPattyCookingOutput();
            }
        }
        return null;
    }



    /*
 * 
 *  Burning Functions
 * 
 * 
 */


    // Burning Check
    private bool HasBurningKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (StoveScriptableBurned kitchenObject in _stoveScriptableBurnedArray) {
            if (kitchenObject.GetMeatPattyBurnedInput() == kitchenObjectScriptable) {
                return true;
            }
        }

        return false;
    }
    // Burning Time
    private float GetBurningTimeKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (StoveScriptableBurned kitchenObject in _stoveScriptableBurnedArray) {
            if (kitchenObject.GetMeatPattyBurnedInput() == kitchenObjectScriptable) {
                return kitchenObject.GetBurnedTime();
            }
        }
        return 0;
    }
    // Burning output 
    private KitchenObjectScriptable GetBurningOutPutKitchenObject(KitchenObjectScriptable kitchenObjectScriptable) {
        foreach (StoveScriptableBurned kitchenObject in _stoveScriptableBurnedArray) {
            if (kitchenObject.GetMeatPattyBurnedInput() == kitchenObjectScriptable) {
                return kitchenObject.GetMeatPattyBurnedOutput();
            }
        }
        return null;
    }



}
