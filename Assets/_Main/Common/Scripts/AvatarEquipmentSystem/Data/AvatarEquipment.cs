using UnityEngine;

[CreateAssetMenu(menuName = "Project/Avatar/Equipment")]
public class AvatarEquipment : ScriptableObject
{
    public string key;
    public string equipmentName;
    public EquipmentType type;

    // prefab, sprite (UI)
}

public enum EquipmentType
{
    Cloth,
    ToolAndMedicine,
}