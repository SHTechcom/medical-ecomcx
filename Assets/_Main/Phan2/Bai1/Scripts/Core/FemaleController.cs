using Sirenix.Utilities;
using UnityEngine;

namespace Bai11
{
    public class FemaleController : MonoBehaviour
    {
        [SerializeField] private GameObject overviewModel;
        [SerializeField] private GameObject detailModel;
        [SerializeField] private GameObject animThuTinh;
        private bool isPlayingAnimThuTinh;
        [SerializeField] private ChuaNgoaiTuCungAnim chuaNgoaiTuCungAnim;

        private UIFemaleMainView UIFemaleMainView => FemaleViewManager.Instance.GetView<UIFemaleMainView>();
        private UIBack UIBack => GameViewManager.Instance.GetView<UIBack>();

        private void Start()
        {
            UIFemaleMainView.OnClickedBack(Back);
            UIFemaleMainView.OnClickedShowOverviewModel(ShowOverviewModel);
            UIFemaleMainView.OnClickedShowDetailModel(ShowDetailMdoel);
            UIFemaleMainView.OnClickedPlayThuTinhAnim(PlayThuTinhAnim);
            UIFemaleMainView.OnClickedPlayPathologicalSimulation(PlayPathologicalSimulation);

            ResetStatus();
        }

        private void Back()
        {
            LessonController.Instance.ResetStatus();
        }

        private void ShowOverviewModel()
        {
            ResetStatus();
        }

        private void ShowDetailMdoel()
        {
            ResetStatus();

            overviewModel.SetActive(false);
            detailModel.SetActive(true);

            UIFemaleMainView.ShowOverviewButton.gameObject.SetActive(true);
            UIFemaleMainView.ShowDetailButton.gameObject.SetActive(false);
            UIFemaleMainView.PlayThuTinhAnimButton.gameObject.SetActive(true);
            UIFemaleMainView.PlayPathologicalSimulationButton.gameObject.SetActive(true);
        }

        private void PlayThuTinhAnim()
        {
            isPlayingAnimThuTinh = true;
            animThuTinh.SetActive(isPlayingAnimThuTinh);

            UIFemaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                StopThuTinhAnim();
                UIBack.Hide();
                UIFemaleMainView.Show();
            });
            UIBack.Show();
        }

        private void StopThuTinhAnim()
        {
            isPlayingAnimThuTinh = false;
            animThuTinh.SetActive(isPlayingAnimThuTinh);
        }

        private void PlayPathologicalSimulation()
        {
            chuaNgoaiTuCungAnim.Play();
            UIFemaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                StopPathologicalSimulation();
                UIBack.Hide();
                UIFemaleMainView.Show();
            });
            UIBack.Show();
        }

        private void StopPathologicalSimulation()
        {
            chuaNgoaiTuCungAnim.Stop();
        }

        public void ResetStatus()
        {
            CameraController.Instance.SetType(CameraType.Free);
            overviewModel.SetActive(true);
            detailModel.SetActive(false);

            UIFemaleMainView.ShowOverviewButton.gameObject.SetActive(false);
            UIFemaleMainView.ShowDetailButton.gameObject.SetActive(true);
            UIFemaleMainView.PlayThuTinhAnimButton.gameObject.SetActive(false);
            UIFemaleMainView.PlayPathologicalSimulationButton.gameObject.SetActive(false);

            animThuTinh.SetActive(false);
            chuaNgoaiTuCungAnim.Stop();
        }
    }
}
