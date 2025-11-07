using System;
using UnityEngine;
using TMPro;

namespace _Main.Common.Scripts.Avatar.UI
{
    public class AvatarEquipmentInteractUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text infoText;

        public static Action<AvatarEquipmentObject> RayCastAction;
        
        private void Awake()
        {
            RayCastAction += OnRayCast;
        }

        private void Start()
        {
            infoText.text = "";
        }

        private void OnDestroy()
        {
            RayCastAction -= OnRayCast;
        }

        private void OnRayCast(AvatarEquipmentObject obj)
        {
            if (obj != null)
            {
                if (obj.Type == EquipmentType.Cloth)
                {
                    infoText.text = $"Nhấn [E] để {(!obj.IsEquipped ? "mặc" : "tháo")} <b>{obj.EquipmentName}</b>";
                }
                else if (obj.Type == EquipmentType.ToolAndMedicine)
                {
                    infoText.text = $"Nhấn [E] để {(!obj.IsEquipped ? "trang bị" : "bỏ")} <b>{obj.EquipmentName}</b>";
                }
            }
            else
            {
                infoText.text = "";
            }
        }
    }
}