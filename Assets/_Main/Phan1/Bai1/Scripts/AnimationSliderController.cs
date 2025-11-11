using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts
{
    [RequireComponent(typeof(Animation))]
    public class AnimationSliderController : MonoBehaviour
    {
        public Animation anim;
        public AnimationClip clip;
        public Slider slider;
        public bool isInvert;

        private AnimationState state;
        private float clipLength;

        void Start()
        {
            if (anim == null) anim = GetComponent<Animation>();

            if (string.IsNullOrEmpty(clip.name) || !anim.GetClip(clip.name))
            {
                Debug.LogError("Clip không tồn tại!");
                return;
            }

            anim.Play(clip.name);
            state = anim[clip.name];
            state.speed = 0;
            clipLength = state.length;
            slider.minValue = 0;
            slider.maxValue = clipLength;
            slider.onValueChanged.AddListener(OnSliderChanged);

            OnSliderChanged(slider.value);
        }

        void OnSliderChanged(float value)
        {
            if (state == null) return;
            if (isInvert)
            {
                float invertedValue = clipLength - Mathf.Clamp(value, 0f, clipLength);
                state.time = invertedValue;
            }
            else
            {
                state.time = Mathf.Clamp(value, 0f, clipLength);
            }

            anim.Sample();
        }
    }
}