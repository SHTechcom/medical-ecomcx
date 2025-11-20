using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class Dialog : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text contentText;

    [SerializeField] string tableName = "Table";   // tên StringTable bạn đang dùng

    /// <summary>
    /// name: text bình thường (KHÔNG localize)
    /// contentKey: KEY trong StringTable (CÓ localize)
    /// </summary>
    public void Set(string name, string contentKey)
    {
        // Name: gán thẳng, không dịch
        nameText.text = name;

        // Content: dùng Localization
        var localizedContent = new LocalizedString(tableName, contentKey);
        localizedContent.StringChanged += value =>
        {
            contentText.text = value;
        };
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);


    public void OnClicked(System.Action callback)
    {
        // Tùy UI của bạn – ví dụ nếu có button close:
        // closeButton.onClick.RemoveAllListeners();
        // closeButton.onClick.AddListener(() => callback?.Invoke());
        callback?.Invoke();
    }
}
