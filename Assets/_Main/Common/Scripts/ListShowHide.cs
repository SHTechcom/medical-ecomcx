using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListShowHide : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj;       // list part 3D của bạn
    [SerializeField] private Button showHideAllBtn;      // nút toggle ẩn/hiện tất cả

    public static ListShowHide Instance { get; private set; }

    private InfoButton currentSelected;   // ⭐ Lưu InfoButton được bấm gần nhất
    private bool isShowingAll = true;     // ⭐ Trạng thái toggle

    private TMP_Text btnText;             // ⭐ lấy text trên nút

    private void Awake()
    {
        Instance = this;

        if (showHideAllBtn != null)
        {
            btnText = showHideAllBtn.GetComponentInChildren<TMP_Text>();
            showHideAllBtn.onClick.AddListener(OnClickShowHideAll);

            showHideAllBtn.gameObject.SetActive(false); // ban đầu ẩn đến khi chọn InfoButton
        }
    }

    // ⭐ InfoButton gọi hàm này
    public void OnInfoButtonSelected(InfoButton info)
    {
        currentSelected = info;

        // Lần đầu bấm InfoButton → hiện nút
        if (!showHideAllBtn.gameObject.activeSelf)
            showHideAllBtn.gameObject.SetActive(true);

        // Cập nhật text khi vừa chọn InfoButton (không toggle)
        UpdateButtonLabel();
    }

    public void Show()
    {
        showHideAllBtn.gameObject.SetActive(true);
    }

    public void Hide()
    {
        showHideAllBtn.gameObject.SetActive(false);
    }

    // ⭐ Hàm được gọi khi bấm showHideAllBtn
    public void OnClickShowHideAll()
    {
        if (currentSelected == null || currentSelected.targetPart == null)
        {
            Debug.LogWarning("Chưa chọn InfoButton hoặc InfoButton chưa có targetPart.");
            return;
        }

        isShowingAll = !isShowingAll;  // ⭐ Toggle state

        if (isShowingAll)
        {
            // ⭐ HIỆN TẤT CẢ
            foreach (var part in obj)
                if (part != null) part.SetActive(true);
        }
        else
        {
            // ⭐ ẨN HẾT TRỪ thằng được chọn
            foreach (var part in obj)
                if (part != null) part.SetActive(part == currentSelected.targetPart);
        }

        UpdateButtonLabel();
    }

    public void ShowAll()
    {
        isShowingAll = true;  // ⭐ Toggle state

        if (isShowingAll)
        {
            // ⭐ HIỆN TẤT CẢ
            foreach (var part in obj)
                if (part != null) part.SetActive(true);
        }
    }

    // ⭐ Đổi text nút theo trạng thái
    private void UpdateButtonLabel()
    {
        if (btnText == null) return;

        if (isShowingAll)
            btnText.text = "Chỉ hiển thị phần này"; // next action = hide others
        else
            btnText.text = "Hiển thị tất cả";       // next action = show all
    }
}
