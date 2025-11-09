using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UISelectGender : BaseView
    {
        public Button selectMaleButton;
        public Button selectFemaleButton;

        public void OnClickSelectMale(Action callback)
        {
            selectMaleButton.onClick.RemoveAllListeners();
            selectMaleButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickSelectFemale(Action callback)
        {
            selectFemaleButton.onClick.RemoveAllListeners();
            selectFemaleButton.onClick.AddListener(() => { callback?.Invoke(); });
        }
    }
}
