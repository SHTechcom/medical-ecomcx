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

        private bool loop;

        public void Init(ObjectPool<TTPathMover> poolRef, Transform[] path, float duration, Ease ease, bool loop)
        {
            pool = poolRef;
            pathPoints = path;
            moveDuration = duration;
            easeType = ease;
            this.loop = loop;
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

            if (loop)
            {
                moveTween = transform.DOPath(path, moveDuration, PathType.CatmullRom)
                    .SetEase(easeType)
                    .SetLoops(-1, LoopType.Restart);
            }
            else
            {
                moveTween = transform.DOPath(path, moveDuration, PathType.CatmullRom)
                    .SetEase(easeType);
            }
        }

        private void OnDisable()
        {
            moveTween?.Kill();
        }
    }
}
