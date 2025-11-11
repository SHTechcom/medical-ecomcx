using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour
{
    public Button btn;
    public bool isRotateSelf;
    public Vector3 positionTarget;
    public Vector3 rotationTarget;
    public float distance = 0.3f;

    private void Awake()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void Reset()
    {
        btn = GetComponent<Button>();
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        transform.localScale = Vector3.one * distance;
    }

    private void OnClick()
    {
        if (!isRotateSelf)
        {
            CameraController.Instance.OnCameraAroundTarget(transform, distance);
        }
        else
        {
            CameraController.Instance.OnClickAndDrag(positionTarget, rotationTarget);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Display(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
