using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UIMaleMainView : BaseView
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button playTTAnimButton;
        [SerializeField] private Button playPathologicalSimulationButton;

        public void OnClickedBack(Action callback)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedPlayTTAnim(Action callback)
        {
            playTTAnimButton.onClick.RemoveAllListeners();
            playTTAnimButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedPlayPathologicalSimulation(Action callback)
        {
            playPathologicalSimulationButton.onClick.RemoveAllListeners();
            playPathologicalSimulationButton.onClick.AddListener(() => { callback?.Invoke(); });
        }
    }
}
