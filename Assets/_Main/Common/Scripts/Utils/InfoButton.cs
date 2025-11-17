using Bai11;
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
    AudioSource audio;

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
        audio.Play();
        Select();
        if (!isRotateSelf)
        {
            CameraController.Instance.OnCameraAroundTarget(transform, distance);
        }
        else
        {
            CameraController.Instance.OnClickAndDrag(positionTarget, rotationTarget);
        }
    }

    public void Select()
    {
        LessonController.Instance.SelectInfoButton(this);

        // ⭐ Thông báo cho ListShowHide biết InfoButton nào vừa được chọn
        if (ListShowHide.Instance != null)
            ListShowHide.Instance.OnInfoButtonSelected(this);

        OnSelectedEvent?.Invoke();
    }

    public void Deselect()
    {
        OnDeselectedEvent?.Invoke();
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
