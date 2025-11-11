using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Bai11
{
    public class TTPathSpawner : MonoBehaviour
    {
        [Header("Path Settings")]
        [SerializeField] private Transform[] pathPoints;
        [SerializeField] private TTPathMover pathMoverPrefab;
        [SerializeField] private int poolSize = 10;

        [Header("Movement Settings")]
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private Ease easeType = Ease.Linear;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 0.5f; // mỗi 0.5s spawn 1 mover
        [SerializeField] private int maxActive = 5; // số mover tối đa hoạt động cùng lúc

        private ObjectPool<TTPathMover> pool;
        private float timer;
        private List<TTPathMover> items = new List<TTPathMover>();

        public bool loop;

        private void Start()
        {
            pool = new ObjectPool<TTPathMover>(pathMoverPrefab, poolSize, transform);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                TrySpawnMover();
            }
        }

        private void OnDisable()
        {
            Hide();
        }

        private void TrySpawnMover()
        {
            int activeCount = poolSize - pool.CountInactive;
            if (activeCount >= maxActive) return;

            var mover = pool.Get();
            items.Add(mover);
            mover.transform.position = pathPoints[0].position;
            mover.Init(pool, pathPoints, moveDuration, easeType, loop);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            items.ForEach(i => pool.Release(i));
            items.Clear();
            gameObject.SetActive(false);
        }
    }
}
