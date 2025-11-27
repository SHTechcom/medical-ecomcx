using Bai11;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public Button btn;
    public bool isRotateSelf;
    public Vector3 positionTarget;
    public Vector3 rotationTarget;
    public float distance = 0.3f;

    // ⭐ THÊM: ref để bạn gán bộ phận 3D tương ứng
    [Header("Kéo bộ phận 3D cần giữ lại vào đây")]
    public GameObject targetPart;

    public UnityEvent OnSelectedEvent;
    public UnityEvent OnDeselectedEvent;
    [SerializeField] string content;
    private Dialog infoDialog => DialogManager.Instance.Get();
    private UIBack UIBack => GameViewManager.Instance.GetView<UIBack>();

    private void Awake()
    {
        if (btn == null)
            btn = GetComponent<Button>();

        btn.onClick.AddListener(OnClick);
    }

    private void Reset()
    {
        btn = GetComponent<Button>();
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.localScale = Vector3.one * distance;
    }

    private void OnClick()
    {
        infoDialog.Set("...", content);
        infoDialog.Show();
        var dialog = DialogManager.Instance.Get();
        dialog.displayButton.gameObject.SetActive(true);
        Select();
        if (!isRotateSelf)
        {
            CameraController.Instance.OnCameraAroundTarget(transform, distance);
        }
        else
        {
            CameraController.Instance.OnClickAndDrag(positionTarget, rotationTarget);
        }
        //
        LessonController.Instance.LessonSpawned.GetComponent<MaleController>()?.UIMaleMainView.Hide();
        LessonController.Instance.LessonSpawned.GetComponent<FemaleController>()?.UIFemaleMainView.Hide();
        UIBack.Show();
        UIBack.OnClickedBack(() =>
        {
            var btns = FindObjectsOfType<InfoButton>(true);
            btns.ForEach(i => i.Deselect());
            LessonController.Instance.LessonSpawned.GetComponent<MaleController>()?.UIMaleMainView.Show();
            LessonController.Instance.LessonSpawned.GetComponent<FemaleController>()?.UIFemaleMainView.Show();
            UIBack.Hide();
            ListShowHide.Instance?.ShowAll();
            infoDialog.Hide();
        });
    }

    public void Select()
    {
        LessonController.Instance.SelectInfoButton(this);

        // ⭐ Thông báo cho ListShowHide biết InfoButton nào vừa được chọn
        if (ListShowHide.Instance != null)
        {
            ListShowHide.Instance.OnInfoButtonSelected(this);
            //ListShowHide.Instance.Show();
        }

        OnSelectedEvent?.Invoke();
        var dialog = DialogManager.Instance.Get();
        dialog.displayButton.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        OnDeselectedEvent?.Invoke();
        var dialog = DialogManager.Instance.Get();
        dialog.displayButton.gameObject.SetActive(false);

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Display(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
