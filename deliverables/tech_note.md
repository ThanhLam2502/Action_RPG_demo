# Tech Notes

| Topic                  | Note                                                                                                        |
|:-----------------------|:------------------------------------------------------------------------------------------------------------|
| Rigidbody              | Điều khiển vị trí vật thể thông qua hệ thống mô phỏng vật lý của Unity                                      |
| Character Controller   | Di chuyển Game Object mà không chịu tác động hay ảnh hưởng vật lý                                           |
| Terrain                | Xây dựng và quản lý địa hình                                                                                |
| ScriptableObject       | Dùng để lưu trữ data độc lập -- <chưa tìm hiểu sâu>                                                         |
| C# event               | Sử dụng event để truyền dữ liệu khi có sự kiện xảy ra (dùng nội bộ)                                         |
| Cinemachine            | Package có sẵn để xử lý camera (rất ttootscho xử lý góc nhìn thứ 3)                                         |
| Unity trigger collider | method có sẵn để xử lý va chạm (enter or leave zone)                                                        |
| Physics.OverlapSphere  | cách xử lý kiểm tra va chạm (colider) dựa trên radius                                                       |
| State Pattern          | Xử lý lập trình theo hướng state (tại mỗi thời điểm trong nhóm trạng thái chỉ có 1 state active)            |
| Polling Object         | Kỹ thuật lập trình tái sử dụng đối tương                                                                    |
| Singleton Pattern      | Giữ 1 instance tồn tại duy nhất                                                                             |
| ActionsInput           | Kỹ thuật của unity để lắng nghe Input thay vì theo cách truyền thống Check Key Binding từ Update            |
| Dependency Injection   | Kỹ thuật inject dependency từ bên ngoài, thường thông qua interface; thường sử dụng DI Container để quản lý |
| Interface              | Khỏi tạo contract để những đối tượng sử dụng đều phai triển khai nó (dung phối hợp tối với DJ)              |

## Rigidbody

### Khái niệm

Là một component của Unity giúp **GameObject tham gia vào hệ thống Physics Engine**, cho phép GameObject chịu tác động của các quy luật vật
lý như **lực, trọng lực, va chạm,..**

Khi có Rigidbody, Unity sẽ tự mô phỏng các yếu tố vật lý như:

- Khối lượng (Mass): mặc định 1
- Lực tác động (Force)
- Gia tốc (Acceleration)
- Vận tốc (Velocity)
- Trọng lực (Gravity)
- Va chạm (Collision)
- Động lượng (Momentum)

> GameObject có Rigidbody được xem là một đối tượng trong thế giới vật lý.

### Mục đích

Để Unity Physics Engine tự tính toán chuyển động và va chạm thay vì phải tự xử lý bằng `Công thức` hoặc
`Transform`

Example:

**Không dùng Rigidbody**: tự tính s = v * t

```csharp
    transform.position += direction * speed * Time.deltaTime;
```

**Dùng Rigidbody**

```csharp
void FixedUpdate()
{
    rb.AddForce(direction * force);
}
```

### Khi nào nên sử dụng

- Đối tượng cần mô phỏng vật lý: xe tải, thùng gỗ, viên đạn, hòn đá,...
- Cần sử dụng lực: đẩy, knockback, nổ (văng ra xa), nhảy, lướt,...
- Khi đối tượng cần tương tác vật lý với các Rigidbody khác (ex: Player bị đẩy, xe đâm nhau, bom nổ,...)
- Xử lý va chạm bằng
    - `OnCollisionEnter()`: Va chạm vật lý giữa các Collider không phải Trigger (vật thể sẽ cản nhau )
    - `OnTriggerEnter()`: dùng cho vùng Trigger, không xảy ra phản ứng vật lý (vật thể đi xuyên qua nhau)

**Không nên dùng**

- UI
- Object trang trí
- Static Environment
- Đối tượng không cần mô phỏng vật lý

### Cách sử dụng

#### 1. Thêm Component

- Rigidbody
- Rigidbody2D

#### 2. Cấu hình

