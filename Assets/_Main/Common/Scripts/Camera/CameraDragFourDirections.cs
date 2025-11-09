using UnityEngine;

namespace Camera
{
    [AddComponentMenu("Camera/CameraDragFourDirections")]
    public class CameraDragFourDirections : MonoBehaviour
    {
        [Header("Mouse Look Settings")]
        public float sensitivity = 1f;
        public float smoothTime = 0.15f;
        public bool invertY = false;

        [Header("Pitch Limits (Up/Down)")]
        public float minPitch = -80f;
        public float maxPitch = 80f;

        [Header("Keyboard Move Settings")]
        public float moveSpeed = 5f;

        [Header("Movement Limits")]
        public bool useMoveLimits = false;
        public Vector3 minPosition = new Vector3(-10f, 0f, -10f);
        public Vector3 maxPosition = new Vector3(10f, 10f, 10f);
        [Tooltip("Giảm kích thước giới hạn di chuyển camera.")]
        public float moveLimitOffset = 0f;

        [Header("Gizmo Settings")]
        public bool showGizmo = true;
        public bool showOffsetBound = true;
        public Color gizmoOriginalColor = new Color(0f, 1f, 0f, 0.25f);
        public Color gizmoOffsetColor = new Color(1f, 0f, 0f, 0.25f);

        private float yaw;
        private float pitch;
        private Vector3 currentEulerVelocity;
        private bool isDragging = false;

        void Start()
        {
            Vector3 euler = transform.localEulerAngles;
            yaw = euler.y;
            pitch = euler.x;
        }

        void Update()
        {
            HandleMouseLook();
            HandleKeyboardMovement();
        }

        void HandleMouseLook()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * (invertY ? 1 : -1);

                yaw += mouseX;
                pitch += mouseY;
                pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

                Quaternion targetRotation = Quaternion.Euler(pitch, yaw, 0f);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, 1f - Mathf.Exp(-smoothTime * 60f * Time.deltaTime));
            }
        }

        void HandleKeyboardMovement()
        {
            float h = 0f;
            float v = 0f;
            float y = 0f;

            if (Input.GetKey(KeyCode.A)) h = -1f;
            if (Input.GetKey(KeyCode.D)) h = 1f;
            if (Input.GetKey(KeyCode.W)) v = 1f;
            if (Input.GetKey(KeyCode.S)) v = -1f;
            if (Input.GetKey(KeyCode.Space)) y = 1f; // ⬆️ Thêm phím Space để bay lên trên

            Vector3 direction = new Vector3(h, y, v).normalized;
            if (direction.magnitude > 0f)
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.Self);

                if (useMoveLimits)
                {
                    Vector3 pos = transform.position;
                    pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
                    pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);
                    pos.z = Mathf.Clamp(pos.z, minPosition.z, maxPosition.z);
                    transform.position = pos;
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            if (!useMoveLimits || !showGizmo) return;

            Gizmos.color = gizmoOriginalColor;
            Vector3 originalCenter = (minPosition + maxPosition) * 0.5f;
            Vector3 originalSize = maxPosition - minPosition;
            Gizmos.DrawCube(originalCenter, originalSize);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(originalCenter, originalSize);

            if (showOffsetBound)
            {
                Vector3 adjustedMin = minPosition + Vector3.one * moveLimitOffset;
                Vector3 adjustedMax = maxPosition - Vector3.one * moveLimitOffset;
                Vector3 offsetCenter = (adjustedMin + adjustedMax) * 0.5f;
                Vector3 offsetSize = adjustedMax - adjustedMin;

                Gizmos.color = gizmoOffsetColor;
                Gizmos.DrawCube(offsetCenter, offsetSize);
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(offsetCenter, offsetSize);
            }
        }
    }
}