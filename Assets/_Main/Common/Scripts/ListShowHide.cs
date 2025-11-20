using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;   // ✅ thêm

public class ListShowHide : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj;       // list part 3D của bạn
    [SerializeField] private Button showHideAllBtn;      // nút toggle ẩn/hiện tất cả

    // ✅ Tên bảng và key để dễ chỉnh trong Inspector
    [Header("Localization")]
    [SerializeField] private string tableName = "Table";
    [SerializeField] private string showAllKey = "ShowAllBtn";      // "Hiển thị tất cả"
    [SerializeField] private string showOnlyThisKey = "ShowOnlyThis"; // "Chỉ hiển thị phần này"

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
        UpdateButtonLabel();
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

        UpdateButtonLabel();
    }

    // ⭐ Đổi text nút theo trạng thái (dùng Localization)
    private void UpdateButtonLabel()
    {
        if (btnText == null) return;

        string keyToUse = isShowingAll ? showOnlyThisKey : showAllKey;

        var localized = new LocalizedString(tableName, keyToUse);
        var handle = localized.GetLocalizedStringAsync();
        handle.Completed += op =>
        {
            // tránh null nếu object đã bị destroy
            if (btnText != null)
                btnText.text = op.Result;
        };
    }
}
