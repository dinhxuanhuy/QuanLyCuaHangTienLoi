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
    public partial class UCQuanLyDuLieu : UserControl
    {
        // ----- BẮT ĐẦU THÊM MỚI -----
        // 1. Định nghĩa khuôn mẫu (delegate) cho sự kiện
        //    Sự kiện này sẽ gửi đi một UserControl
        public delegate void NavigateRequestEventHandler(UserControl uc);

        // 2. Định nghĩa sự kiện (event) dựa trên khuôn mẫu đó
        //    Form cha (frmTrangChu) sẽ "lắng nghe" sự kiện này
        public event NavigateRequestEventHandler NavigateRequest;
        // ----- KẾT THÚC THÊM MỚI -----

        public UCQuanLyDuLieu()
        {
            InitializeComponent();
        }

        private void btn_quanLyNhanVien_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucNhanVien;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void btn_sanPham_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucSanPham;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void btn_khuyenMai_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = Program.ucKhuyenMai;
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }

        private void btn_nhaCungCap_Click(object sender, EventArgs e)
        {
            UserControl ucCanChuyenToi = new UCNCC();
            NavigateRequest?.Invoke(ucCanChuyenToi);
        }
    }
}
