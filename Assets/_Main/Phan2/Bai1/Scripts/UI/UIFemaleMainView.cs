using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UIFemaleMainView : BaseView
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button showOverviewButton;
        [SerializeField] private Button showDetailButton;
        [SerializeField] private Button playThuTinhAnimButton;
        [SerializeField] private Button playPathologicalSimulationButton;

        public Button ShowOverviewButton => showOverviewButton;
        public Button ShowDetailButton => showDetailButton;
        public Button PlayThuTinhAnimButton => playThuTinhAnimButton;
        public Button PlayPathologicalSimulationButton => playPathologicalSimulationButton;

        public void OnClickedBack(Action callback)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedShowOverviewModel(Action callback)
        {
            showOverviewButton.onClick.RemoveAllListeners();
            showOverviewButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedShowDetailModel(Action callback)
        {
            showDetailButton.onClick.RemoveAllListeners();
            showDetailButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedPlayThuTinhAnim(Action callback)
        {
            playThuTinhAnimButton.onClick.RemoveAllListeners();
            playThuTinhAnimButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedPlayPathologicalSimulation(Action callback)
        {
            playPathologicalSimulationButton.onClick.RemoveAllListeners();
            playPathologicalSimulationButton.onClick.AddListener(() => { callback?.Invoke(); });
        }
    }
}
