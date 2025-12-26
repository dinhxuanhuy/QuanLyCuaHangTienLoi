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

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || txtTenDangNhap.Text == "Tên đăng nhập")
            {
                MessageBox.Show("Vui lòng nhập aa tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || txtMatKhau.Text == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Gọi BAL để xác thực và lấy vai trò
            string err = "";
            string maVaiTro = "";
            // HÀM BAL NÀY PHẢI TỰ HASH MẬT KHẨU NHẬP VÀO RỒI MỚI SO SÁNH VỚI DB
            bool dangNhapThanhCong = dbtk.KiemTraDangNhap(txtTenDangNhap.Text.Trim(), txtMatKhau.Text, out maVaiTro, ref err);

            if (dangNhapThanhCong)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

             
                    string maNV = dbtk.TimMaNV(
                        txtTenDangNhap.Text.Trim(),
                        txtMatKhau.Text,
                        ref err);
                    frmTrangChu main = new frmTrangChu();
                    //main.MaNV = maNV;
                    //main.CapNhatTenNhanVien(txtTenDangNhap.Text, maNV);
                    this.Hide();
                    main.ShowDialog();





            }

            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Lỗi: " + err, "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
