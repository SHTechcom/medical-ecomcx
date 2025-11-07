# Avatar Equipment System

## Tính năng chính

- Quản lý 2 loại trang bị: **Cloths** và **ToolAndMedicine**
- Equip/UnEquip items với hệ thống events
- Kiểm tra trạng thái trang bị
- So sánh và validate với preset
- Áp dụng nhanh preset
- Tìm items thiếu/thừa so với preset

## Property

1. Scriptable Object:
    - `AvatarEquipment` (ScriptableObject chứa thông tin item)
    - `AvatarEquipmentPreset` (ScriptableObject chứa danh sách items)
    - `EquipmentType` enum: loại của item

## Sử dụng cơ bản

### Khởi tạo

```csharp
// Khởi tạo system
var equipmentSystem = new AvatarEquipmentSystem();

// Hoặc khởi tạo với preset có sẵn (Tự động equip các item)
var equipmentSystem = new AvatarEquipmentSystem(myPreset);
```

### Equip/UnEquip Items

```csharp
// Equip một item (Nếu gọi 2 lần thì sẽ UnEquip)
AvatarEquipmentSystem.Equip(clothItem);

// UnEquip một item
AvatarEquipmentSystem.UnEquip(clothItem);

// Kiểm tra xem item có đang được equip không
bool isEquipped = AvatarEquipmentSystem.IsEquipped(clothItem);
```

### Làm việc với Preset

```csharp
// Áp dụng nhanh preset (xóa hết và equip theo preset)
AvatarEquipmentSystem.QuickApplyPreset(myPreset);

// Kiểm tra xem trang bị hiện tại có khớp với preset không
bool isValid = AvatarEquipmentSystem.CheckValidAllPreset(myPreset);

// Kiểm tra từng loại trang bị
bool clothsValid = AvatarEquipmentSystem.CheckValidPreset(EquipmentType.Cloth, myPreset);
```

### Tìm items thiếu/thừa

```csharp
// Lấy danh sách items thiếu so với preset
var missingItems = AvatarEquipmentSystem.GetMissingItems(EquipmentType.Cloth, myPreset);

// Lấy danh sách items thừa so với preset
var extraItems = AvatarEquipmentSystem.GetExtraItems(EquipmentType.Cloth, myPreset);
```

### Events

```csharp
// Đăng ký sự kiện khi equip item
AvatarEquipmentSystem.OnEquipItem += OnItemEquipped;

// Đăng ký sự kiện khi unequip item
AvatarEquipmentSystem.OnUnEquipItem += OnItemUnEquipped;

private void OnItemEquipped(AvatarEquipment item)
{
    Debug.Log($"Equipped: {item.equipmentName}");
}

private void OnItemUnEquipped(AvatarEquipment item)
{
    Debug.Log($"UnEquipped: {item.equipmentName}");
}

// Nhớ hủy đăng ký khi không dùng nữa
```

## API Reference

### Properties

| Property                             | Type                    | Description                        |
|--------------------------------------|-------------------------|------------------------------------|
| `CurrentClothsEquipments`            | `List<AvatarEquipment>` | Danh sách quần áo đang equip       |
| `CurrentToolsAndMedicinesEquipments` | `List<AvatarEquipment>` | Danh sách công cụ/thuốc đang equip |

### Events

| Event           | Type                      | Description               |
|-----------------|---------------------------|---------------------------|
| `OnEquipItem`   | `Action<AvatarEquipment>` | Được gọi khi equip item   |
| `OnUnEquipItem` | `Action<AvatarEquipment>` | Được gọi khi unequip item |

### Methods

#### `Init()`: Khởi tạo/reset hệ thống, xóa tất cả trang bị hiện tại.

#### `Equip(AvatarEquipment item)`: Equip một item. Nếu item đã được equip, sẽ tự động unequip.

#### `UnEquip(AvatarEquipment item)`: UnEquip một item.

#### `IsEquipped(AvatarEquipment item)`: Kiểm tra xem item có đang được equip hay không.

**Returns:** `bool` - true nếu đang equip, false nếu không

#### `CheckValidAllPreset(AvatarEquipmentPreset preset)`: Kiểm tra xem tất cả trang bị hiện tại có khớp với preset hay không.

**Returns:** `bool` - true nếu khớp hoàn toàn, false nếu không

#### `CheckValidPreset(EquipmentType type, AvatarEquipmentPreset preset)`: Kiểm tra một loại trang bị cụ thể so với preset.

**Returns:** `bool` - true nếu khớp, false nếu có items thiếu/thừa hoặc preset null

#### `QuickApplyPreset(AvatarEquipmentPreset preset)`: Xóa tất cả trang bị hiện tại và áp dụng preset mới.

#### `GetMissingItems(EquipmentType type, AvatarEquipmentPreset preset)`: Lấy danh sách items thiếu so với preset.

**Returns:** `List<AvatarEquipment>` - Danh sách items thiếu, hoặc null nếu preset null

#### `GetExtraItems(EquipmentType type, AvatarEquipmentPreset preset)`: Lấy danh sách items thừa so với preset.

**Returns:** `List<AvatarEquipment>` - Danh sách items thừa, hoặc null nếu preset null

#### `ClearEquipment()`: Xóa tất cả trang bị hiện tại.

## Lưu ý quan trọng

⚠️ **Events**: Nhớ hủy đăng ký events khi ko sử dụng nữa để tránh memory leaks và lỗi null reference.

⚠️ **Null Safety**: Các phương thức đều có xử lý null, nhưng nên kiểm tra trước khi truyền vào để đảm bảo.

## Ví dụ

Có thể xem ở **AvatarEquipmentCallExample.cs**