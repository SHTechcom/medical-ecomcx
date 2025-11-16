using System;
using _Main.Phan1.Bai1.Scripts.TaskSystem;
using _Main.Phan1.Bai1.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts
{
    public class SliderCheckWithBaseActionStep : MonoBehaviour
    {
        public BaseActionStep step;
        public Slider slider;
        public string completeText;
        public string warningText;
        public float minPercent = 0;
        public float maxPercent = 1;
        public GameObject[] objsActiveWhenTrue;

        private void Start()
        {
            slider.onValueChanged.AddListener(ActiveObject);
        }

        public void CheckWithStep()
        {
            var currentPercent = slider.value / (slider.maxValue - slider.minValue);

            if (currentPercent >= minPercent && currentPercent <= maxPercent)
            {
                step.CompleteAction();
                if (!string.IsNullOrEmpty(completeText))
                {
                    WarningUI.Instance?.Show(completeText);
                }
            }
            else
            {
                WarningUI.Instance?.Show(warningText);
            }
        }

        public void ActiveObject(float value)
        {
            var currentPercent = slider.value / (slider.maxValue - slider.minValue);
            foreach (var obj in objsActiveWhenTrue)
            {
                obj.SetActive(currentPercent >= minPercent && currentPercent <= maxPercent);
            }
        }
    }
}