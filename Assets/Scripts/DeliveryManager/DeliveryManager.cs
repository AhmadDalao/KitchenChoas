using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }


    [SerializeField] private RecipeListScriptableObject recipeList;
    private List<RecipeScriptableObject> waitingRecipeList;


    private int recipeDeliveredCount;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int maxRecipeSpawned = 4;


    private void Awake() {

        waitingRecipeList = new List<RecipeScriptableObject>();

        // set the instance.
        if (Instance != null) {
            Debug.Log("there is more than 1 instance for Delivery Manager");
        }

        Instance = this;
    }


    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (GameManager.Instance.IsPlayingTimeState() && waitingRecipeList.Count < maxRecipeSpawned) {
                RecipeScriptableObject recipe = recipeList.GetRecipeListRandom();
                Debug.Log(recipe.GetRecipeName());
                waitingRecipeList.Add(recipe);
                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {
        // loop over the waitingRecipeList to compare the content of each list to the list on the plate.
        for (int i = 0; i < waitingRecipeList.Count; i++) {
            // get the each waiting recipe from the list
            RecipeScriptableObject waitingRecipe = waitingRecipeList[i];

            bool plateContentMatchesRecipe = true;

            // check if the count of the waitingRecipe equals to the plate list count.
            if (waitingRecipe.GetRecipeScriptableObject().Count == plateKitchenObject.GetKitchenObjectList().Count) {
                Debug.Log("count match now we can loop over both lists");
                // loop over the waitingRecipe which was spawned 
                // ingredientOnRecipe is the ingredient on the waiting recipe
                foreach (KitchenObjectScriptable ingredientOnRecipe in waitingRecipe.GetRecipeScriptableObject()) {
                    bool ingredientMatch = false;
                    // loop over the plateKitchenObject list which contains the kitchen objects ( ingredients ).
                    // @item is the kitchen object on top of the plate
                    foreach (KitchenObjectScriptable ingredientOnPlate in plateKitchenObject.GetKitchenObjectList()) {
                        if (ingredientOnPlate == ingredientOnRecipe) {
                            Debug.Log("You have a match a correct recipe was delivered");
                            ingredientMatch = true;
                            break;
                        }
                    }
                    // if ingredientMatch is still false
                    if (!ingredientMatch) {
                        // ingredient not found
                        plateContentMatchesRecipe = false;

                    }
                }

                if (plateContentMatchesRecipe) {
                    // player successfully delivered the correct recipe.
                    Debug.Log(" player successfully delivered the correct recipe.");
                    recipeDeliveredCount++;
                    // remove from the list
                    waitingRecipeList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }


            }
        }

        // no matches found.
        // player did not deliver correct recipe.
        Debug.Log(" player did not deliver correct recipe.");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

    }


    public List<RecipeScriptableObject> GetRecipeScriptableObjectsListFromManager() {
        return waitingRecipeList;
    }

    public int GetRecipeDeliveredCount() {
        return recipeDeliveredCount;
    }

}
