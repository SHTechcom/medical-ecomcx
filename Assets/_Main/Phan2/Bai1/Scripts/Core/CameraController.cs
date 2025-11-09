using UnityEngine;

namespace Bai11
{
    public enum CameraMode
    {
        Locked,
        Free
    }

    public class CameraController : MonoBehaviour
    {
        private CameraMode cameraMode;

        public void SetMode(CameraMode mode)
        {
            cameraMode = mode;
        }

        public void SetFixedPositionAndRotation()
        {

        }
    }
}