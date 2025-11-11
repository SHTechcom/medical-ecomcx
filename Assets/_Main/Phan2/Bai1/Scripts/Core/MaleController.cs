using System;
using UnityEngine;

namespace Bai11
{
    public class MaleController : MonoBehaviour
    {
        private bool isPlayingAnim;
        [SerializeField] private GameObject duongdantinhAnim;
        [SerializeField] private PathologicalSimulationAnimation pathologicalSimulationAnimation;

        private UIMaleMainView UIMaleMainView => MaleViewManager.Instance.GetView<UIMaleMainView>();
        private UIBack UIBack => GameViewManager.Instance.GetView<UIBack>();

        private void Start()
        {
            //register events
            UIMaleMainView.OnClickedBack(Back);
            UIMaleMainView.OnClickedPlayTTAnim(PlayTTAnim);
            UIMaleMainView.OnClickedPlayPathologicalSimulation(PlayPathologicalSimulationAnim);
        }

        private void Back()
        {
            LessonController.Instance.ResetStatus();
        }

        private void PlayTTAnim()
        {
            isPlayingAnim = true;
            duongdantinhAnim.gameObject.SetActive(isPlayingAnim);
            ShowUIBack(StopTTAnim);
        }

        private void StopTTAnim()
        {
            isPlayingAnim = false;
            duongdantinhAnim.gameObject.SetActive(isPlayingAnim);
        }

        private void PlayPathologicalSimulationAnim()
        {
            pathologicalSimulationAnimation.Play();
            ShowUIBack(StopPathologicalSimulationAnim);
        }

        private void StopPathologicalSimulationAnim()
        {
            pathologicalSimulationAnimation.Stop();
        }

        private void ShowUIBack(Action callback)
        {
            UIMaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                callback?.Invoke();
                UIBack.Hide();
                UIMaleMainView.Show();
            });
            UIBack.Show();
        }
    }
}
