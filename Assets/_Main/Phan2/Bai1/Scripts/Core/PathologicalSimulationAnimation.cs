using DG.Tweening;
using UnityEngine;

public class PathologicalSimulationAnimation : MonoBehaviour
{
    [SerializeField] private GameObject nieudao;

    [SerializeField] private GameObject tuyentienlietPivotRight;
    [SerializeField] private GameObject tuyentienlietRight;

    [SerializeField] private GameObject tuyentienlietPivotLeft;
    [SerializeField] private GameObject tuyentienlietLeft;

    private Vector3 scalePivotMax = new Vector3(1.2f, 1.2f, 1);
    private Tween tweenAnimR;
    private Tween tweenAnimL;

    private Outline outlineNieudao;
    private Outline outlineRight;
    private Outline outlineLeft;

    private bool isPlaying;

    public bool Isplaying => isPlaying;

    public void Play()
    {
        DialogManager.Instance.Get().Set("...", "U xơ tuyến tiền liệt (phì đại) gây chèn ép niệu đạo, dẫn đến bí đái.");
        DialogManager.Instance.Get().Show();
        isPlaying = true;
        tweenAnimR = tuyentienlietPivotRight.transform.DOScale(scalePivotMax, 1)
            .SetLoops(-1, LoopType.Yoyo);
        tweenAnimL = tuyentienlietPivotLeft.transform.DOScale(scalePivotMax, 1)
            .SetLoops(-1, LoopType.Yoyo);

        if (!nieudao.TryGetComponent<Outline>(out outlineNieudao))
        {
            outlineNieudao = nieudao.AddComponent<Outline>();
        }
        if (!tuyentienlietRight.TryGetComponent<Outline>(out outlineRight))
        {
            outlineRight = tuyentienlietRight.AddComponent<Outline>();
        }
        if (!tuyentienlietLeft.TryGetComponent<Outline>(out outlineLeft))
        {
            outlineLeft = tuyentienlietLeft.AddComponent<Outline>();
        }

        outlineNieudao.OutlineColor = Color.red;
        outlineRight.OutlineColor = Color.red;
        outlineLeft.OutlineColor = Color.red;

        outlineNieudao.enabled = true;
        outlineRight.enabled = true;
        outlineLeft.enabled = true;
    }

    public void Stop()
    {
        DialogManager.Instance.Get().Hide();
        isPlaying = false;
        tweenAnimR.Kill();
        tweenAnimL.Kill();
        tuyentienlietPivotRight.transform.localScale = Vector3.one;
        tuyentienlietPivotLeft.transform.localScale = Vector3.one;

        outlineNieudao.enabled = false;
        outlineRight.enabled = false;
        outlineLeft.enabled = false;
    }
}
