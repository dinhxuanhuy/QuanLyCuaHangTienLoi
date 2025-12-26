using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCQuanLyHoaDon : UserControl
    {
        // ----- BẮT ĐẦU THÊM MỚI -----
        // 1. Định nghĩa khuôn mẫu (delegate) cho sự kiện
        //    Sự kiện này sẽ gửi đi một UserControl
        public delegate void NavigateRequestEventHandler(UserControl uc);

        // 2. Định nghĩa sự kiện (event) dựa trên khuôn mẫu đó
        //    Form cha (frmTrangChu) sẽ "lắng nghe" sự kiện này
        public event NavigateRequestEventHandler NavigateRequest;
        // ----- KẾT THÚC THÊM MỚI -----

        public UCQuanLyHoaDon()
        {
            InitializeComponent();
        }

        private void btn_hoaDonBan_Click(object sender, EventArgs e)
        {
            // Lấy UserControl Doanh Thu mà bạn đã tạo sẵn trong Program.cs
            UserControl ucCanChuyenToi = Program.ucHoaDonBan;

            // 4. Kích hoạt sự kiện và gửi UserControl đi
            //    Dấu ? (null-conditional operator) để kiểm tra xem có ai (frmTrangChu)
            //    đang lắng nghe sự kiện này không. Nếu có, nó sẽ gọi Invoke.
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void btn_hoaDonNhap_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucHoaDonNhap;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
    }
}
