using System;
using _Main.Common.Scripts.Avatar.UI;
using UnityEngine;

namespace _Main.Common.Scripts.Avatar
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 200f;
        [SerializeField] private Transform playerBody;
        [SerializeField] private LayerMask raycastMask;
        [SerializeField] private AvatarEquipmentPreset preset;

        private float _xRotation = 0f;
        private AvatarEquipmentObject _currentObject;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Rotate();
            RaycastAndInteract();
        }

        private void Rotate()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }

        private void RaycastAndInteract()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, 10f, raycastMask))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out AvatarEquipmentObject equipmentObject))
                    {
                        _currentObject = equipmentObject;
                        AvatarEquipmentInteractUI.RayCastAction?.Invoke(equipmentObject);
                    }
                    else SetRayCastNull();
                }
                else SetRayCastNull();
            }
            else SetRayCastNull();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentObject != null)
                {
                    _currentObject.Equip();
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AvatarCheckListUI.Instance.Show(EquipmentType.Cloth, preset);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                AvatarCheckListUI.Instance.Show(EquipmentType.ToolAndMedicine, preset);
            }

            void SetRayCastNull()
            {
                _currentObject = null;
                AvatarEquipmentInteractUI.RayCastAction?.Invoke(null);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.forward * 10f);
        }
    }
}