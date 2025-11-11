using UnityEngine;

namespace Bai11
{
    public class MaleController : MonoBehaviour
    {
        private bool isPlayingAnim;
        [SerializeField] private GameObject duongdantinhAnim;
        [SerializeField] private PathologicalSimulationAnimation pathologicalSimulationAnimation;

        private UIMaleMainView UIMaleMainView => MaleViewManager.Instance.GetView<UIMaleMainView>();

        private void Start()
        {
            //register events
            UIMaleMainView.OnClickedPlayTTAnim(PlayStopTTAnim);
            UIMaleMainView.OnClickedPlayPathologicalSimulation(PlayStopPlayPathologicalSimulationAnim);
        }

        private void PlayStopTTAnim()
        {
            isPlayingAnim = !isPlayingAnim;
            duongdantinhAnim.gameObject.SetActive(isPlayingAnim);
        }

        private void PlayStopPlayPathologicalSimulationAnim()
        {
            if (pathologicalSimulationAnimation.Isplaying)
            {
                pathologicalSimulationAnimation.Stop();
            }
            else
            {
                pathologicalSimulationAnimation.Play();
            }

        }
    }
}
