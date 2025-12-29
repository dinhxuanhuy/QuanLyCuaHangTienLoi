using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataAccessLayer;

namespace BusinessAccessLayer
{
    public class BALHoaDon
    {
        // Giả định: DAL.Instance đã được thiết lập chuỗi kết nối
        DAL dp = null;

        public BALHoaDon()
        {
            dp = DAL.Instance;
        }

        // Hàm hỗ trợ tìm kiếm chi tiết hóa đơn (Dùng bởi TimKiemHoaDonBan/Nhap)
        private DataTable TimKiemChiTiet(string maHoaDon, bool isBanHang)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@MaHD", SqlDbType.NVarChar);
            param[0].Value = maHoaDon.Trim();

            string viewName = isBanHang ? "view_ChiTietHDBanHang" : "view_ChiTietHoaDonNhapHang";
            string maCol = isBanHang ? "MaHD" : "MaDonNhap";

            // Sửa lại câu query cho chuẩn
            string query = $@"
                SELECT ct.*, sp.TenSP 
                FROM {viewName} ct 
                INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP 
                WHERE ct.{maCol} = @MaHD";

            return dp.MyExecuteQuery(query, param);
        }

        public DataTable LayHoaDonBan()
        {
            // Lấy dữ liệu từ VIEW đã FIX
            return dp.MyExecuteQuery("SELECT * FROM view_HoaDonBanHang", null);
        }

        public DataTable LayLichSuNhapHang()
        {
            string err = "";
            // Gọi Stored Procedure không cần tham số
            return dp.ExecuteQueryDataSet("sp_LayLichSuNhapHang", CommandType.StoredProcedure, null, ref err).Tables[0];
        }

        // Tìm kiếm Chi Tiết Hóa Đơn Bán
        public DataTable TimKiemChiTietHoaDonBan(string maHoaDon)
        {
            return TimKiemChiTiet(maHoaDon, true);
        }

        // Tìm kiếm Chi Tiết Hóa Đơn Nhập
        public DataTable TimKiemChiTietHoaDonNhap(string maHoaDon)
        {
            return TimKiemChiTiet(maHoaDon, false);
        }

        // Lấy thông tin NV từ Mã Hóa Đơn Bán
        public DataTable GetNhanVienBanHang(string maHD)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@MaHD", SqlDbType.NVarChar);
            param[0].Value = maHD.Trim();

            // Lấy Tên NV và Mã NV (vì MaNV là NVARCHAR)
            string query = @"
                SELECT T2.HoTen 
                FROM HDBanHang T1
                INNER JOIN NhanVien T2 ON T1.NVBanHang = T2.MaNV
                WHERE T1.MaHD = @MaHD";

            return dp.MyExecuteQuery(query, param);
        }

        // Lấy danh sách sản phẩm (Để hiện lên bảng chọn hàng)
        public DataTable LayDanhSachSanPham()
        {
            // Chỉ lấy sản phẩm còn hàng (SoLuong > 0)
            string query = "SELECT * FROM vSanPham";
            return dp.MyExecuteQuery(query, null);
        }
        
        // Tạo Hóa Đơn Mới (Bước 1 của quy trình Mua)
        // Trả về: Mã Hóa Đơn vừa tạo (ví dụ: "HD0099")
        public string TaoHDBanHang(string maNV, ref string err)
        {
            // Gọi Stored Procedure: sp_TaoHDBanHang
            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@MaNV", maNV),
        new SqlParameter("@Ngay", DateTime.Now) // Lấy ngày giờ hiện tại
            };

            // Dùng MyExecuteScalar vừa thêm ở DAL để lấy Mã HD trả về
            object result = dp.MyExecuteScalar("sp_TaoHDBanHang", CommandType.StoredProcedure, ref err, param);

            if (result != null)
            {
                return result.ToString(); // Trả về mã HD
            }
            return null; // Lỗi hoặc không có kết quả
        }

        // Thêm Chi Tiết Hóa Đơn (Bước 2 của quy trình Mua - gọi trong vòng lặp)
        public bool ThemChiTietHDBanHang(string maHD, string maSP, int soLuong, ref string err)
        {
            // Gọi Stored Procedure: sp_ThemChiTietHDBanHang
            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@MaHD", maHD),
        new SqlParameter("@MaSP", maSP),
        new SqlParameter("@SoLuong", soLuong)
            };

            // Hàm này trả về True/False
            return dp.MyExecuteNonQuery("sp_ThemChiTietHDBanHang", CommandType.StoredProcedure, ref err, param);
        }
        // Trong class BALHoaDon

        public DataTable LayThongTinKhuyenMai(string maSP)
        {
            try
            {
                // Truy vấn lấy Mức KM và Điều Kiện dựa trên MaSP
                // Chỉ lấy các khuyến mãi đang trong thời gian hiệu lực
                string query = @"
            SELECT k.MucKM, k.DieuKien
            FROM SanPhamKhuyenMai spkm
            INNER JOIN KhuyenMai k ON spkm.MaKM = k.MaKM
            WHERE spkm.MaSP = @MaSP 
              AND k.ThoiGianBatDau <= GETDATE() 
              AND k.ThoiGianKetThuc >= GETDATE()";

                SqlParameter[] param = new SqlParameter[] {
            new SqlParameter("@MaSP", maSP)
        };

                return dp.MyExecuteQuery(query, param);
            }
            catch
            {
                return null;
            }
        }
    }
}