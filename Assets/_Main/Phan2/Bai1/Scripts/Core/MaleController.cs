using System;
using UnityEngine;

namespace Bai11
{
    public class MaleController : MonoBehaviour
    {
        private bool isPlayingAnim;
        [SerializeField] private GameObject duongdantinhAnim;
        [SerializeField] private PathologicalSimulationAnimation pathologicalSimulationAnimation;

        public UIMaleMainView UIMaleMainView => MaleViewManager.Instance.GetView<UIMaleMainView>();
        private UIBack UIBack => GameViewManager.Instance.GetView<UIBack>();

        [Header("Duong dan tinh")]
        public AudioClip maleClipDuongDanTinh;
        public AudioClip femaleClipDuongDanTinh;
        [Header("Benh ly")]
        public AudioClip maleClipBenhLy;
        public AudioClip femaleClipBenhLy;

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
            var dialog = DialogManager.Instance.Get();
            dialog.Show();
            dialog.Set("...", "Quan sát mô phỏng đường dẫn tinh");
            PlayAudioBenhLy(maleClipDuongDanTinh, femaleClipDuongDanTinh);
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
            var dialog = DialogManager.Instance.Get();
            dialog.Show();
            dialog.Set("...", "Quan sát mô phỏng bênh lý u xơ tuyến tiền liệt phì đại gây chèn ép niệu đạo, dẫn đến bí đái");
            PlayAudioBenhLy(maleClipBenhLy, femaleClipBenhLy);
            ShowUIBack(StopPathologicalSimulationAnim);
        }

        public void PlayAudioBenhLy(AudioClip maleClip, AudioClip femaleClip)
        {
            var ac = AudioController.Instance;
            if (ac == null) return;

            AudioClip clipToPlay = null;

            if (ac.IsMaleVoice)
            {
                clipToPlay = maleClip;
            }
            else
            {
                clipToPlay = femaleClip;
            }

            if (clipToPlay == null)
            {
                Debug.Log("[InfoButton] No clip for voice. male=" + maleClip + ", female=" + femaleClip);
                ac.Stop();
                return;
            }

            ac.Play(clipToPlay);
        }

        private void StopPathologicalSimulationAnim()
        {
            pathologicalSimulationAnimation.Stop();
            DialogManager.Instance.Get().Hide();
        }

        private void ShowUIBack(Action callback)
        {
            LessonController.Instance.SetShowHideInfo(false);
            UIMaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                LessonController.Instance.SetShowHideInfo(true);
                callback?.Invoke();
                UIBack.Hide();
                UIMaleMainView.Show();
            });
            UIBack.Show();
        }
    }
}
