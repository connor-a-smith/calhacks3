using UnityEngine;
using System.Collections;

public class RecipeController : MonoBehaviour {

    public RecipeController instance;

    public Ingredient[] allIngredients;

    public Recipe[] recipes;

    public void Awake() {

        if (instance == null) {

            instance = this;

        }

        else {

            Destroy(this.gameObject);

        }
    }

    public void Start() {

        foreach(Ingredient ingredient in allIngredients) {

            string translatedName = ingredient.name.ToString();
            // pass through translator
            ingredient.translatedName = translatedName;

        }
    }

    public Object GetIngredientPrefab(Ingredient.EnglishName ingredientToGet) {

        foreach (Ingredient ingredient in allIngredients) {

            if (ingredient.name == ingredientToGet) {

                return ingredient.prefab;

            }
        }

        return null;

    }



}
