using UnityEngine;

namespace _Main.Phan1.Bai1.Scripts
{
    public class AnimationCaller : MonoBehaviour
    {
        public Animation anim;

        public void PlayAnimation(AnimationClip animationClip)
        {
            if (anim != null && !string.IsNullOrEmpty(animationClip.name))
            {
                anim.Play(animationClip.name);
            }
        }
    }
}