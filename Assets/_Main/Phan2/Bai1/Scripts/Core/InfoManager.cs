using Frank;
using Sirenix.Utilities;
using UnityEngine;

namespace Bai11
{
    public class InfoManager : SingletonPersistent<InfoManager>
    {
        private InfoButton[] btns;
        private bool isShowing = true;
        private UIMain UIMain => GameViewManager.Instance.GetView<UIMain>();

        private void Start()
        {
            btns = FindObjectsOfType<InfoButton>(true);

            // register events
            UIMain.OnClickedShowInfoButton(ShowHide);
        }

        public void Initialize()
        {

        }

        public void ShowHide()
        {
            isShowing = !isShowing;
            btns.ForEach(i => i.Display(isShowing));
        }
    }
}
