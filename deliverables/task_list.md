# Task List

| Task ID | Nội dung                             | Kết quả                                                                 | Status | Depends On             |
|:--------|:-------------------------------------|:------------------------------------------------------------------------|:-------|:-----------------------|
| T001    | Khởi tạo Unity Project               | Unity project chạy được                                                 | [x]    | -                      |
| T002    | Khởi tạo Git Repository              | Có Github repository                                                    | [x]    | -                      |
| T003    | Setup Repository Structure           | client/, docs/, .gitignore, README                                      | [x]    | T002                   |
| T004    | Định hướng tầm nhìn dự án            | docs/01_project.md                                                      | [x]    | -                      |
| T005    | Xác định Architecture cơ bản         | docs/02_architecture.md                                                 | []     | T003                   |
| T006    | Xác định Game Design cơ bản          | docs/03_game_design.md                                                  | []     | T004                   |
| T007    | Xác định Development Roadmap         | docs/04_development_roadmap.md                                          | []     | T004                   |
| T008    | Setup Dev Note / Decisions / Backlog | docs/05_dev_note.md, 06_decisions.md, 07_backlog.md                     | []     | T003                   |
| T009    | Xác định Demo Scope                  |                                                                         | []     | T006, T007             |
| T010    | Setup Unity Folder Structure         | Folder structure cho Scripts, Prefabs, Animations, Materials, Scenes... | []     | T005                   |
| T011    | Setup Input System                   | Input cho Move, Attack, Sprint, Jump, Interact...                       | [x]    | T010                   |
| T012    | Setup Game Scene                     | Có Main/Game Scene dùng để test gameplay                                | [x]    | T010                   |
| T013    | Chuyển Camera sang Top-down          | Camera nhìn Top-down phù hợp với Action RPG                             | [x]    | T012                   |
| T014    | Camera Follow Player                 | Camera follow nhân vật ổn định                                          | [x]    | T013                   |
| T015    | Camera Zoom                          | Zoom in/out phù hợp với Top-down                                        | [x]    | T013                   |
| T016    | Hoàn thiện Player Controller         | Player di chuyển ổn định trong World                                    | [x]    | T011                   |
| T017    | Player Sprint                        | Có chạy nhanh/sprint                                                    | [x]    | T016                   |
| T018    | Player Animation Controller          | Blend/transition animation movement hợp lý                              | [x]    | T016                   |
| T019    | Player Jump                          | Jump hoạt động trong gameplay                                           | [x]    | T016                   |
| T020    | Setup Player Interaction             | Player có thể tương tác với object trong World                          | [x]    | T016                   |
| T021    | Setup Collectible Object             | Tạo hệ thống object có thể thu thập                                     | [x]    | T020                   |
| T022    | Collect Item                         | Player thu thập item và item biến mất                                   | [x]    | T021                   |
| T023    | Item Data                            | Tạo data cơ bản cho Item                                                | []     | T021                   |
| T024    | Setup Weapon                         | Player có thể equip/hold Sword                                          | [x]    | T016                   |
| T025    | Weapon Draw / Sheath                 | Rút kiếm và thu kiếm hoạt động                                          | [x]    | T024                   |
| T026    | Setup Combat Controlle               | Có state/action cho Attack                                              | [x]    | T016                   |
| T027    | Basic Attack                         | Player thực hiện đòn đánh bằng kiếm                                     | [x]    | T025, T026             |
| T028    | Attack Combo                         | Player có thể thực hiện combo attack                                    | []     | T027                   |
| T029    | Weapon Hit Detection                 | Weapon có vùng hit và xác định target khi đánh trúng                    | []     | T027                   |
| T030    | Attack Damage                        | Đòn đánh gây damage lên Enemy                                           | []     | T029                   |
| T031    | Setup Health System                  | Player/Enemy có HP và nhận damage                                       | []     | T030                   |
| T032    | Enemy Base                           | Có Enemy có thể tồn tại trong World                                     | []     | T031                   |
| T033    | Enemy Detection                      | Enemy có thể phát hiện Player                                           | []     | T032                   |
| T034    | Enemy Take Damage                    | Enemy nhận damage và phản ứng khi bị đánh                               | []     | T030, T032             |
| T035    | Enemy Death                          | Enemy chết khi HP về 0                                                  | []     | T034                   |
| T036    | Enemy Spawn                          | Có thể spawn nhiều Enemy trong Scene                                    | []     | T035                   |
| T037    | Enemy Spawn 2                        | Enemy spawn ở những khu vực nhất đinh                                   | []     | T036                   |
| T038    | Loot Drop                            | Enemy chết có thể tạo Loot                                              | []     | T035                   |
| T039    | Loot Pickup                          | Player có thể nhặt Loot                                                 | []     | T036, T022             |
| T040    | Setup Inventory                      | Player có Inventory cơ bản                                              | []     | T023                   |
| T041    | Add Item To Inventory                | Item thu thập được đưa vào Inventory                                    | []     | T040, T022             |
| T042    | Inventory UI                         | Có UI hiển thị Item trong Inventory                                     | []     | T041                   |
| T043    | Weapon / Item Inventory              | Weapon hoặc Loot có thể được quản lý trong Inventory                    | []     | T042                   |
| T044    | Main Menu                            | Main Menu có Play Game                                                  | []     | -                      |
| T045    | Main Menu → Game Scene               | Play Game chuyển vào Gameplay Scene                                     | []     | T044, T012             |
| T046    | Gameplay Loop                        | Hoàn thành flow: Explore → Collect → Combat → Kill → Loot → Inventory   | []     | T022, T028, T035, T042 |
| T047    | Basic Feedback                       | Có feedback khi hit, damage, death, pickup                              | []     | T030, T035, T039       |
| T048    | Prototype Cleanup                    | Xóa code/test không cần thiết, refactor những phần chính                | []     | T046                   |
| T049    | Bug Fix & Polish                     | Fix các bug chính và hoàn thiện prototype                               | []     | T046, T047, T048       |
| T050    | Demo Build                           | Build playable demo hoàn chỉnh                                          | []     | T049                   |
| T051    | Update Documentation                 | Cập nhật Dev Note, Decisions, Architecture theo implementation thực tế  | []     | T050                   |
|         |                                      |                                                                         | []     | -                      |
