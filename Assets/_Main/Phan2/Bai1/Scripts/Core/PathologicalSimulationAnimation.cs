using DG.Tweening;
using UnityEngine;

namespace Bai11
{
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
            // Lấy dialog 1 lần cho gọn
            var dialog = DialogManager.Instance.Get();

            // name = "..." (không localize)
            // contentKey = KEY trong StringTable (có localize)
            // -> NHỚ tạo key "Pathology_Prostate_Blocked" trong bảng Table
            dialog.Set("...", "Pathology_Prostate_Blocked");

            dialog.OnClicked(() =>
            {
                dialog.Hide();
            });
            dialog.Show();

            isPlaying = true;

            // Animation tuyến tiền liệt phồng lên – thu vào
            tweenAnimR = tuyentienlietPivotRight.transform
                .DOScale(scalePivotMax, 1f)
                .SetLoops(-1, LoopType.Yoyo);

            tweenAnimL = tuyentienlietPivotLeft.transform
                .DOScale(scalePivotMax, 1f)
                .SetLoops(-1, LoopType.Yoyo);

            // Outline
            if (!nieudao.TryGetComponent(out outlineNieudao))
                outlineNieudao = nieudao.AddComponent<Outline>();

            if (!tuyentienlietRight.TryGetComponent(out outlineRight))
                outlineRight = tuyentienlietRight.AddComponent<Outline>();

            if (!tuyentienlietLeft.TryGetComponent(out outlineLeft))
                outlineLeft = tuyentienlietLeft.AddComponent<Outline>();

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

            // Dừng tween + reset scale
            tweenAnimR?.Kill();
            tweenAnimL?.Kill();
            tuyentienlietPivotRight.transform.localScale = Vector3.one;
            tuyentienlietPivotLeft.transform.localScale = Vector3.one;

            // Tắt outline
            if (!nieudao.TryGetComponent(out outlineNieudao))
                outlineNieudao = nieudao.AddComponent<Outline>();

            if (!tuyentienlietRight.TryGetComponent(out outlineRight))
                outlineRight = tuyentienlietRight.AddComponent<Outline>();

            if (!tuyentienlietLeft.TryGetComponent(out outlineLeft))
                outlineLeft = tuyentienlietLeft.AddComponent<Outline>();

            outlineNieudao.enabled = false;
            outlineRight.enabled = false;
            outlineLeft.enabled = false;
        }
    }
}
