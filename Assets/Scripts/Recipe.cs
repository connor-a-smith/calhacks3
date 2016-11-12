using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Recipe {

    public string name;
    public Object prefab;
    public List<Ingredient.EnglishName> coreIngredients;

    public bool ContainsIngredient(Ingredient.EnglishName ingredientToCheck) {

        foreach(Ingredient.EnglishName ingredient in coreIngredients) {

            if(ingredientToCheck == ingredient) {

                return true;

            }
        }

        return false;

    }

}
