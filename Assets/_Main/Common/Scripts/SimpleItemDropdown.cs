using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleItemDropdown : MonoBehaviour
{
    public Button btn;
    public TMP_Text txt;

    public void OnClicked(Action action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { action?.Invoke(); });
    }

    public void SetText(string content)
    {
        txt?.SetText(content);
    }
}
