# Unity Structure

```
Assets
├── Art
├── Audio
├── Animation
├── Materials
├── Prefabs
├── Scenes
├── Scripts
├── ScriptableObjects
├── UI
├── Resources
└── Settings
```

```
Scripts
├── Core
├── Character
├── Combat
├── Skill
├── Weapon
├── Enemy
├── Boss
├── Inventory
├── Item
├── Equipment
├── NPC
├── Shop
├── UI
├── Managers
├── Save
└── Utilities
```

# Temp

```text
Assets/
└── _Game/
    │
    ├── Core/
    │   ├── Runtime/
    │   ├── Events/
    │   ├── Utilities/
    │   └── Data/
    │
    ├── Player/
    │   ├── Runtime/
    │   ├── Input/
    │   └── UI/
    │
    ├── Hero/
    │   ├── Runtime/
    │   ├── Data/
    │   ├── Content/
    │   └── UI/
    │
    ├── Combat/
    │   ├── Runtime/
    │   └── Data/
    │
    ├── Skill/
    │   ├── Runtime/
    │   ├── Data/
    │   └── Content/
    │
    ├── Equipment/
    │   ├── Runtime/
    │   ├── Data/
    │   ├── Content/
    │   └── UI/
    │
    ├── Item/
    │   ├── Runtime/
    │   ├── Data/
    │   └── Content/
    │
    ├── Inventory/
    │   ├── Runtime/
    │   └── UI/
    │
    ├── Enemy/
    │   ├── Runtime/
    │   ├── Data/
    │   └── Content/
    │
    ├── World/
    │   ├── Runtime/
    │   ├── Environment/
    │   ├── Resources/
    │   └── Interactable/
    │
    └── Home/
        ├── Runtime/
        ├── Building/
        ├── Farming/
        └── Production/
```

```text
| Folder      | Ý nghĩa                            |
| ----------- | ---------------------------------- |
| `Runtime`   | Code chạy trong game               |
| `Data`      | Definition/config/ScriptableObject |
| `Content`   | Các loại cụ thể của feature        |
| `UI`        | UI thuộc feature                   |
| `Input`     | Input mapping/handler              |
| `Prefabs`   | Chỉ tạo khi prefab đủ nhiều        |
| `Animation` | Chỉ tạo khi animation đủ nhiều     |
| `VFX`       | Chỉ tạo khi VFX đủ nhiều           |
| `Audio`     | Chỉ tạo khi audio đủ nhiều         |

```


# Design Pattern sẽ học

Những pattern quan trọng của Unity:

- Singleton
- State Machine
- ScriptableObject
- Observer/Event
- Object Pool
- Factory
- Interface
- Strategy
- Command (cho Skill)
- Dependency Injection (nếu muốn)