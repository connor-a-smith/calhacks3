using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.LanguageTranslation.v1;
using System.Collections;

public class TranslationController : MonoBehaviour {

    private static LanguageTranslation m_Translate = new LanguageTranslation();

    public static void Translate(string englishVersion, string language, LanguageTranslation.TranslateCallback callbackMethod) {

        Debug.LogFormat("Translating {0} to {1}...", englishVersion, language);

        m_Translate.GetTranslation(englishVersion, "en", language, callbackMethod);

    }
}
