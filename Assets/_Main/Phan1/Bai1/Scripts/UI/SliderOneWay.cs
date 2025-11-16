using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts.UI
{
    public class SliderOneWay : MonoBehaviour
    {
        public Slider slider;
        private float maxReached = 0f;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnSliderChanged);
        }

        void OnSliderChanged(float v)
        {
            if (v < maxReached)
            {
                slider.SetValueWithoutNotify(maxReached);
                return;
            }

            maxReached = v;
        }
    }
}