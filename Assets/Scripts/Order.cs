using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order : MonoBehaviour {

    private List<Recipe> recipesRemaining;
    private bool coreComplete = false;
    private string orderName = "";

	// Use this for initialization
	void Start () {

        recipesRemaining = new List<Recipe>(RecipeController.instance.recipes);
	
	}

    public void AddIngredient(Ingredient.EnglishName ingredient) {

        if(!coreComplete) {

            foreach(Recipe recipe in recipesRemaining) {

                if(!recipe.ContainsIngredient(ingredient)) {

                    recipesRemaining.Remove(recipe);

                }

                else {

                    recipe.coreIngredients.Remove(ingredient);

                    if(recipe.coreIngredients.Count <= 0) {

                        GameObject.Instantiate(recipe.prefab);
                        orderName = recipe.name;
                        coreComplete = true;

                    }
                }
            }

            if(recipesRemaining.Count <= 0) {

                // TRASH
                recipesRemaining.Clear();
                recipesRemaining = new List<Recipe>(RecipeController.instance.recipes);

                return;

            }
        }
        else {

            // First, we have to check to make sure this ingredient is an additional ingredient
            foreach (Ingredient additionalIngredient in RecipeController.instance.allIngredients) {

                if(additionalIngredient.name == ingredient && additionalIngredient.isAdditionalIngredient) {

                    // This is an additional ingredient


                }
            }
        }
    }
}
