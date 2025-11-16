using System;
using Frank;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts.UI
{
    public class StepUIControl : Singleton<StepUIControl>
    {
        public Button skipButton;
        public Button continueButton;

        private void Awake()
        {
            skipButton.onClick.AddListener(HideAll);
            continueButton.onClick.AddListener(HideAll);
        }

        private void Start()
        {
            ShowSkip(false);
            ShowContinue(false);
        }

        public void HideAll()
        {
            skipButton.gameObject.SetActive(false);
            continueButton.gameObject.SetActive(false);
        }

        public void ShowSkip(bool isShow)
        {
            skipButton.gameObject.SetActive(isShow);
        }

        public void ShowContinue(bool isShow)
        {
            continueButton.gameObject.SetActive(isShow);
        }
    }
}