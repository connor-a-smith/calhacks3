using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerOrder : MonoBehaviour {

    private Recipe desiredDish;
    private List<Ingredient.EnglishName> desiredIngredients;

	// Use this for initialization
	void Start () {
        //Get a random dish and ingredients
        SetDishAndIngredients();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SetDishAndIngredients()
    {
        desiredDish = RecipeController.instance.recipes[Random.Range(0, RecipeController.instance.recipes.Count)];

        desiredIngredients = new List<Ingredient.EnglishName>();

        //Getting a copy of the additional ingredients to avoid duplicates.
        List<Ingredient> ingredientOptions = new List<Ingredient>(RecipeController.instance.additionalIngredients);

        int numAdditionalIngredients = Random.Range(0, ingredientOptions.Count);

        //Loop through the copied array, adding ingredients that haven't been added before.
        for (int i = 0; i < numAdditionalIngredients; i++)
        {
            int randomIndex = Random.Range(0, ingredientOptions.Count);
            desiredIngredients.Add(ingredientOptions[randomIndex].name);
            desiredIngredients.RemoveAt(randomIndex);


        }

           
    }

    void OnColliderEnter(Collision other)
    {
        if (other.gameObject.GetComponent<FinishedFood>())
        {

        }
    }

}
