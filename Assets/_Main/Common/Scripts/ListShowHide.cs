using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListShowHide : MonoBehaviour
{
    [SerializeField] private List<GameObject> obj;

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
    }
    private void Start()
    {
        var dialog = DialogManager.Instance.Get();
        dialog.Hide();
        dialog.OnClickDisplayButton(OnClickShowHideAll);
    }
    public void OnInfoButtonSelected(InfoButton info)
    {
        currentSelected = info;

        UpdateButtonSprite();
    }

    public void Show()
    {
        UpdateButtonSprite();
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
