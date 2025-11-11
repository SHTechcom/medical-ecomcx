using DG.Tweening;
using UnityEngine;

namespace Bai11
{
    public class TTPathMover : MonoBehaviour
    {
        [SerializeField] private Transform[] pathPoints;
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private Ease easeType = Ease.Linear;

        private Tween moveTween;
        private ObjectPool<TTPathMover> pool;

        public void Init(ObjectPool<TTPathMover> poolRef, Transform[] path, float duration, Ease ease = Ease.Linear)
        {
            pool = poolRef;
            pathPoints = path;
            moveDuration = duration;
            easeType = ease;
            MoveAlongPath();
        }

        private void MoveAlongPath()
        {
            if (pathPoints == null || pathPoints.Length == 0)
            {
                Debug.LogWarning("PathMover: pathPoints rỗng!");
                return;
            }

            // Convert Transform[] -> Vector3[]
            Vector3[] path = new Vector3[pathPoints.Length];
            for (int i = 0; i < pathPoints.Length; i++)
                path[i] = pathPoints[i].position;

            moveTween?.Kill();

            moveTween = transform.DOPath(path, moveDuration, PathType.CatmullRom)
                .SetEase(easeType)
                .SetLoops(-1, LoopType.Restart);
        }

        private void OnDisable()
        {
            moveTween?.Kill();
        }
    }
}
