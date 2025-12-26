using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    internal static class Program
    {
        public static UC_caLamViec ucCaLamViec;
        public static UCCaLamViecDieuChinh ucCaLamViecDieuChinh;
        public static UCKhuyenMai ucKhuyenMai;
        public static UCNhaCungCap ucNhaCungCap;
        public static UCNhanVien ucNhanVien;
        public static UCSanPham ucSanPham;
        public static UCThongKe ucThongKe;
        public static UCThongKeChiPhi ucThongKeChiPhi;
        public static UCThongKeDoanhThu ucThongKeDoanhThu;
        public static UCThongKeLoiNhuan ucThongKeLoiNhuan;
        public static UCQuanLyDuLieu ucQuanLyDuLieu;
        public static UCHoaDonNhap ucHoaDonNhap;
        public static UCHoaDonBan ucHoaDonBan;
        public static UCQuanLyHoaDon ucQuanLyHoaDon;
        public static UCQuanLyTaiKhoan ucQuanLyTaiKhoan;
        public static UCThemHDBanHang ucThemHDBanHang;
        public static UCChatbox ucChatbox;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmDangNhap());
        }
    }
}