| Thuộc tính          | Ý nghĩa                                                                                             |
|---------------------|-----------------------------------------------------------------------------------------------------|
| Mass                | Khối lượng                                                                                          |
| Velocity            | Vận tốc hiện tại                                                                                    |
| Drag                | Lực cản                                                                                             |
| Angular Drag        | Lực cản khi quay                                                                                    |
| Use Gravity         | Có chịu trọng lực                                                                                   |
| Is Kinematic        | Nếu bật, Rigidbody không chịu tác động của lực và trọng lực, chỉ di chuyển bằng code hoặc Animation |
| Constraints         | Khóa các trục                                                                                       |
| Collision Detection | Kiểu phát hiện va chạm: Discrete, Continuous, Continuous Dynamic, Continuous Speculative            |
| Interpolate         | Làm mượt chuyển động                                                                                |
| Sleep Mode          | Khi Rigidbody đứng yên lâu sẽ ngừng tính toán Physics để tối ưu hiệu năng                           |

#### 3. Điều khiển

- `linearVelocity`
- `AddForce()`
- `MovePosition()`
- `MoveRotation()`

### Example

> Cú pháp: AddForce (Vector3 force, ForceMode mode)

```csharp
rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
```

- `force`: Hướng và độ lớn của lực.
- `mode`: Cách áp dụng lực.

| ForceMode      | Ý nghĩa                                  | Ví dụ           |
|----------------|------------------------------------------|-----------------|
| Force          | Tác động lực liên tục (mặc định)         | Xe chạy         |
| Acceleration   | Giống Force nhưng bỏ qua Mass            | Gió             |
| Impulse        | Tác động lực tức thời                    | Nhảy            |
| VelocityChange | Thay đổi trực tiếp Velocity, bỏ qua Mass | Dash, Knockback |

### Lưu ý

- Không nên vừa dùng Rigidbody vừa sửa Transform

```csharp
// Nên
rb.MovePosition(...);
rb.linearVelocity = ...;

// Không nên
transform.position += ...
```

- Không chỉnh Velocity và AddForce cùng lúc nếu không thật sự cần

```csharp
rb.velocity = ..
rb.AddForce(...)
```

- Code vật lý trong `FixedUpdate()` (không code trong `Update()`)
- Chỉ bật `Use Gravity` khi cần.
- Chọn `Collision Detection` phù hợp để tránh xuyên vật thể.
- Không dùng `AddForce()` nếu muốn di chuyển với tốc độ cố định.

```text
Best Practice
- Physics → FixedUpdate()
- Input → Update()
- Không sửa Transform trực tiếp
- Chỉ dùng Rigidbody khi thật sự cần Physics
```

---

## Character Controller

### Khái niệm

Là một component của Unity dùng để điều khiển nhân vật thông qua kinematic character movement, thay vì mô phỏng nhân vật như một vật thể vật lý bằng
Rigidbody.

CharacterController phù hợp với các nhân vật cần movement được kiểm soát trực tiếp, thay vì chuyển động vật lý tự nhiên.

### Mục đích

Dùng CharacterController để xây dựng movement cho character mà lập trình viên muốn kiểm soát trực tiếp tốc độ và hướng di chuyển.

Ví dụ:

```csharp
Vector3 movement = direction * speed;
_characterController.Move(movement * Time.deltaTime);

```

Thay vì để Physics Engine quyết định chuyển động thông qua:
rb.AddForce (...);

Vì vậy, Character Controller thường phù hợp với:

- Player character.
- Enemy character.
- NPC.
- Các nhân vật cần movement ổn định, dễ kiểm soát.

### Khi nào nên sử dụng

Nên sử dụng khi:

- Character cần movement chính xác và predictable.
- Không cần tương tác vật lý phức tạp với các Rigidbody.
- Cần xử lý movement, slope, step và ground bằng gameplay code.
- Muốn tự kiểm soát gravity, jump, acceleration, movement speed,...

### Cách sử dụng

### Example

### Lưu ý

### So sánh với Rigidbody

---