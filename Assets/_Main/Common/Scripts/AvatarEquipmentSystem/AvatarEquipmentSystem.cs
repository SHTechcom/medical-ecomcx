using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AvatarEquipmentSystem : MonoBehaviour
{
    [ShowInInspector, ReadOnly] public readonly List<AvatarEquipment> CurrentClothsEquipments = new();
    [ShowInInspector, ReadOnly] public readonly List<AvatarEquipment> CurrentToolsEquipments = new();
    [ShowInInspector, ReadOnly] public readonly List<AvatarEquipment> CurrentMedicinesEquipments = new();

    // Đăng ký sự kiện cần phải gọi hủy sự kiện tránh lỗi
    public static Action<AvatarEquipment> OnEquipItem;
    public static Action<AvatarEquipment> OnUnEquipItem;

    /// <summary>
    /// Equip item, có thể gọi lần nữa để un equip
    /// </summary>
    /// <param name="item">so item</param>
    public void Equip(AvatarEquipment item)
    {
        if (item == null)
        {
            LogWarning($"Equip failed: item null");
        }

        switch (item.type)
        {
            case EquipmentType.Cloth:
                InternalEquip(item, CurrentClothsEquipments);
                break;
            case EquipmentType.Tool:
                InternalEquip(item, CurrentToolsEquipments);
                break;
            case EquipmentType.Medicine:
                InternalEquip(item, CurrentMedicinesEquipments);
                break;
            default:
                LogWarning($"Equip failed: unknown type {item.type}");
                break;
        }
    }

    /// <summary>
    /// Un equip item
    /// </summary>
    /// <param name="item">so item</param>
    public void UnEquip(AvatarEquipment item)
    {
        if (item == null)
        {
            LogWarning("UnEquip failed: item null");
            return;
        }

        switch (item.type)
        {
            case EquipmentType.Cloth:
                InternalUnEquip(item, CurrentClothsEquipments);
                break;
            case EquipmentType.Tool:
                InternalUnEquip(item, CurrentToolsEquipments);
                break;
            case EquipmentType.Medicine:
                InternalUnEquip(item, CurrentMedicinesEquipments);
                break;
        }
    }

    /// <summary>
    /// Check valid tất cả các equip type so với preset
    /// </summary>
    /// <param name="preset">so preset</param>
    /// <returns>true/false</returns>
    public bool CheckValidAllPreset(AvatarEquipmentPreset preset)
    {
        if (preset == null)
        {
            LogWarning("Preset is null");
            return false;
        }

        bool clothValid = CheckValidPreset(EquipmentType.Cloth, preset);
        bool toolValid = CheckValidPreset(EquipmentType.Tool, preset);
        bool medValid = CheckValidPreset(EquipmentType.Medicine, preset);

        bool allValid = clothValid && toolValid && medValid;

        if (allValid) Log("Preset matches current equipment");
        else Log("Preset does not match current equipment");

        return allValid;
    }

    /// <summary>
    /// Check valid các đồ theo type so với preset
    /// Trả về false nếu preset null hoặc có đồ thừa hoặc thiếu, các trường hợp còn lại trả về true (bao gồm cả trường hợp list rỗng)
    /// </summary>
    /// <param name="type">EquipmentType</param>
    /// <param name="preset">so preset</param>
    /// <returns>true/false</returns>
    public bool CheckValidPreset(EquipmentType type, AvatarEquipmentPreset preset)
    {
        if (preset == null) return false;

        switch (type)
        {
            case EquipmentType.Cloth:
                return CheckListMatch("Cloths", preset.cloths, CurrentClothsEquipments);
            case EquipmentType.Tool:
                return CheckListMatch("Tools", preset.tools, CurrentToolsEquipments);
            case EquipmentType.Medicine:
                return CheckListMatch("Medicines", preset.medicines, CurrentMedicinesEquipments);
        }

        return false;
    }

    /// <summary>
    /// Apply nhanh preset (Force equip)
    /// </summary>
    /// <param name="preset">so preset</param>
    public void QuickApplyPreset(AvatarEquipmentPreset preset)
    {
        if (preset == null) return;

        ClearEquipment();

        if (preset.cloths != null)
        {
            foreach (var item in preset.cloths) Equip(item);
        }

        if (preset.tools != null)
        {
            foreach (var item in preset.tools) Equip(item);
        }

        if (preset.medicines != null)
        {
            foreach (var item in preset.medicines) Equip(item);
        }

        Log("Preset applied.");
    }

    /// <summary>
    /// Lấy danh sách item thiếu theo type và so với preset, nếu preset null trả về null
    /// </summary>
    /// <param name="type">EquipmentType</param>
    /// <param name="preset">so preset</param>
    /// <returns>Danh sách đồ thiếu</returns>
    public List<AvatarEquipment> GetMissingItems(EquipmentType type, AvatarEquipmentPreset preset)
    {
        if (preset == null) return null;

        switch (type)
        {
            case EquipmentType.Cloth:
                return GetMissingItems(preset.cloths, CurrentClothsEquipments);
            case EquipmentType.Tool:
                return GetMissingItems(preset.tools, CurrentToolsEquipments);
            case EquipmentType.Medicine:
                return GetMissingItems(preset.medicines, CurrentMedicinesEquipments);
        }

        return null;
    }

    /// <summary>
    /// Lấy danh sách item thừa theo type và so với preset, nếu preset null trả về null
    /// </summary>
    /// <param name="type">EquipmentType</param>
    /// <param name="preset">so preset</param>
    /// <returns>Danh sách đồ thừa</returns>
    public List<AvatarEquipment> GetExtraItems(EquipmentType type, AvatarEquipmentPreset preset)
    {
        if (preset == null) return null;

        switch (type)
        {
            case EquipmentType.Cloth:
                return GetExtraItems(preset.cloths, CurrentClothsEquipments);
            case EquipmentType.Tool:
                return GetExtraItems(preset.tools, CurrentToolsEquipments);
            case EquipmentType.Medicine:
                return GetExtraItems(preset.medicines, CurrentMedicinesEquipments);
        }

        return null;
    }

    /// <summary>
    /// Clear danh sách item đang equip
    /// </summary>
    public void ClearEquipment()
    {
        CurrentClothsEquipments.Clear();
        CurrentToolsEquipments.Clear();
        CurrentMedicinesEquipments.Clear();
    }

    private static void InternalEquip(AvatarEquipment item, List<AvatarEquipment> list)
    {
        if (item == null) return;

        if (list.Contains(item))
        {
            if (list.Remove(item))
            {
                OnUnEquipItem?.Invoke(item);
                Log($"UnEquipped {item.equipmentName}");
            }
        }
        else
        {
            list.Add(item);
            OnEquipItem?.Invoke(item);
            Log($"Equipped {item.equipmentName}");
        }
    }

    private static void InternalUnEquip(AvatarEquipment item, List<AvatarEquipment> list)
    {
        if (item == null) return;
        if (list.Remove(item))
        {
            OnUnEquipItem?.Invoke(item);
            Log($"UnEquipped {item.equipmentName}");
        }
    }

    private static bool CheckListMatch(string label, List<AvatarEquipment> presetList, List<AvatarEquipment> currentList)
    {
        var missingItems = GetMissingItems(presetList, currentList);

        var extraItems = GetExtraItems(presetList, currentList);

        if (missingItems.Count == 0 && extraItems.Count == 0)
        {
            Log($"{label}: Match");
            return true;
        }

        if (missingItems.Count > 0) LogWarning($"{label} missing: {string.Join(", ", missingItems)}");
        if (extraItems.Count > 0) LogWarning($"{label} extra: {string.Join(", ", extraItems)}");

        return false;
    }

    private static List<AvatarEquipment> GetMissingItems(List<AvatarEquipment> presetList, List<AvatarEquipment> currentList)
    {
        var missingItems = new List<AvatarEquipment>();
        foreach (var item in presetList)
        {
            if (!currentList.Contains(item))
                missingItems.Add(item);
        }

        return missingItems;
    }

    private static List<AvatarEquipment> GetExtraItems(List<AvatarEquipment> presetList, List<AvatarEquipment> currentList)
    {
        var extraItems = new List<AvatarEquipment>();
        foreach (var item in currentList)
        {
            if (!presetList.Contains(item))
                extraItems.Add(item);
        }

        return extraItems;
    }

    #region HELPER

    private static void Log(object o)
    {
        Debug.Log($"[AVATAR EQUIPMENT SYSTEM]: {o}");
    }

    private static void LogWarning(object o)
    {
        Debug.LogWarning($"[AVATAR EQUIPMENT SYSTEM]: {o}");
    }

    private static void LogError(object o)
    {
        Debug.LogError($"[AVATAR EQUIPMENT SYSTEM]: {o}");
    }

    private static void Log(object o, UnityEngine.Object context)
    {
        Debug.Log($"[AVATAR EQUIPMENT SYSTEM]: {o}", context);
    }

    private static void LogWarning(object o, UnityEngine.Object context)
    {
        Debug.LogWarning($"[AVATAR EQUIPMENT SYSTEM]: {o}", context);
    }

    private static void LogError(object o, UnityEngine.Object context)
    {
        Debug.LogError($"[AVATAR EQUIPMENT SYSTEM]: {o}", context);
    }

    #endregion
}