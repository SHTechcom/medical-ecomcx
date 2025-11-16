using _Main.Phan1.Bai1.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Phan1.Bai1.Scripts
{
    public class AnimationCaller : MonoBehaviour
    {
        public bool registerUIControl;
        public Animation anim;
        
        public void PlayAnimation(AnimationClip clip)
        {
            if (clip == null || anim == null)
                return;

            if (registerUIControl)
            {
                AnimationUIControl.Instance?.RegisterAnimationControl(anim, clip);
                ShowAnimationUIControl();
            }

            anim.Play(clip.name);
        }

        public void ShowAnimationUIControl()
        {
            AnimationUIControl.Instance?.Show();
        }

        public void HideAnimationUIControl()
        {
            AnimationUIControl.Instance?.Hide();
        }
    }
}