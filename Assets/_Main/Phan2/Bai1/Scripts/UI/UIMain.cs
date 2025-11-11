using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UIMain : BaseView
    {
        [SerializeField] private Button showInfoButton;

        public void OnClickedShowInfoButton(Action callback)
        {
            showInfoButton.onClick.RemoveAllListeners();
            showInfoButton.onClick.AddListener(() => { callback?.Invoke(); });
        }
    }
}
