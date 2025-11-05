using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Avatar/Equipment_Preset")]
public class AvatarEquipmentPreset : ScriptableObject
{
    [Header("Cloth")] public List<AvatarEquipment> cloths;
    public string exactlyClothWarning;
    public string missingClothWarning;
    public string extraClothWarning;

    [Header("Tools And Medicines")] public List<AvatarEquipment> toolsAndMedicines;
    public string exactlyToolAndMedicineWarning;
    public string missingToolAndMedicineWarning;
    public string extraToolAndMedicineWarning;
}