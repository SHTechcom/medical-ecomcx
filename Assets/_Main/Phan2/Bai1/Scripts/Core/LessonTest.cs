using Bai11;
using UnityEngine;

public class LessonTest : MonoBehaviour
{
    private Outline outlineSelected;

    private void Start()
    {
        DialogManager.Instance.Get().Set("...", "Hãy chỉ (click) vào vị trí thường xảy ra chửa ngoài tử cung nhất?");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out var hit))
            {
                if (hit.collider.TryGetComponent<Outline>(out var outline))
                {
                    if (outlineSelected)
                    {
                        outlineSelected.enabled = false;
                    }
                    outline.enabled = true;
                    outlineSelected = outline;
                }
                if (hit.collider.CompareTag("doaneo"))
                {
                    DialogManager.Instance.Get().Set("...", "ĐÚNG!");
                }
                else
                {
                    DialogManager.Instance.Get().Set("...", "SAI!");
                }
                DialogManager.Instance.Get().Show();
                DialogManager.Instance.Get().OnClicked(Clear);
            }
        }
    }

    public void Clear()
    {
        DialogManager.Instance.Get().Hide();
        LessonController.Instance.gameObject.SetActive(true);
        LessonController.Instance.ResetStatus();
        Destroy(gameObject);
    }
}
