using UnityEngine;

namespace Bai11
{
    public class ChuaNgoaiTuCungAnim : MonoBehaviour
    {
        [SerializeField] private GameObject doaneo;
        private Outline outlineDoaneo;
        private bool isPlaying;
        [SerializeField] private GameObject animMove;

        public bool IsPlaying => isPlaying;

        public void Play()
        {
            isPlaying = true;
            if (!doaneo.TryGetComponent<Outline>(out outlineDoaneo))
            {
                outlineDoaneo = doaneo.AddComponent<Outline>();
            }

            outlineDoaneo.OutlineColor = Color.red;
            outlineDoaneo.enabled = true;
            animMove.SetActive(true);
        }

        public void Stop()
        {
            isPlaying = false;
            if (!doaneo.TryGetComponent<Outline>(out outlineDoaneo))
            {
                outlineDoaneo = doaneo.AddComponent<Outline>();
            }
            outlineDoaneo.enabled = false;
            animMove.SetActive(false);
        }
    }
}
