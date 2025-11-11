using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UIMaleMainView : BaseView
    {
        [SerializeField] private Button playTTAnimButton;
        [SerializeField] private Button playPathologicalSimulationButton;

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
