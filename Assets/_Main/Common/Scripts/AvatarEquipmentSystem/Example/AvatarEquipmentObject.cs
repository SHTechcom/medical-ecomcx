using System;
using UnityEngine;

namespace _Main.Common.Scripts.Avatar
{
    public class AvatarEquipmentObject : MonoBehaviour, IAvatarEquipmentObject
    {
        [SerializeField] private AvatarEquipment equipmentInfo;

        public string EquipmentName
        {
            get
            {
                if (equipmentInfo == null) return "NULL INFO OBJECT";
                return equipmentInfo.equipmentName;
            }
        }

        public EquipmentType Type
        {
            get
            {
                if (equipmentInfo == null) return EquipmentType.None;
                return equipmentInfo.type;
            }
        }

        public bool IsEquipped
        {
            get
            {
                if (equipmentInfo == null) return false;
                return AvatarEquipmentSystem.IsEquipped(equipmentInfo);
            }
        }

        private void OnEnable()
        {
            AvatarEquipmentSystem.OnEquipItem += OnEquip;
            AvatarEquipmentSystem.OnUnEquipItem += OnUnEquip;
        }

        private void OnDisable()
        {
            AvatarEquipmentSystem.OnEquipItem -= OnEquip;
            AvatarEquipmentSystem.OnUnEquipItem -= OnUnEquip;
        }

        public void Equip()
        {
            AvatarEquipmentSystem.Equip(equipmentInfo);
        }

        public void UnEquip()
        {
            AvatarEquipmentSystem.UnEquip(equipmentInfo);
        }

        public void OnEquip(AvatarEquipment equipment)
        {
            if (equipment == equipmentInfo)
            {
            }
        }

        public void OnUnEquip(AvatarEquipment equipment)
        {
            if (equipment == equipmentInfo)
            {
            }
        }
    }
}