using UnityEngine;

public class ShowHide : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(false);
    }
    public void Hide()
    {
        gameObject.SetActive(true);
    }
}
