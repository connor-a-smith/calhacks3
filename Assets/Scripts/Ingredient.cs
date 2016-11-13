﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ingredient {

    public enum EnglishName {

        Cheese,
        Tomato,
        Lettuce

    }

    public EnglishName name;

    public bool isAdditionalIngredient = false;

    public Object prefab;

    [HideInInspector] public string translatedName;



}
