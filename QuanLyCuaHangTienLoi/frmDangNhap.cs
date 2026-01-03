using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessAccessLayer;

namespace QuanLyCuaHangTienLoi
{
    public partial class frmDangNhap : Form
    {
        BALTaiKhoan dbtk;
        public frmDangNhap()
        {
            dbtk = new BALTaiKhoan();
            InitializeComponent();
        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || txtTenDangNhap.Text == "Tên đăng nhập")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || txtMatKhau.Text == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Khai báo biến để hứng dữ liệu trả về
            string err = "";
            string maVaiTro = "";
            string maNV = ""; // Biến chứa Mã Nhân Viên

            // 3. Gọi hàm KiemTraDangNhap (Phiên bản mới có 5 tham số)
            bool dangNhapThanhCong = dbtk.KiemTraDangNhap(
                txtTenDangNhap.Text.Trim(),
                txtMatKhau.Text,
                out maVaiTro,
                out maNV, // Hứng Mã NV tại đây
                ref err
            );

            if (dangNhapThanhCong)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Lưu vào các biến toàn cục (Static)
                UCQuanLyTaiKhoan.TenDangNhapHienTai = txtTenDangNhap.Text.Trim();
                UCQuanLyTaiKhoan.VaiTroHienTai = maVaiTro;

                // GÁN MÃ NV VÀO BIẾN TOÀN CỤC CỦA TRANG CHỦ
                frmTrangChu.MaNV = maNV;

                // 5. Khởi tạo và hiển thị Form Trang Chủ
                Program.frmMain = new frmTrangChu();

                // Cập nhật giao diện (Label tên, mã...)
                Program.frmMain.CapNhatTenNhanVien(txtTenDangNhap.Text, maNV);

                this.Hide();
                Program.frmMain.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Lỗi: " + err, "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
