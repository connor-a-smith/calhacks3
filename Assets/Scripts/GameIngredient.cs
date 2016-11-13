using UnityEngine;
using System.Collections;

[RequireComponent(typeof (VRTK.VRTK_InteractableObject))]
public class GameIngredient : MonoBehaviour {

    public Ingredient.EnglishName name;

    private Ingredient correspondingIngredient;

    [HideInInspector] public VRTK.VRTK_ObjectTooltip tooltip;

    public void Start() {

        foreach (Ingredient ingredient in RecipeController.instance.allIngredients) {

            if (ingredient.name == name) {

                correspondingIngredient = ingredient;
                break;

            }
        }

        GameObject tooltipObject = GameObject.Instantiate(RecipeController.instance.tooltipPrefab) as GameObject;

        tooltipObject.transform.parent = this.gameObject.transform;
        tooltipObject.transform.localPosition = Vector3.up;

        tooltip = tooltipObject.GetComponent<VRTK.VRTK_ObjectTooltip>();
        
        tooltip.displayText = correspondingIngredient.translatedName;
        tooltip.drawLineTo = this.gameObject.transform;
        tooltip.gameObject.SetActive(false);

    }

    public void OnTriggerEnter(Collider other) {

        Debug.LogWarning("Entering?");

        SteamVR_TrackedObject controller = other.GetComponent<SteamVR_TrackedObject>();

        if (controller != null) {

            EnableTooltip();

        }
    }

    public void OnTriggerExit(Collider other) {

        SteamVR_TrackedObject controller = other.GetComponent<SteamVR_TrackedObject>();
        if(controller != null) {

            DisableTooltip();

        }
    }

    public void EnableTooltip() {
        tooltip.displayText = correspondingIngredient.translatedName;

        tooltip.gameObject.SetActive(true);

    }

    public void DisableTooltip() {

        tooltip.gameObject.SetActive(false);

    }

}
