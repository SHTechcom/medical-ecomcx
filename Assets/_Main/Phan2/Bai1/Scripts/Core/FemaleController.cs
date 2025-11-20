using System;
using Sirenix.Utilities;
using UnityEngine;

namespace Bai11
{
    public class FemaleController : MonoBehaviour
    {
        [SerializeField] private GameObject truyenvuModel;
        [SerializeField] private GameObject overviewModel;
        [SerializeField] private GameObject detailModel;
        [SerializeField] private GameObject animThuTinh;
        private bool isPlayingAnimThuTinh;
        [SerializeField] private ChuaNgoaiTuCungAnim chuaNgoaiTuCungAnim;
        [SerializeField] private GameObject tuthetucung;
        [SerializeField] private GameObject UI;

        public UIFemaleMainView UIFemaleMainView => FemaleViewManager.Instance.GetView<UIFemaleMainView>();
        private UIBack UIBack => GameViewManager.Instance.GetView<UIBack>();

        private void Start()
        {
            UIFemaleMainView.OnClickedBack(Back);
            UIFemaleMainView.OnClickedShowVu(ShowTuyenVuModel);
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

        private void ShowTuyenVuModel()
        {
            Clear();
            truyenvuModel.SetActive(true);
            UIFemaleMainView.ShowOverviewButton.gameObject.SetActive(true);
            UIFemaleMainView.ShowDetailButton.gameObject.SetActive(true);
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
            UI.SetActive(true);

            isPlayingAnimThuTinh = true;
            animThuTinh.SetActive(isPlayingAnimThuTinh);
            ShowUIBack(StopThuTinhAnim);
        }

        private void StopThuTinhAnim()
        {
            isPlayingAnimThuTinh = false;
            animThuTinh.SetActive(isPlayingAnimThuTinh);
        }

        private void PlayPathologicalSimulation()
        {
            UI.SetActive(true);

            chuaNgoaiTuCungAnim.Play();
            ShowUIBack(StopPathologicalSimulation);
        }

        private void StopPathologicalSimulation()
        {
            chuaNgoaiTuCungAnim.Stop();
        }

        public void ResetStatus()
        {
            CameraController.Instance.SetType(CameraType.Free);
            truyenvuModel.SetActive(false);
            overviewModel.SetActive(true);
            detailModel.SetActive(false);

            UIFemaleMainView.ShowVuButton.gameObject.SetActive(true);
            UIFemaleMainView.ShowOverviewButton.gameObject.SetActive(false);
            UIFemaleMainView.ShowDetailButton.gameObject.SetActive(true);
            UIFemaleMainView.PlayThuTinhAnimButton.gameObject.SetActive(false);
            UIFemaleMainView.PlayPathologicalSimulationButton.gameObject.SetActive(false);

            animThuTinh.SetActive(false);
            chuaNgoaiTuCungAnim.Stop();
        }

        public void Clear()
        {
            CameraController.Instance.SetType(CameraType.Free);
            truyenvuModel.SetActive(false);
            overviewModel.SetActive(false);
            detailModel.SetActive(false);

            UIFemaleMainView.ShowVuButton.gameObject.SetActive(false);
            UIFemaleMainView.ShowOverviewButton.gameObject.SetActive(false);
            UIFemaleMainView.ShowDetailButton.gameObject.SetActive(false);
            UIFemaleMainView.PlayThuTinhAnimButton.gameObject.SetActive(false);
            UIFemaleMainView.PlayPathologicalSimulationButton.gameObject.SetActive(false);

            animThuTinh.SetActive(false);
            chuaNgoaiTuCungAnim.Stop();
        }

        public void ShowTuTheTuCung()
        {
            tuthetucung.SetActive(true);
            ShowUIBack(HideTuTheTuCung);
        }

        public void HideTuTheTuCung()
        {
            tuthetucung.SetActive(false);
        }

        private void ShowUIBack(Action callback)
        {
            UIFemaleMainView.Hide();
            UIBack.OnClickedBack(() =>
            {
                UI.SetActive(false);
                callback?.Invoke();
                UIBack.Hide();
                UIFemaleMainView.Show();
            });
            UIBack.Show();
        }
    }
}
