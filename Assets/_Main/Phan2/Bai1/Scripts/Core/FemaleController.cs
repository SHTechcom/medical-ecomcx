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

        private void Start()
        {
            UIFemaleMainView.OnClickedShowOverviewModel(ShowOverviewModel);
            UIFemaleMainView.OnClickedShowDetailModel(ShowDetailMdoel);
            UIFemaleMainView.OnClickedPlayThuTinhAnim(OnClickedPlayThuTinhAnim);
            UIFemaleMainView.OnClickedPlayPathologicalSimulation(OnClickedPlayPathologicalSimulation);

            ResetStatus();
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

        private void OnClickedPlayThuTinhAnim()
        {
            isPlayingAnimThuTinh = !isPlayingAnimThuTinh;
            animThuTinh.SetActive(isPlayingAnimThuTinh);
        }

        private void OnClickedPlayPathologicalSimulation()
        {
            if (chuaNgoaiTuCungAnim.IsPlaying)
            {
                chuaNgoaiTuCungAnim.Stop();
            }
            else
            {
                chuaNgoaiTuCungAnim.Play();
            }
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
