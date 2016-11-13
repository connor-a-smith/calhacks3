using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1;

public class RecipeController : MonoBehaviour {

    public static RecipeController instance;

    public string language;

    [HideInInspector] public string andString;
    [HideInInspector] public string withString;

    public Object trashPrefab;

    [HideInInspector] public List<Ingredient> additionalIngredients;
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
            TranslationController.Translate(translatedName, language, ingredient.OnGetTranslation);

            if (ingredient.isAdditionalIngredient)
            {
                additionalIngredients.Add(ingredient);


            }
        }

        TranslationController.Translate("and", language, SetAndString);
        TranslationController.Translate("with", language, SetWithString);

        andString = "y";
        withString = "con";

    }

    public Object GetIngredientPrefab(Ingredient.EnglishName ingredientToGet) {

        foreach (Ingredient ingredient in allIngredients) {

            if (ingredient.name == ingredientToGet) {

                return ingredient.prefab;

            }
        }

        return null;
    }

    public void SetAndString(IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1.Translations translation) {

        if(translation != null && translation.translations.Length > 0) {
            andString = translation.translations[0].translation;
        }

        Debug.LogFormat("'And' string is set to {0}", andString);

    }

    public void SetWithString(IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1.Translations translation) {
        if(translation != null && translation.translations.Length > 0) {
            withString = translation.translations[0].translation;
        }

        Debug.LogFormat("'With' string is set to {0}", withString);
    }
}
