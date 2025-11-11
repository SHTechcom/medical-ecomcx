using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    public Camera targetCamera;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main; // Tự động tìm camera chính nếu chưa gán
    }

    void LateUpdate()
    {
        // Hướng canvas nhìn về camera
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
                         targetCamera.transform.rotation * Vector3.up);
    }
}
