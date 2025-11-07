using System;
using System.Collections.Generic;
using Frank;
using TMPro;
using UnityEngine;

namespace _Main.Common.Scripts.Avatar.UI
{
    public class AvatarCheckListUI : SingletonPersistent<AvatarCheckListUI>
    {
        [SerializeField] private AvatarCheckListItem itemPrefab;
        [SerializeField] private TMP_Text checkListNote;
        [SerializeField] private RectTransform content;

        private readonly List<AvatarCheckListItem> AvatarCheckListItems = new();

        private AvatarEquipmentPreset _preset;
        private EquipmentType _currentCheckListType;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Show(EquipmentType type, AvatarEquipmentPreset preset)
        {
            gameObject.SetActive(true);

            checkListNote.text = string.Empty;
            ClearList();

            _preset = preset;
            _currentCheckListType = type;

            switch (type)
            {
                case EquipmentType.Cloth:
                    Setup(preset.cloths);
                    break;
                case EquipmentType.ToolAndMedicine:
                    Setup(preset.toolsAndMedicines);
                    break;
            }

            UpdateEquipments();
        }

        private void Setup(List<AvatarEquipment> items)
        {
            content.DetachChildren();
            foreach (var avatarEquipment in items)
            {
                var item = SimplePool.Spawn(itemPrefab);
                item.transform.SetParent(content, false);
                item.AddToList(avatarEquipment, this);

                AvatarCheckListItems.Add(item);
            }
        }

        private void ClearList()
        {
            foreach (var item in AvatarCheckListItems)
            {
                item.RemoveFromList();
            }

            AvatarCheckListItems.Clear();
        }

        public void UpdateEquipments()
        {
            if (_preset == null) return;

            var missing = AvatarEquipmentSystem.GetMissingItems(_currentCheckListType, _preset);
            var extra = AvatarEquipmentSystem.GetExtraItems(_currentCheckListType, _preset);

            string message = _currentCheckListType switch
            {
                EquipmentType.Cloth when missing.Count > 0 => _preset.missingClothWarning,
                EquipmentType.ToolAndMedicine when missing.Count > 0 => _preset.missingToolAndMedicineWarning,

                EquipmentType.Cloth when extra.Count > 0 => _preset.extraClothWarning,
                EquipmentType.ToolAndMedicine when extra.Count > 0 => _preset.extraToolAndMedicineWarning,

                EquipmentType.Cloth => _preset.exactlyClothWarning,
                EquipmentType.ToolAndMedicine => _preset.exactlyToolAndMedicineWarning,

                _ => string.Empty
            };

            checkListNote.text = message;
        }
    }
}