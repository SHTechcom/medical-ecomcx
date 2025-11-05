using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Common.Scripts.Avatar.UI
{
    public class AvatarCheckListItem : MonoBehaviour
    {
        [SerializeField] private Image checkImage;
        [SerializeField] private TMP_Text itemName;

        private AvatarEquipment _avatarEquipment;
        private AvatarCheckListUI _checkListUI;

        public bool IsEquipped
        {
            get
            {
                if (_avatarEquipment == null) return false;
                return AvatarEquipmentSystem.IsEquipped(_avatarEquipment);
            }
        }

        public void RefreshInfo(AvatarEquipment equipment)
        {
            _avatarEquipment = equipment;
            UpdateVisual();
        }

        public void AddToList(AvatarEquipment equipment, AvatarCheckListUI checkListUI)
        {
            _checkListUI = checkListUI;
            RefreshInfo(equipment);

            AvatarEquipmentSystem.OnEquipItem += OnEquip;
            AvatarEquipmentSystem.OnUnEquipItem += OnUnEquip;
        }

        public void RemoveFromList()
        {
            _avatarEquipment = null;
            _checkListUI = null;
            
            AvatarEquipmentSystem.OnEquipItem -= OnEquip;
            AvatarEquipmentSystem.OnUnEquipItem -= OnUnEquip;

            SimplePool.Despawn(gameObject);
        }

        private void UpdateVisual()
        {
            if (_avatarEquipment == null)
            {
                SimplePool.Despawn(gameObject);
                return;
            }

            itemName.text = _avatarEquipment.equipmentName;
            checkImage.color = IsEquipped ? Color.green : Color.red;
        }

        public void OnEquip(AvatarEquipment equip)
        {
            if (equip == _avatarEquipment)
            {
                UpdateVisual();
                _checkListUI.UpdateEquipments();
            }
        }

        public void OnUnEquip(AvatarEquipment equip)
        {
            if (equip == _avatarEquipment)
            {
                UpdateVisual();
                _checkListUI.UpdateEquipments();
            }
        }
    }
}