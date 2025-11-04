using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Avatar/Equipment_Database")]
public class AvatarEquipmentDatabase : ScriptableObject
{
    // Để spawn object ở phần chọn đồ
    public List<AvatarEquipment> cloths;
    public List<AvatarEquipment> tools;
    public List<AvatarEquipment> medicines;
    
#if UNITY_EDITOR
    [Sirenix.OdinInspector.Button]
    public void QuickSetType()
    {
        if (cloths != null && cloths.Count > 0)
        {
            foreach (var item in cloths)
            {
                item.type = EquipmentType.Cloth;
            }
        }

        if (tools != null && tools.Count > 0)
        {
            foreach (var item in tools)
            {
                item.type = EquipmentType.Tool;
            }
        }

        if (medicines != null && medicines.Count > 0)
        {
            foreach (var item in medicines)
            {
                item.type = EquipmentType.Medicine;
            }
        }
    }
#endif
}