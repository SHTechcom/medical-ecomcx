using System;
using Bai11;
using UnityEngine;
using UnityEngine.UI;

public class UIBack : BaseView
{
    [SerializeField] private Button backButton;

    public void OnClickedBack(Action callback)
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() =>
        {
            callback?.Invoke();
        });
    }
}
