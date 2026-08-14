# Decision for Project Topdown RPG Demo

## DEC-001: Feature-Based Project Structure

### Decision

Sử dụng feature-based structure làm cách tổ chức chính cho gameplay code.

Các thành phần thuộc cùng một feature được đặt gần nhau, thay vì tổ chức toàn bộ project theo module.

Ví dụ
```text
Hero/ 
├── Runtime/    - system
├── Data/ 
├── Content/ 
└── Prefabs/
```

### Reason

Project thực hiện theo hướng solo dev.

Việc tổ chức theo module hóa khiến các thành phần của cùng một feature bị phân tán.
(gây khó khăn khi cần tìm các content prefab/script liên quan)

## DEC-002 — CharacterController for Hero

### Decision

Hero locomotion sử dụng CharacterController.

### Reason

Kiểm soát cách di chuyển, tương tác của Hero 