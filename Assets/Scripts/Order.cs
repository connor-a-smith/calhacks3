using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour {

    public Transform spawnLocation;

    private List<Recipe> recipesRemaining;
    private bool coreComplete = false;
    private string orderName = "";

    private List<Ingredient.EnglishName> addedIngredients;
    private List<GameObject> addedObjects;

    private List<Ingredient> additionalIngredients;

	// Use this for initialization
	void Start () {

        recipesRemaining = new List<Recipe>(RecipeController.instance.recipes);
        addedIngredients = new List<Ingredient.EnglishName>();
        addedObjects = new List<GameObject>();
	
	}

    public void OnTriggerEnter(Collider other) {

        GameIngredient otherIngredient = other.GetComponent<GameIngredient>();

        if (otherIngredient != null) {

            addedObjects.Add(other.gameObject);

            AddIngredient(otherIngredient.name);

        }
    }

    public void AddIngredient(Ingredient.EnglishName ingredient) {

        if(!coreComplete) {

            for (int i = 0; i < recipesRemaining.Count; i++) {

                Recipe recipe = recipesRemaining[i];

                if(!recipe.ContainsIngredient(ingredient)) {

                    Debug.LogFormat("Recipe {0} did not contain {1}!", recipe.name, ingredient.ToString());
                    recipesRemaining.Remove(recipe);
                    i--;

                }
                else {

                    Debug.LogFormat("Recipe {0} contained ingredient {1}!", recipe.name, ingredient.ToString());
                }               
            }

            if(recipesRemaining.Count <= 0) {

                GameObject.Instantiate(RecipeController.instance.trashPrefab);
                DestroyActiveIngredients();

                recipesRemaining.Clear();
                recipesRemaining = new List<Recipe>(RecipeController.instance.recipes);

                Debug.LogFormat("No recipes remaining, producing trash.");

                return;
            }

            if(!addedIngredients.Contains(ingredient)) {
                addedIngredients.Add(ingredient);
            }

            CheckRemainingRecipes();

        }
        else {

            // First, we have to check to make sure this ingredient is an additional ingredient
            foreach (Ingredient additionalIngredient in RecipeController.instance.allIngredients) {

                if(additionalIngredient.name == ingredient && additionalIngredient.isAdditionalIngredient) {

                    additionalIngredients.Add(additionalIngredient);

                }
            }
        }
    }

    public void CheckRemainingRecipes() {

        // Sort the active ingredients for comparison
        addedIngredients.Sort();
       
        foreach(Recipe recipe in recipesRemaining) {

            if(recipe.coreIngredients.Count == addedIngredients.Count) {

                recipe.coreIngredients.Sort();

                for(int i = 0; i < addedIngredients.Count; i++) {

                    if(addedIngredients[i] != recipe.coreIngredients[i]) {

                        return;

                    }
                }

                // If you get this far, then all ingredients are the same.
                CreateOrder(recipe);

            }
        }
    }

    public void CreateOrder(Recipe recipe) {

        Debug.LogFormat("Order Complete! Creating a {0}", recipe.name);

        GameObject newFood = GameObject.Instantiate(recipe.prefab, spawnLocation.position, Quaternion.identity) as GameObject;

        newFood.GetComponent<FinishedFood>().foodRecipe = recipe;

        coreComplete = true;
        orderName = recipe.name;

        

        DestroyActiveIngredients();

    }

    public void DestroyActiveIngredients() {

        foreach(GameObject ingredient in addedObjects) {

            GameObject.Destroy(ingredient);

        }

        addedIngredients.Clear();

        coreComplete = false;

        orderName = "";
    }

    public override string ToString() {

        if(coreComplete) {

            string orderString = orderName;

            for(int i = 0; i < additionalIngredients.Count; i++) {

                if(i == additionalIngredients.Count - 1) {

                    orderString += RecipeController.instance.andString + " ";
                    orderString += additionalIngredients[i].translatedName + ".";

                }

                else if(i == 0) {

                    orderString += " " + RecipeController.instance.withString + " ";
                    orderString += additionalIngredients[i].translatedName + ", ";
                }

                else {

                    orderString += additionalIngredients[i].translatedName + ", ";

                }
            }

            return orderString;

        }

        return "";
     
    }
}
