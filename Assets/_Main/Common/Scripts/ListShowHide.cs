using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListShowHide : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj;
    [SerializeField] private Button showHideAllBtn;

    [Header("Sprites")]
    [SerializeField] private Sprite spriteShowAll;        // img1
    [SerializeField] private Sprite spriteShowOnlyThis;   // img2

    public static ListShowHide Instance { get; private set; }

    private InfoButton currentSelected;
    private bool isShowingAll = true;

    private Image btnImage;  // ⭐ ảnh của button

    private void Awake()
    {
        Instance = this;

        if (showHideAllBtn != null)
        {
            btnImage = showHideAllBtn.GetComponent<Image>();   // ⭐ lấy ảnh Button
            showHideAllBtn.onClick.AddListener(OnClickShowHideAll);

            showHideAllBtn.gameObject.SetActive(false);
        }
    }

    public void OnInfoButtonSelected(InfoButton info)
    {
        currentSelected = info;

        if (!showHideAllBtn.gameObject.activeSelf)
            showHideAllBtn.gameObject.SetActive(true);

        UpdateButtonSprite();
    }

    public void Show()
    {
        showHideAllBtn.gameObject.SetActive(true);
        UpdateButtonSprite();
    }

    public void Hide()
    {
        showHideAllBtn.gameObject.SetActive(false);
    }

    public void OnClickShowHideAll()
    {
        if (currentSelected == null || currentSelected.targetPart == null)
        {
            Debug.LogWarning("Chưa chọn InfoButton hoặc InfoButton chưa có targetPart.");
            return;
        }

        isShowingAll = !isShowingAll;

        if (isShowingAll)
        {
            foreach (var part in obj)
                if (part != null) part.SetActive(true);
        }
        else
        {
            foreach (var part in obj)
                if (part != null) part.SetActive(part == currentSelected.targetPart);
        }

        UpdateButtonSprite();
    }

    public void ShowAll()
    {
        isShowingAll = true;

        foreach (var part in obj)
            if (part != null) part.SetActive(true);

        UpdateButtonSprite();
    }

    // ⭐ Thay sprite theo trạng thái
    private void UpdateButtonSprite()
    {
        if (btnImage == null) return;

        btnImage.sprite = isShowingAll ? spriteShowOnlyThis : spriteShowAll;
    }
}
