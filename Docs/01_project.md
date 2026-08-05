# Tổng quan

Là **pet project** được phát triển bằng **Unity**, với mục tiêu là xây dựng một dự án hoàn chỉnh, có kiến trúc sạch, dễ
bảo trì và có khả năng mở rộng lâu dài.

Dự án không chỉ hướng đến việc tạo ra một bản demo, mà còn là quá trình học tập và thực hành quy trình phát triển game
chuyên nghiệp, từ thiết kế gameplay, xây dựng kiến trúc đến tổ chức mã nguồn và tài liệu.

Dự án được thiết kế với mục tiêu xây dựng một thế giới hoàn chỉnh, nơi **chiến đấu và phát triển kinh tế diễn ra song
song**, thay vì tập trung vào vòng lặp đánh quái - loot đồ truyền thống.

Về lâu dài, trò chơi hướng tới mô hình kết hợp giữa World và Home. Người chơi sẽ khám phá thế giới, chiến đấu và thu
thập tài nguyên trong World, đồng thời phát triển Home theo nhiều hướng như nông trại, chăn nuôi, sản xuất và chế tạo.
Hai hệ thống này sẽ bổ trợ lẫn nhau, tạo nên một vòng lặp gameplay và kinh tế thống nhất.

Trong giai đoạn đầu, dự án tập trung xây dựng nền tảng RPG với các hệ thống cốt lõi như Combat, Hero, Enemy, Equipment,
Inventory và Quest, đồng thời hoàn thiện kiến trúc để sẵn sàng mở rộng sang Home, Economy và Multiplayer trong các giai
đoạn tiếp theo.

# Mục tiêu

- Học Unity thông qua một dự án thực tế.
- Xây dựng kiến trúc có khả năng mở rộng
- Xây dựng tư duy thiết kế gameplay và game system.
- Tạo một codebase sạch, dễ bảo trì

# Định hướng Gameplay

Game thuộc thể loại:

- Top-down Action RPG
- PvE
- Single Player (giai đoạn đầu)

Người chơi điều khiển Hero mình yêu thích để tạo nên hướng build yêu thích phục vụ chiến đấu, thu thập, và phát triển
nhân vật

Gameplay xoay quanh:

- Khám phá bản đồ
- Chiến đấu với quái
- Đánh boss
- Thu thập tài nguyên
- Xây dựng công trình trong Home (tương lai)
- Nâng cấp, chế tạo trang bị
- Build account theo hướng phát triển yêu thích
- Giao lưu, trao đổi mua bán với người chơi khác (tương lai)

# Triết lý thiết kế

Ngay từ đầu, dự án được xây dựng với mục tiêu hỗ trợ những hệ thống lớn trong tương lai.
- Có nhiều Hero trong cùng tài khoản
- Hero có Skill riêng
- Weapon có Skill riêng
- NPC có Shop
- Quest
- Inventory
- Equipment
- Save/Load
- Crafting
- Multiplayer (tương lai)
