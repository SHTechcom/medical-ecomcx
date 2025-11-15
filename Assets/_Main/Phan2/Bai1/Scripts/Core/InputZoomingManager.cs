using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InputZoomingManager : MonoBehaviour
{
    Vector3 lastPos;
    Quaternion lastRot;

    [SerializeField] Transform _camera;
    [SerializeField] Button backButton;
    [SerializeField] Transform transformMoveToCamera;
    [SerializeField] Transform transformMoveToPrefabs;

    ZoomingAndRotate zoomingTarget;

    [SerializeField] float zoomingDuration = 1f;

    private void Start()
    {
        backButton.onClick.AddListener(OnClickBackButton);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.transform.name);

                if (hit.transform.TryGetComponent<ZoomingAndRotate>(out var a))
                {
                    backButton.gameObject.SetActive(true);
                    // Nếu đã có target đang được chọn rồi thì không cho chọn thêm nữa
                    if (zoomingTarget != null && zoomingTarget.IsTargeted)
                        return;

                    // Lưu lại vị trí/rotation camera trước khi zoom
                    lastPos = _camera.position;
                    lastRot = _camera.rotation;

                    // Zoom camera tới vị trí mong muốn
                    _camera.DOMove(transformMoveToCamera.position, zoomingDuration);
                    _camera.DORotateQuaternion(transformMoveToCamera.rotation, zoomingDuration);

                    // Đánh dấu target
                    zoomingTarget = a;
                    a.OnChosingTarget();

                    // DI CHUYỂN + XOAY OBJECT tới transformMoveToPrefabs
                    a.transform.DOMove(transformMoveToPrefabs.position, zoomingDuration);
                    a.transform.DORotateQuaternion(transformMoveToPrefabs.rotation, zoomingDuration);
                }
            }
        }
    }

    private void OnClickBackButton()
    {
        // BACK LẦN 1: nếu vẫn đang có target → trả target về chỗ cũ
        if (zoomingTarget != null && zoomingTarget.IsTargeted)
        {
            zoomingTarget.OutChosing();   // trong này tự move/rotate về vị trí ban đầu
            zoomingTarget = null;
            return;
        }

        // BACK LẦN 2: không còn target → trả camera về chỗ c
        backButton.gameObject.SetActive(false);
        _camera.DOMove(lastPos, zoomingDuration);
        _camera.DORotateQuaternion(lastRot, zoomingDuration);
    }
}
