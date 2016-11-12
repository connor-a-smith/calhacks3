using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Recipe {

    public string name;
    public Object prefab;
    public Ingredient.EnglishName[] coreIngredients;
    public Ingredient.EnglishName[] additionalIngredients;

}
