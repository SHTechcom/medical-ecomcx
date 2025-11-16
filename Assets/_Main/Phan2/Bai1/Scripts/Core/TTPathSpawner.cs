using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Bai11
{
    public class TTPathSpawner : MonoBehaviour
    {
        private UIMaleMainView UIMaleMainView => MaleViewManager.Instance.GetView<UIMaleMainView>();

        [Header("Path Settings")]
        [SerializeField] private Transform[] pathPoints;
        [SerializeField] private TTPathMover pathMoverPrefab;
        [SerializeField] private int poolSize = 10;

        [Header("Movement Settings")]
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private Ease easeType = Ease.Linear;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 0.5f; // mỗi 0.5s spawn 1 mover
        [SerializeField] private int maxActive = 5;          // số mover tối đa hoạt động cùng lúc

        private ObjectPool<TTPathMover> pool;
        private float timer;
        private List<TTPathMover> items = new List<TTPathMover>();

        public bool loop;

        [Header("Speed Control")]
        [SerializeField] private float minSpeed = 0.25f;
        [SerializeField] private float maxSpeed = 4f;
        [SerializeField] private float speedStep = 0.25f;

        private float currentSpeed = 1f; // 1 = bình thường

        [SerializeField] Button slow, fast, stop;
        bool isStop = false;
        [SerializeField] Sprite stopImage,playImage;
        [SerializeField] Image imageNeedChange;

        // bật/tắt việc spawn & chạy tween
        private bool isSpawning = true;

        private void Start()
        {
            pool = new ObjectPool<TTPathMover>(pathMoverPrefab, poolSize, transform);

            slow.onClick.AddListener(OnClickSpeedDown);
            fast.onClick.AddListener(OnClickSpeedUp);
            stop.onClick.AddListener(OnClickStop);

        }

        private void Update()
        {
            if (!isSpawning) return; // Stop thì không spawn thêm

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

            // áp speed hiện tại cho mover vừa spawn
            mover.SetSpeed(currentSpeed);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            // trả tất cả mover về pool
            items.ForEach(i =>
            {
                if (i != null)
                    pool.Release(i);
            });
            items.Clear();

            gameObject.SetActive(false);
        }

        // ========== CÁC HÀM NÚT ==========

        // STOP: dừng lại tại chỗ, không reset, không trả pool
        public void OnClickStop()
        {

            if (isStop) 
            {
                isStop= false;
                imageNeedChange.sprite = stopImage;
                isSpawning = true;
                ResumeAllMovers();
                Debug.Log("[TTPathSpawner] PLAY");
            }
            else
            {
                isStop = true;
                imageNeedChange.sprite = playImage;
                isSpawning = false;
                PauseAllMovers();
                Debug.Log("[TTPathSpawner] STOP (PAUSE)");
            }
        }

        // Tăng tốc
        public void OnClickSpeedUp()
        {
            currentSpeed = Mathf.Clamp(currentSpeed + speedStep, minSpeed, maxSpeed);
            ApplySpeedToAllMovers();
            Debug.Log("[TTPathSpawner] Speed Up -> " + currentSpeed);
        }

        // Giảm tốc
        public void OnClickSpeedDown()
        {
            currentSpeed = Mathf.Clamp(currentSpeed - speedStep, minSpeed, maxSpeed);
            ApplySpeedToAllMovers();
            Debug.Log("[TTPathSpawner] Speed Down -> " + currentSpeed);
        }

        // ========== HỖ TRỢ ==========

        private void ApplySpeedToAllMovers()
        {
            foreach (var mover in items)
            {
                if (mover == null) continue;
                mover.SetSpeed(currentSpeed);
            }
        }

        private void PauseAllMovers()
        {
            foreach (var mover in items)
            {
                if (mover == null) continue;
                mover.Pause();
            }
        }

        private void ResumeAllMovers()
        {
            foreach (var mover in items)
            {
                if (mover == null) continue;
                mover.Resume();
                mover.SetSpeed(currentSpeed); // đảm bảo speed đúng sau resume
            }
        }
    }
}
