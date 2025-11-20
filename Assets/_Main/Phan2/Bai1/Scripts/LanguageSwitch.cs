using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSwitch : MonoBehaviour
{
    // Đảm bảo: index 0 = English, index 1 = Vietnamese trong Localization Settings
    public void SetLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }                                   

    public Sprite imageVN; // icon hiển thị khi đang là tiếng Việt
    public Sprite imageEN; // icon hiển thị khi đang là tiếng Anh

    Button button;
    Image image;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeLanguage);
        SetLocale(1);
        image.sprite = imageVN;
    }

    private void ChangeLanguage()
    {
        // 🔹 Nếu đang là tiếng Anh -> chuyển sang tiếng Việt
        if (LocalizationSettings.SelectedLocale.Identifier.Code == "en")
        {
            SetLocale(1);
            image.sprite = imageVN;
        }
        // 🔹 Ngược lại (đang là tiếng Việt) -> chuyển sang tiếng Anh
        else
        {
            SetLocale(0);
            image.sprite = imageEN;
        }
        Debug.Log("Current Locale: " + LocalizationSettings.SelectedLocale.Identifier.Code);
    }
}
