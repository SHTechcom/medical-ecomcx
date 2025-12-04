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

    [Header("Kéo bộ phận 3D cần giữ lại vào đây")]
    public GameObject targetPart;

    [Header("Audio cho content này (có thể để trống)")]
    public AudioClip maleClip;
    public AudioClip femaleClip;
    public bool playAudioOnClick = true;

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
        float d = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.localScale = Vector3.one * d;
    }

    private void OnClick()
    {
        // Text dialog
        infoDialog.Set("...", content);
        infoDialog.Show();
        var dialog = DialogManager.Instance.Get();
        dialog.displayButton.gameObject.SetActive(true);

        // Đăng ký InfoButton hiện tại cho AudioController
        if (AudioController.Instance != null)
        {
            AudioController.Instance.currentInfoButton = this;

            if (playAudioOnClick)
            {
                Debug.Log("[InfoButton] OnClick play audio: " + name);
                PlayAudioForCurrentVoice();
            }
        }

        // ======= PHẦN CŨ CỦA BẠN =======
        Select();

        if (!isRotateSelf)
        {
            CameraController.Instance.OnCameraAroundTarget(transform, distance);
        }
        else
        {
            CameraController.Instance.OnClickAndDrag(positionTarget, rotationTarget);
        }

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

    public void PlayAudioForCurrentVoice()
    {
        var ac = AudioController.Instance;
        if (ac == null) return;

        AudioClip clipToPlay = null;

        if (ac.IsMaleVoice)
        {
            clipToPlay = maleClip;
        }
        else
        {
            clipToPlay = femaleClip;
        }

        if (clipToPlay == null)
        {
            Debug.Log("[InfoButton] No clip for voice. male=" + maleClip + ", female=" + femaleClip);
            ac.Stop();
            return;
        }

        ac.Play(clipToPlay);
    }

    public void Select()
    {
        LessonController.Instance.SelectInfoButton(this);

        if (ListShowHide.Instance != null)
        {
            ListShowHide.Instance.OnInfoButtonSelected(this);
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
