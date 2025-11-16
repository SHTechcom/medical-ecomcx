using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts
{
    public class ObjectScaleBySlider : MonoBehaviour
    {
        public Slider slider;

        [System.Flags]
        public enum ScaleAxis
        {
            X = 1 << 0,
            Y = 1 << 1,
            Z = 1 << 2
        }

        public ScaleAxis axes = ScaleAxis.X | ScaleAxis.Y | ScaleAxis.Z;

        public float minScale = 0.1f;
        public float maxScale = 1.5f;

        private Vector3 initialScale;

        private void Awake()
        {
            initialScale = transform.localScale;

            if (slider != null)
                slider.onValueChanged.AddListener(OnSliderChanged);
        }

        void OnSliderChanged(float value)
        {
            float scaleValue = Mathf.Lerp(minScale, maxScale, value);
            Vector3 newScale = initialScale;

            if ((axes & ScaleAxis.X) != 0)
                newScale.x = initialScale.x * scaleValue;

            if ((axes & ScaleAxis.Y) != 0)
                newScale.y = initialScale.y * scaleValue;

            if ((axes & ScaleAxis.Z) != 0)
                newScale.z = initialScale.z * scaleValue;

            transform.localScale = newScale;
        }
    }
}