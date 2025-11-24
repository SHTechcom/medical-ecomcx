using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSwitch : MonoBehaviour
{
    public void SetLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }                                   

    public Sprite imageVN; // icon hiển thị khi đang là tiếng Việt
    public Sprite imageEN; // icon hiển thị khi đang là tiếng Anh

    [SerializeField] Button button;
    [SerializeField] Image image;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        SetLocale(0); // Mặc định là tiếng Việt
        image.sprite = imageVN;
        button.onClick.AddListener(ChangeLanguage);
    }

    private void ChangeLanguage()
    {
        if (LocalizationSettings.SelectedLocale.Identifier.Code == "en")
        {
            SetLocale(0);
            image.sprite = imageVN;
        }
        else
        {
            SetLocale(1);
            image.sprite = imageEN;
        }
        Debug.Log("Current Locale: " + LocalizationSettings.SelectedLocale.Identifier.Code);
    }
}
