using System;
using Frank;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts.UI
{
    public class BottomUIControl : Singleton<BottomUIControl>
    {
        public GameObject buttonGroup;
        public Button showPanelButton;
        public Button hidePanelButton;
        public RectTransform mainPanel;
        public RectTransform descriptionPanel;
        public TMP_Text descriptionText;

        private void Awake()
        {
            showPanelButton.onClick.AddListener(ShowPanel);
            hidePanelButton.onClick.AddListener(HidePanel);
        }

        private void Start()
        {
            Active(false);
        }

        public void Active(bool isActive)
        {
            mainPanel.gameObject.SetActive(isActive);
            buttonGroup.gameObject.SetActive(isActive);
        }

        public void ShowPanel()
        {
            Active(true);
            hidePanelButton.gameObject.SetActive(true);
            showPanelButton.gameObject.SetActive(false);
            mainPanel.anchoredPosition = new Vector2(0, 0);
        }

        public void HidePanel()
        {
            hidePanelButton.gameObject.SetActive(false);
            showPanelButton.gameObject.SetActive(true);
            mainPanel.anchoredPosition = new Vector2(0, -250);
        }

        public void SetDescriptionText(string text)
        {
            descriptionText.text = text;
        }
    }
}