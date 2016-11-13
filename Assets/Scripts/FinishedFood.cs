using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishedFood : MonoBehaviour {
    [HideInInspector] public List<Ingredient> additionalIngredients;
    public Recipe foodRecipe;

    [HideInInspector] public VRTK.VRTK_ObjectTooltip tooltip;

    //If true, then init will be called in start. This may be false, because when being instantiated, the recipe will not be set yet.
    public bool createdAtCompileTime = false;

    // Use this for initialization
    void Start () {
	    if(createdAtCompileTime) {
            init();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void init() {
        GameObject tooltipObject = GameObject.Instantiate(RecipeController.instance.tooltipPrefab) as GameObject;

        tooltipObject.transform.parent = this.gameObject.transform;
        tooltipObject.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

        tooltipObject.transform.localPosition = (Vector3.up / tooltipObject.transform.localScale.y);

        Debug.LogWarning("Local position is " + tooltipObject.transform.localPosition);

        tooltip = tooltipObject.GetComponent<VRTK.VRTK_ObjectTooltip>();

        tooltip.displayText = GetComponent<FinishedFood>().foodRecipe.name;
        tooltip.drawLineTo = this.gameObject.transform;
        tooltip.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other) {


        SteamVR_TrackedObject controller = other.GetComponent<SteamVR_TrackedObject>();

        if(controller != null) {

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
        tooltip.gameObject.SetActive(true);

    }

    public void DisableTooltip() {

        tooltip.gameObject.SetActive(false);

    }
}
