using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1;

public class RecipeController : MonoBehaviour {

    public Object trashPrefab;

    public static RecipeController instance;

    public List<Ingredient> allIngredients;

    public List<Recipe> recipes;



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
            ingredient.translateToSpanish();


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
