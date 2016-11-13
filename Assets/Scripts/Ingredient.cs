using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1;

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

    public string translatedName = "queso";

    private LanguageTranslation m_Translate = new LanguageTranslation();

    public void translateToSpanish() {
        Debug.Log(name.ToString());
        m_Translate.GetTranslation(name.ToString(), "en", "es", OnGetTranslation);
    }

    private void OnGetTranslation(IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1.Translations translation)
    {
        if (translation != null && translation.translations.Length > 0)
            translatedName = translation.translations[0].translation;
        Debug.Log(translatedName);

    }

}
