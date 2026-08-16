# Action_RPG_demo

Unity 3D Game tutorial

# Description

Đây là pet project được phát triển bằng Unity với mục tiêu là xây dựng một dự án hoàn chỉnh, có kiến trúc sạch, dễ bảo
trì và mở rộng lâu dài.

Dự án không chỉ hướng đến việc tạo ra một sản phẩm có thể chơi được mà còn là quá trình học tập và thực hành quy trình
phát triển game chuyên nghiệp, từ thiết kế gameplay, xây dựng kiến trúc phần mềm đến tổ chức mã nguồn và tài liệu.

# Goals

- Học Unity thông qua một dự án thực tế.
- Xây dựng tư duy thiết kế Gameplay và Game System.
- Áp dụng Clean Architecture và các nguyên tắc SOLID.
- Xây dựng codebase dễ đọc, dễ bảo trì và mở rộng.
- Tổ chức tài liệu và quy trình phát triển chuyên nghiệp.

# Technical

- Unity
- C#
- ScriptableObject
- Git, Github

# Scope

Giai đoạn đầu tập trung xây dựng nền tảng của một Top-down Action RPG.

Sau khi kiến trúc ổn định, dự án sẽ tiếp tục mở rộng sang các hệ thống Home, Economy và Multiplayer.

## Controls

| Key             | Action                                  |
|-----------------|-----------------------------------------|
| `W` `A` `S` `D` | Move                                    |
| `R`             | Equip / Unequip Weapon (Draw / Sheathe) |
| `E`             | Collect                                 |
| `Shift`         | Sprint / Run                            |
| `Ctrl`          | Walk                                    |
| `Space`         | Jump                                    |

### Gameplay

* **Move:** Sử dụng `WASD` để di chuyển nhân vật.
* **Equip / Unequip Weapon:** Nhấn `R` để rút hoặc cất vũ khí.
* **Collect:** Nhấn `E` khi ở gần vật phẩm hoặc đối tượng có thể thu thập.
* **Sprint:** Giữ `Shift` để chạy nhanh.
* **Walk:** Giữ `Ctrl` để đi bộ.
* **Jump:** Nhấn `Space` để nhảy.

# Repository Structure

```text
Action_RPG_demo/
│
├── client/                 # Unity Project
│   ├── Assets/
│   ├── Packages/
│   ├── ProjectSettings/
│   └── .gitignore
│
├── docs/                   # Project documentation
│   ├── 01_project.md               # giới thiệu, định hướng
│   ├── 02_game_design.md           # game play
│   ├── 03_architecture.md          # kiến trúc hệ thống
│   ├── 04_development_roadmap.md   # roadmap
│   ├── 05_references.md            # tutorial, research & learning
│   ├── 06_dev_note.md              # nhật ký phát triển, ý tưởng ngắn  
│   ├── 07_decisions.md             # các quyết định quan trọng
│   ├── 08_backlog.md               # ý tưởng tương lai, technical debt, wishlist,..
│   └── Yeu_cau_Unity_1_tuan.docx
│
├── deliverables/           # Submission materials
│   ├── tech_note.md        
│   ├── time_line.md        
│   └── task_list.md
│
├── tools/                  # Development tools & scripts
├── README.md
├── LICENSE
└── .gitignore
```