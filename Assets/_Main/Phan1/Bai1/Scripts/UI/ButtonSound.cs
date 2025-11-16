using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts.UI
{
    public class ButtonSound : MonoBehaviour
    {
        public Button btn;
        public string keySound;
        public AudioClip btnAudio;

        private void Reset()
        {
            btn = GetComponent<Button>();
        }

        private void Awake()
        {
            if (btn == null)
            {
                enabled = false;
                return;
            }
            
            btn.onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            if (!string.IsNullOrEmpty(keySound))
            {
                SoundManager.Instance.Play(keySound);
            }
            else
            {
                SoundManager.Instance.Play(btnAudio);
            }
        }
    }
}