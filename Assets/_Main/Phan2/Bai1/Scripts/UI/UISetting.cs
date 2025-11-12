using System;
using Bai11;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : BaseView
{
    [SerializeField] private Button closeButton;

    public void OnClickedClose(Action callback)
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => { callback?.Invoke(); });
    }
}
