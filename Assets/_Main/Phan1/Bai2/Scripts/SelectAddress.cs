using _Main.Phan1.Bai1.StepSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectAddress : MonoBehaviour
{
    public Button select1Btn;
    public Button select2Btn;

    public UnityEvent onCompletedChose;

    private void Start()
    {
        select1Btn.onClick.AddListener(OnSelectAddress1);
        select2Btn.onClick.AddListener(OnSelectAddress2);
    }

    public void OnSelectAddress1()
    {
        //TODO: Spawn
        Hide();
        onCompletedChose?.Invoke();
    }

    public void OnSelectAddress2()
    {
        //TODO: Spawn
        Hide();
        onCompletedChose?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
