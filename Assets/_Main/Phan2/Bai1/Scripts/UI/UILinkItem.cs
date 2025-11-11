using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UILinkItem : MonoBehaviour
    {
        public Button selectButton;
        public TMP_Text labelText;
        public TMP_Text linkText;

        private string link;

        private void Start()
        {
            OnClickedSelectLink(OpenLink);
        }

        public void OnClickedSelectLink(Action callback)
        {
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void Set(string label, string link)
        {
            this.link = link;
            labelText.text = label;
            linkText.text = link;
        }

        private void OpenLink()
        {
            Application.OpenURL($"{link}");
        }
    }
}
