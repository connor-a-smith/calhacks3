using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour {

    private Recipe desiredDish;
    private List<Ingredient.EnglishName> desiredIngredients;
    [SerializeField] private string successString = "Gracias!";
    [SerializeField] private Text tvText;
    
    // Use this for initialization
    void Start () {
        //Get a random dish and ingredients
        SetDishAndIngredients();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Sets the dish and ingredients this person ordered.
    /// </summary>
    private void SetDishAndIngredients()
    {
        desiredDish = RecipeController.instance.recipes[Random.Range(0, RecipeController.instance.recipes.Count)];

        string genderText;

        //una for female items, uno otherwise.
        if (desiredDish.name[desiredDish.name.Length-1] == 'a')
        {
            genderText = "una";
        }

        else
        {
            genderText = "uno";
        }


        tvText.text = "Yo quiero " + genderText + " " + desiredDish.name;

        desiredIngredients = new List<Ingredient.EnglishName>();

        //Getting a copy of the additional ingredients to avoid duplicates.
        List<Ingredient> ingredientOptions = new List<Ingredient>(RecipeController.instance.additionalIngredients);

        int numAdditionalIngredients = Random.Range(0, ingredientOptions.Count);

        print(numAdditionalIngredients);


        //Loop through the copied array, adding ingredients that haven't been added before.
        for (int i = 0; i < numAdditionalIngredients; i++)
        {
            int randomIndex = Random.Range(0, ingredientOptions.Count);
            desiredIngredients.Add(ingredientOptions[randomIndex].name);
            desiredIngredients.RemoveAt(randomIndex);

            print(ingredientOptions[randomIndex].name);
        }
        print(desiredDish.name);

    }

    void OnColliderEnter(Collision other)
    {
        FinishedFood food = other.gameObject.GetComponent<FinishedFood>();
        if (food != null)
        {
            receiveOrder(food);
        }
    }

    private int receiveOrder(FinishedFood food)
    {
        int score = 0;
        string result;

        StartCoroutine(StartOrderAfterInputSeconds(5f));
                
        //If the entire food is wrong, then earn 0 points.
        if (food.foodRecipe.name != desiredDish.name)
        {
            tvText.text = "Wrong order!";


            return 0; 
        }


        else
        {
            tvText.text = "Good Job!";
            return 10;
            
        }


        /*
        //Loop through the finished food's additional ingredients, matches get bonus! If either have extras, then minus points :<

        foreach (Ingredient ingredient in food.additionalIngredients)
        {
            if (desiredIngredients.Contains(ingredient.name))
            {
                score += 10;
                desiredIngredients.Remove(ingredient.name);
            }

            else
            {
                score -= 10;


            }
        }

        //If there are ingredients missed.
        if (desiredIngredients.Count != 0)
        {
            

        }*/
        
    }

    IEnumerator StartOrderAfterInputSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SetDishAndIngredients();
    }
}
