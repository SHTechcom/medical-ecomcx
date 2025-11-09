using UnityEngine;

namespace Bai11
{
    public class GameViewManager : MonoBehaviour
    {
        [SerializeField] private BaseView[] views;

        public T GetView<T>() where T : BaseView
        {
            foreach (var view in this.views)
            {
                if (view is T)
                {
                    return view as T;
                }
            }

            return default(T);
        }
    }
}
