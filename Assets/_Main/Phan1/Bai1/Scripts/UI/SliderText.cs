using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts.UI
{
    public class SliderText : MonoBehaviour
    {
        public Slider slider;
        public TMP_Text text;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnChange);
            OnChange(slider.value);
        }

        private void OnChange(float value)
        {
            text.text = value.ToString("F1");
        }
    }
}