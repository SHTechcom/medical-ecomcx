using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Avatar/Equipment_Preset")]
public class AvatarEquipmentPreset : ScriptableObject
{
    [Header("Cloth")] public List<AvatarEquipment> cloths;
    [Header("Tools")] public List<AvatarEquipment> tools;
    [Header("Medicines")] public List<AvatarEquipment> medicines;
}