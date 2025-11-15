using DG.Tweening;
using UnityEngine;

namespace Bai11
{
    public class TTPathMover : MonoBehaviour
    {
        private ObjectPool<TTPathMover> pool;
        private Tween moveTween;

        public void Init(ObjectPool<TTPathMover> pool, Transform[] pathPoints, float moveDuration, Ease easeType, bool loop)
        {
            this.pool = pool;

            // Chuẩn bị path
            Vector3[] path = new Vector3[pathPoints.Length];
            for (int i = 0; i < pathPoints.Length; i++)
                path[i] = pathPoints[i].position;

            // Tạo tween di chuyển
            moveTween?.Kill();
            moveTween = transform.DOPath(path, moveDuration, PathType.Linear)
                                 .SetEase(easeType)
                                 .OnComplete(OnComplete);

            if (loop)
                moveTween.SetLoops(-1, LoopType.Restart);

            moveTween.timeScale = 1f;
        }

        private void OnComplete()
        {
            // Khi chạy xong thì trả về pool
            pool.Release(this);
        }

        public void SetSpeed(float speed)
        {
            if (moveTween != null && moveTween.IsActive())
                moveTween.timeScale = speed;
        }

        public void Pause()
        {
            if (moveTween != null && moveTween.IsActive())
                moveTween.Pause();
        }

        public void Resume()
        {
            if (moveTween != null && moveTween.IsActive())
                moveTween.Play();
        }

        private void OnDisable()
        {
            moveTween?.Kill();
            moveTween = null;
        }
    }
}
