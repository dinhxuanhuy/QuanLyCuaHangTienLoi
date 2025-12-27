using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCQuanLyTaiKhoan : UserControl
    {
        // ----- BẮT ĐẦU THÊM MỚI -----
        // 1. Định nghĩa khuôn mẫu (delegate) cho sự kiện
        //    Sự kiện này sẽ gửi đi một UserControl
        public delegate void NavigateRequestEventHandler(UserControl uc);

        // 2. Định nghĩa sự kiện (event) dựa trên khuôn mẫu đó
        //    Form cha (frmTrangChu) sẽ "lắng nghe" sự kiện này
        public event NavigateRequestEventHandler NavigateRequest;
        // ----- KẾT THÚC THÊM MỚI -----
        BALTaiKhoan dbtk = null;
        public UCQuanLyTaiKhoan()
        {
            InitializeComponent();
            UIStyles.ApplyUIStyle(this);
            dbtk = new BALTaiKhoan();
            pan_dangKy.Hide();
            pan_dangNhap.Show();
            pan_dangXuat.Hide();
        }
        bool checkAdmin = false;
    
        // Xử lý Panel đăng ký
        private void btn_dangNhap_Click(object sender, EventArgs e)
        {
            pan_dangKy.Hide();
            pan_dangNhap.Hide();
            pan_dangXuat.Show();
        }
        private void LoadComboboxData()
        {
            // Load ComboBox MaNV
            DataTable dtMaNV = dbtk.LayDSMaNVChuaCoTK();
            cbb_maNV.DataSource = dtMaNV; // Tên ComboBox cho MaNV
            cbb_maNV.DisplayMember = "MaNV";
            cbb_maNV.ValueMember = "MaNV";
            cbb_maNV.Text = "--- Chọn Mã Nhân Viên ---";
            // Load ComboBox VaiTrò/Chức vụ
            DataTable dtChucVu = dbtk.LayDSChucVu();
            cbb_vaiTro.DataSource = dtChucVu; // Tên ComboBox cho Vai Trò
            cbb_vaiTro.DisplayMember = "ChucVu";
            cbb_vaiTro.ValueMember = "ChucVu";
        }
        private void btn_dangKyHeThong_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu nhập (Validation)
            if (cbb_maNV.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Mã Nhân Viên!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbb_maNV.Focus();
                return;
            }
            if (cbb_vaiTro.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Vai Trò (Chức vụ)!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbb_vaiTro.Focus();
                return;
            }

            // Kiểm tra Tên Đăng Nhập
            if (string.IsNullOrWhiteSpace(txt_tenDangNhapDK.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Đăng Nhập!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tenDangNhapDK.Focus();
                return;
            }
            if (txt_tenDangNhapDK.Text.Trim().Length < 5) // Kiểm tra độ dài tối thiểu 5 ký tự
            {
                MessageBox.Show("Tên đăng nhập phải có ít nhất 5 ký tự!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tenDangNhapDK.Focus();
                return;
            }

            // Kiểm tra Mật khẩu và Xác nhận
            if (string.IsNullOrWhiteSpace(txt_matKhauDK.Text) || string.IsNullOrWhiteSpace(txt_xacNhanMatKhauDK.Text))
            {
                MessageBox.Show("Mật khẩu và Xác nhận mật khẩu không được để trống!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(txt_matKhauDK.Text)) txt_matKhauDK.Focus();
                else txt_xacNhanMatKhauDK.Focus();
                return;
            }
            if (txt_matKhauDK.Text.Length < 6) // Kiểm tra độ dài tối thiểu 6 ký tự
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_matKhauDK.Focus();
                return;
            }
            if (txt_matKhauDK.Text != txt_xacNhanMatKhauDK.Text)
            {
                MessageBox.Show("Mật khẩu và Xác nhận mật khẩu không khớp!", "Lỗi Đăng Ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_matKhauDK.Focus();
                return;
            }

            // 2. Lấy dữ liệu
            string maNV = cbb_maNV.SelectedValue.ToString();
            string tenDangNhap = txt_tenDangNhapDK.Text.Trim();
            string matKhau = txt_matKhauDK.Text; // Mật khẩu thô (BAL sẽ Hash)
            string vaiTro = cbb_vaiTro.SelectedValue.ToString();

            // 3. Gọi BAL để thêm tài khoản
            string err = "";
            // Đảm bảo hàm ThemTaiKhoan trong BAL thực hiện việc Hash mật khẩu và kiểm tra trùng TenDangNhap
            bool ketQua = dbtk.ThemTaiKhoan(maNV, tenDangNhap, matKhau, vaiTro, ref err);

            // 4. Xử lý kết quả
            if (ketQua)
            {
                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                txt_tenDangNhapDK.Clear();
                txt_matKhauDK.Clear();
                txt_xacNhanMatKhauDK.Clear();

                // Load lại combobox MaNV để loại bỏ MaNV vừa đăng ký
                LoadComboboxData();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Lỗi: " + err, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_dangNhapHeThong_Click(object sender, EventArgs e)
        {
            frmTrangChu formCha = this.ParentForm as frmTrangChu;

            // 1. Kiểm tra dữ liệu nhập
            if (string.IsNullOrWhiteSpace(txt_tenDangNhap.Text) || txt_tenDangNhap.Text == "Tên đăng nhập")
            {
                MessageBox.Show("Vui lòng nhập aa tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_matKhau.Text) || txt_matKhau.Text == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Gọi BAL để xác thực và lấy vai trò
            string err = "";
            string maVaiTro = "";
            // HÀM BAL NÀY PHẢI TỰ HASH MẬT KHẨU NHẬP VÀO RỒI MỚI SO SÁNH VỚI DB
            bool dangNhapThanhCong = dbtk.KiemTraDangNhap(txt_tenDangNhap.Text.Trim(), txt_matKhau.Text, out maVaiTro, ref err);
            
            if (dangNhapThanhCong)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (formCha != null)
                {
                    if (maVaiTro == "Nhân viên")
                    {
                        checkAdmin = false;
                        formCha.btn_quanLyCa.Enabled = false;
                        formCha.btn_quanLyDuLieu.Enabled = false;
                        formCha.btn_quanLyHoaDon.Enabled = true;
                        formCha.btn_quanLyTaiKhoan.Enabled = true;
                        formCha.btn_thongKe.Enabled = false;
                    }
                    else if (maVaiTro == "Quản lý")
                    {
                        checkAdmin = true;
                        formCha.btn_quanLyCa.Enabled = true;
                        formCha.btn_quanLyDuLieu.Enabled = true;
                        formCha.btn_quanLyHoaDon.Enabled = true;
                        formCha.btn_quanLyTaiKhoan.Enabled = true;
                        formCha.btn_thongKe.Enabled = true;
                    }
                    // 2. Gọi hàm TimMaNV với đầy đủ tham số
                    string maNV = dbtk.TimMaNV(
                        txt_tenDangNhap.Text.Trim(),
                        txt_matKhau.Text, 
                        ref err);
                    frmTrangChu.MaNV = maNV;
                    formCha.CapNhatTenNhanVien(txt_tenDangNhap.Text, maNV);
                    // Chuyển sang UC mong muốn
                    NavigateRequest?.Invoke(Program.ucQuanLyHoaDon);
                    txt_tenDangXuat.Text = txt_tenDangNhap.Text;
                    pan_dangKy.Hide();
                    pan_dangNhap.Hide();
                    pan_dangXuat.Show();
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Lỗi: " + err, "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý Panel đăng xuất
        private void btn_dangXuat_Click(object sender, EventArgs e)
        {
            frmTrangChu formCha = this.ParentForm as frmTrangChu; // Khai báo formCha
            if (formCha != null)
            {
                formCha.btn_quanLyCa.Enabled = false; 
                formCha.btn_quanLyDuLieu.Enabled = false;
                formCha.btn_quanLyHoaDon.Enabled = false;
                formCha.btn_quanLyTaiKhoan.Enabled = false;
                formCha.btn_thongKe.Enabled = false;
            }
            pan_dangKy.Hide();
            pan_dangNhap.Show();
            pan_dangXuat.Hide();
            txt_tenDangNhap.Text = "";
            txt_matKhau.Text = "";
        }

        private void btn_dangKy_Click(object sender, EventArgs e)
        {
            LoadComboboxData();
            if (checkAdmin == true)
            {
                pan_dangKy.Show();
                pan_dangNhap.Hide();
                pan_dangXuat.Hide();
            }
            else
            {
                MessageBox.Show("Chỉ có người quản trị mới có quyền đăng ký tài khoản mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
