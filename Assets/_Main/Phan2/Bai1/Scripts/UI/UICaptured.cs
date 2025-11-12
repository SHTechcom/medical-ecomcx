using System;
using Bai11;
using UnityEngine;
using UnityEngine.UI;

public class UICaptured : BaseView
{
    public Button closeButton;
    public Image capturedImage;

    public void OnClickedClose(Action callback)
    {
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() => { callback?.Invoke(); });
    }

    public void SetCapturedImage(Sprite image)
    {
        capturedImage.sprite = image;
    }
}
