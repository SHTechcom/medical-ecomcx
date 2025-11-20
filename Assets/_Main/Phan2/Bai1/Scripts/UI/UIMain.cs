using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class UIMain : BaseView
    {
        [SerializeField] private Button settingButton;
        [SerializeField] private Button showInfoButton;
        [SerializeField] private Button showLinksButton;
        [SerializeField] private Button showLessonTestButton;
        [SerializeField] private Button showInfoLessonBtn;

        public void OnClickedShowInfoLesson(Action callback)
        {
            showInfoLessonBtn.onClick.RemoveAllListeners();
            showInfoLessonBtn.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedSetting(Action callback)
        {
            settingButton.onClick.RemoveAllListeners();
            settingButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedShowInfoButton(Action callback)
        {
            showInfoButton.onClick.RemoveAllListeners();
            showInfoButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedShowLinksButton(Action callback)
        {
            showLinksButton.onClick.RemoveAllListeners();
            showLinksButton.onClick.AddListener(() => { callback?.Invoke(); });
        }

        public void OnClickedShowLessonTestButton(Action callback)
        {
            showLessonTestButton.onClick.RemoveAllListeners();
            showLessonTestButton.onClick.AddListener(() => { callback?.Invoke(); });
        }
    }
}
