using Bai11;
using Frank;
using UnityEngine;

public class MaleViewManager : Singleton<MaleViewManager>
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
