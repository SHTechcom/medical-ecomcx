using System;
using Frank;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts.UI
{
    public class AnimationUIControl : Singleton<AnimationUIControl>
    {
        public GameObject mainPanel;

        public Slider scrubSlider;

        public float slowSpeed = 0.3f;
        public float fastSpeed = 2f;

        private Animation _anim;
        private AnimationState _currentState;
        private bool _isPaused = false;
        private bool _isScrubbing = false;

        private AnimationState CurrentState
        {
            get
            {
                if (_anim == null || _anim.clip == null) return null;
                return _anim[_anim.clip.name];
            }
        }

        public void RegisterAnimationControl(Animation anim, AnimationClip clip)
        {
            _anim = anim;
            _currentState = anim[clip.name];

            _isPaused = false;
        }

        public void Show()
        {
            mainPanel.SetActive(true);
        }

        public void Hide()
        {
            mainPanel.SetActive(false);
        }

        private void Start()
        {
            if (scrubSlider != null)
            {
                scrubSlider.onValueChanged.AddListener(OnScrubChanged);
            }

            Hide();
        }

        private void Update()
        {
            if (_currentState == null) return;

            if (!_isPaused && !_isScrubbing)
            {
                scrubSlider.value = _currentState.time / _currentState.length;
            }
        }

        public void Slow()
        {
            var state = CurrentState;
            if (state != null)
            {
                state.speed = slowSpeed;
                _isPaused = false;
            }
        }

        public void PlayNormal()
        {
            var state = CurrentState;
            if (state != null)
            {
                state.speed = 1f;
                _isPaused = false;
            }
        }

        public void Forward()
        {
            var state = CurrentState;
            if (state != null)
            {
                state.speed = fastSpeed;
                _isPaused = false;
            }
        }

        public void Replay()
        {
            if (_currentState == null)
                return;

            _currentState.time = 0f;
            _currentState.speed = 1f;

            _anim.Play(_currentState.name);

            _isPaused = false;

            if (scrubSlider != null)
                scrubSlider.value = 0f;
        }

        public void Pause()
        {
            var state = CurrentState;
            if (state != null)
            {
                state.speed = 0f;
                _isPaused = true;
            }
        }

        public void Resume()
        {
            var state = CurrentState;
            if (state != null)
            {
                state.speed = 1f;
                _isPaused = false;
            }
        }

        public void StartScrub()
        {
            _isScrubbing = true;
            Pause();
        }

        public void EndScrub()
        {
            _isScrubbing = false;
            Resume();
        }

        private void OnScrubChanged(float val)
        {
            if (_currentState == null) return;

            if (_isScrubbing)
            {
                _currentState.time = Mathf.Clamp01(val) * _currentState.length;
                _anim.Sample();
            }
        }
    }
}