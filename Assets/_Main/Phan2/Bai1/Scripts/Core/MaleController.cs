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

            UIMaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                StopTTAnim();
                UIBack.Hide();
                UIMaleMainView.Show();
            });
            UIBack.Show();
        }

        private void StopTTAnim()
        {
            isPlayingAnim = false;
            duongdantinhAnim.gameObject.SetActive(isPlayingAnim);
        }

        private void PlayPathologicalSimulationAnim()
        {
            pathologicalSimulationAnimation.Play();
            UIMaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                StopPathologicalSimulationAnim();
                UIBack.Hide();
                UIMaleMainView.Show();
            });
            UIBack.Show();
        }

        private void StopPathologicalSimulationAnim()
        {
            pathologicalSimulationAnimation.Stop();
        }
    }
}
