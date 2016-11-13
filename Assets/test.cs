using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Text testing;
    public Image hola;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (testing != null && testing.text.Contains("taco"))
        {

            hola.enabled = true;
        }
        else
            hola.enabled = false;
    }
}
