using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ingredient {


    public enum EnglishName {

        Null, Cheese

    }

    public EnglishName name;

    public bool isAdditionalIngredient = false;

    public Object prefab;

    public string translatedName = "queso";



}
