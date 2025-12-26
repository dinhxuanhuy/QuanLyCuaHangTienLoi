# 🏪 Hệ Thống Quản Lý Cửa Hàng Tiện Lợi

<div align="center">

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET_Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows_Forms-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

*Giải pháp quản lý toàn diện cho cửa hàng tiện lợi*

[Tính năng](#-tính-năng-chính) • [Cài đặt](#-hướng-dẫn-cài-đặt) • [Sử dụng](#-hướng-dẫn-sử-dụng) • [Demo](#-screenshots)

</div>

---

## 📖 Giới Thiệu

Dự án môn học **Công Nghệ Thông Tin** - Hệ thống quản lý cửa hàng tiện lợi được phát triển bằng **C# Windows Forms**, cung cấp giải pháp toàn diện cho việc quản lý hoạt động kinh doanh của cửa hàng tiện lợi.

### 🎯 Mục Tiêu Dự Án

- ✅ Xây dựng hệ thống quản lý cửa hàng tiện lợi hoàn chỉnh
- ✅ Áp dụng kiến thức lập trình Windows Forms và C#
- ✅ Quản lý dữ liệu hiệu quả với SQL Server
- ✅ Tạo giao diện thân thiện, dễ sử dụng cho người dùng

---

## ✨ Tính Năng Chính

<table>
<tr>
<td width="50%">

### 👤 Quản Lý Người Dùng
- 🔐 **Đăng nhập/Đăng ký**: Xác thực người dùng an toàn
- 🔑 **Phân quyền**: Quản lý các vai trò và quyền truy cập khác nhau

### 👥 Quản Lý Nhân Viên
- ➕ Thêm, sửa, xóa thông tin nhân viên
- 📅 Quản lý ca làm việc
- 🔄 Điều chỉnh lịch làm việc linh hoạt

### 📦 Quản Lý Sản Phẩm
- 📋 Danh mục sản phẩm chi tiết
- 📊 Quản lý tồn kho
- 💰 Cập nhật giá và thông tin sản phẩm

### 🏢 Quản Lý Nhà Cung Cấp
- 📝 Thông tin nhà cung cấp
- 📦 Theo dõi đơn hàng nhập
- 🤝 Quản lý mối quan hệ với đối tác

</td>
<td width="50%">

### 🎁 Quản Lý Khuyến Mãi
- 🎉 Tạo chương trình khuyến mãi
- 🎫 Áp dụng mã giảm giá
- ⏰ Thiết lập điều kiện và thời gian khuyến mãi

### 🧾 Quản Lý Hóa Đơn
- 🛒 **Hóa đơn bán**: Tạo và quản lý hóa đơn bán hàng
- 📥 **Hóa đơn nhập**: Quản lý đơn hàng nhập kho
- 📜 Lịch sử giao dịch chi tiết

### 📊 Thống Kê & Báo Cáo
- 💵 **Doanh thu**: Theo dõi doanh thu theo thời gian
- 💸 **Chi phí**: Phân tích chi phí hoạt động
- 📈 **Lợi nhuận**: Tính toán và báo cáo lợi nhuận
- 📉 Biểu đồ trực quan

### 💾 Quản Lý Dữ Liệu
- 🔄 Sao lưu và phục hồi dữ liệu
- 🗄️ Quản lý cơ sở dữ liệu

</td>
</tr>
</table>

---

## 🛠️ Công Nghệ Sử Dụng

| Công nghệ | Phiên bản | Mô tả |
|-----------|-----------|-------|
| ![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white) | 7.3 | Ngôn ngữ lập trình chính |
| ![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=.net&logoColor=white) | Framework 4.7.2 | Nền tảng phát triển |
| ![Guna UI](https://img.shields.io/badge/Guna_UI-FF6B35?style=flat) | 2.0.4.7 | Thư viện UI hiện đại |
| ![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white) | 2012+ | Hệ quản trị cơ sở dữ liệu |
| ![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=flat&logo=visual-studio&logoColor=white) | 2017+ | IDE phát triển |

---

## 📁 Cấu Trúc Dự Án

```
QuanLyCuaHangTienLoi/
│
├── 📂 QuanLyCuaHangTienLoi/
│   ├── 📄 Program.cs                          # Entry point
│   ├── 📄 frmTrangChu.cs                      # Main form
│   │
│   ├── 📂 User Controls/
│   │   ├── 🔐 UC_dangNhap.cs                  # Đăng nhập
│   │   ├── ✍️ UC_dangKy.cs                    # Đăng ký
│   │   ├── 👥 UC_nhanVien.cs                  # Quản lý nhân viên
│   │   ├── 📅 UC_caLamViec.cs                 # Quản lý ca làm việc
│   │   ├── 🔄 UC_caLamViec_dieuChinh.cs       # Điều chỉnh ca làm việc
│   │   ├── 📦 UC_sanPham.cs                   # Quản lý sản phẩm
│   │   ├── 🏢 UC_nhaCungCap.cs                # Quản lý nhà cung cấp
│   │   ├── 🎁 UC_khuyenMai.cs                 # Quản lý khuyến mãi
│   │   ├── 🎫 UC_khuyenMai_apDung.cs          # Áp dụng khuyến mãi
│   │   ├── 🛒 UC_HDBan.cs                     # Hóa đơn bán
│   │   ├── 📥 UC_HDNhap.cs                    # Hóa đơn nhập
│   │   ├── 📊 UC_thongKe.cs                   # Thống kê tổng quan
│   │   ├── 💵 UC_thongKe_doanhThu.cs          # Thống kê doanh thu
│   │   ├── 💸 UC_thongKe_chiPhi.cs            # Thống kê chi phí
│   │   ├── 📈 UC_thongKe_loiNhuan.cs          # Thống kê lợi nhuận
│   │   └── 💾 UC_quanLyDuLieu.cs              # Quản lý dữ liệu
│   │
│   ├── 📂 Properties/
│   │   └── AssemblyInfo.cs
│   │
│   ├── ⚙️ App.config
│   └── 📦 packages.config
│
└── 📖 README.md
```

---

## 🚀 Hướng Dẫn Cài Đặt

### 📋 Yêu Cầu Hệ Thống

- 💻 **OS**: Windows 7/8/10/11
- 🔧 **.NET Framework**: 4.7.2 trở lên
- 🗄️ **Database**: SQL Server 2012 trở lên
- 🛠️ **IDE**: Visual Studio 2017 trở lên (để phát triển)

### 📥 Các Bước Cài Đặt

#### 1️⃣ Clone Repository

```bash
git clone https://github.com/nguyentranhuynhchi/convenience-store-management.git
cd convenience-store-management/QuanLyCuaHangTienLoi
```

#### 2️⃣ Cài Đặt Dependencies

- Mở solution trong Visual Studio
- Restore NuGet packages (Guna.UI2.WinForms)

```
Tools > NuGet Package Manager > Restore NuGet Packages
```

#### 3️⃣ Cấu Hình Database

- Tạo database trong SQL Server
- Cập nhật connection string trong `App.config`
- Chạy script khởi tạo database (nếu có)

```xml
<connectionStrings>
  <add name="ConvenienceStoreDB" 
       connectionString="Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

#### 4️⃣ Build và Chạy

```
Build > Build Solution (Ctrl + Shift + B)
Debug > Start Debugging (F5)
```

---

## 📱 Hướng Dẫn Sử Dụng

### 🔐 Đăng Nhập

1. Khởi động ứng dụng
2. Nhập tên đăng nhập và mật khẩu
3. Chọn vai trò phù hợp
4. Nhấn "Đăng nhập"

### 👥 Quản Lý Nhân Viên

1. Vào menu **"Nhân viên"**
2. Sử dụng các chức năng: **Thêm**, **Sửa**, **Xóa**
3. Quản lý ca làm việc trong tab **"Ca làm việc"**

### 🛒 Quản Lý Bán Hàng

1. Vào **"Hóa đơn bán"**
2. Chọn sản phẩm và số lượng
3. Áp dụng khuyến mãi (nếu có)
4. Thanh toán và in hóa đơn

### 📊 Xem Thống Kê

1. Vào menu **"Thống kê"**
2. Chọn loại báo cáo: **Doanh thu**, **Chi phí**, **Lợi nhuận**
3. Chọn khoảng thời gian
4. Xem biểu đồ và số liệu chi tiết

---

## 👨‍💻 Thành Viên Nhóm

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/nguyentranhuynhchi">
        <img src="https://github.com/nguyentranhuynhchi.png" width="100px;" alt=""/>
        <br />
        <sub><b>Nguyễn Trần Huỳnh Chi</b></sub>
      </a>
      <br />
      <sub>Developer</sub>
    </td>
  </tr>
</table>

---

## 📝 Ghi Chú

> ⚠️ **Lưu ý quan trọng:**
> - Đây là dự án học tập, được phát triển cho mục đích giáo dục
> - Dữ liệu demo được cung cấp để kiểm tra chức năng
> - Vui lòng không sử dụng cho mục đích thương mại

---

## 📄 License

Dự án này được phát triển cho mục đích học tập tại trường Đại học.

---

## 📧 Liên Hệ

Nếu có bất kỳ thắc mắc nào, vui lòng liên hệ qua:

- 📱 **GitHub**: [convenience-store-management](https://github.com/nguyentranhuynhchi/convenience-store-management)
- 📧 **Email**: [Thêm email nếu muốn]

---

<div align="center">

### ⭐ Nếu thấy dự án hữu ích, hãy cho chúng tôi một star nhé!

</div>

---

## 📸 Screenshots

> 🖼️ *Thêm ảnh chụp màn hình ứng dụng tại đây*

- 🔐 Màn hình đăng nhập
- 🏠 Dashboard chính
- 📦 Quản lý sản phẩm
- 🧾 Hóa đơn bán hàng
- 📊 Báo cáo thống kê

---

## 📌 Phiên Bản

### **Version 1.0.0** - Phát hành đầu tiên

- ✅ Các chức năng cơ bản hoàn chỉnh
- ✅ Giao diện người dùng thân thiện
- ✅ Tích hợp đầy đủ các module quản lý

---

## 🔮 Tính Năng Phát Triển Trong Tương Lai

- [ ] 💳 Tích hợp thanh toán trực tuyến
- [ ] 📱 Ứng dụng mobile
- [ ] 🤖 Báo cáo nâng cao với AI
- [ ] 🎯 Quản lý khách hàng thân thiết
- [ ] 📷 Tích hợp máy quét mã vạch
- [ ] ☁️ Đồng bộ dữ liệu cloud
- [ ] 📧 Gửi hóa đơn qua email
- [ ] 💬 Hệ thống thông báo realtime

---

## 🐛 Báo Lỗi

Nếu phát hiện lỗi, vui lòng tạo issue tại:

👉 [https://github.com/nguyentranhuynhchi/convenience-store-management/issues](https://github.com/nguyentranhuynhchi/convenience-store-management/issues)

**Khi báo lỗi, vui lòng cung cấp:**
- Mô tả chi tiết lỗi
- Các bước để tái hiện lỗi
- Screenshot (nếu có)
- Thông tin môi trường (Windows version, .NET Framework version)

---

## 🙏 Cảm Ơn

Cảm ơn các thầy cô đã hướng dẫn và hỗ trợ trong quá trình thực hiện dự án.

---

<div align="center">

**Made with ❤️ by [Nguyễn Trần Huỳnh Chi](https://github.com/nguyentranhuynhchi)**

[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/nguyentranhuynhchi/convenience-store-management)

</div>