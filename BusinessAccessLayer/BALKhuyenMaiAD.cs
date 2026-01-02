using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public class BALKhuyenMaiAD
    {
        DAL dp = null;
        public BALKhuyenMaiAD() { dp = DAL.Instance; }

        // ... (Các hàm lấy danh sách cũ giữ nguyên) ...
        public DataTable LayDanhSachMaSP()
        {
            string sql = "SELECT MaSP, TenSP FROM SanPham";
            return DAL.Instance.MyExecuteQuery(sql, null);
        }
        public DataTable LayDanhSachMaKM()
        {
            string sql = "SELECT MaKM, LoaiKM FROM KhuyenMai";
            return DAL.Instance.MyExecuteQuery(sql, null);
        }

        // --- BỔ SUNG MỚI TỪ ĐÂY ---

        // 1. Kiểm tra sản phẩm đã có khuyến mãi chưa
        public bool KiemTraSanPhamDaCoKM(string maSP)
        {
            string sql = "SELECT COUNT(*) FROM SanPhamKhuyenMai WHERE MaSP = @MaSP";
            SqlParameter[] p = {
                new SqlParameter("@MaSP", maSP)
            };

            string err = "";
            // Sử dụng MyExecuteScalar để lấy về số lượng dòng tìm thấy
            object result = DAL.Instance.MyExecuteScalar(sql, CommandType.Text, ref err, p);

            // Nếu số lượng > 0 nghĩa là đã có
            if (result != null && int.Parse(result.ToString()) > 0)
            {
                return true;
            }
            return false;
        }

        // 2. Thêm khuyến mãi cho sản phẩm
        public bool ThemKhuyenMaiChoSP(string maSP, string maKM, ref string error)
        {
            string sql = "INSERT INTO SanPhamKhuyenMai (MaSP, MaKM) VALUES (@MaSP, @MaKM)";
            SqlParameter[] p = {
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@MaKM", maKM)
            };

            return DAL.Instance.MyExecuteNonQuery(sql, CommandType.Text, ref error, p);
        }
        // 3. Lấy danh sách Sản phẩm - Khuyến mãi (Kết hợp bảng để lấy tên hiển thị cho đẹp)
        public DataTable LayDanhSachSPKM()
        {
            // Kỹ thuật JOIN bảng để thay vì chỉ hiện MaSP, MaKM thì hiện cả TenSP và LoaiKM
            string sql = @"
        SELECT 
            A.MaSP, 
            B.TenSP, 
            A.MaKM, 
            C.LoaiKM 
        FROM SanPhamKhuyenMai A
        INNER JOIN SanPham B ON A.MaSP = B.MaSP
        INNER JOIN KhuyenMai C ON A.MaKM = C.MaKM";

            return DAL.Instance.MyExecuteQuery(sql, null);
        }
        // 4. Xóa khuyến mãi đã áp dụng cho sản phẩm
        public bool XoaKhuyenMaiCuaSP(string maSP, ref string error)
        {
            // Xóa dòng trong bảng trung gian SanPhamKhuyenMai
            string sql = "DELETE FROM SanPhamKhuyenMai WHERE MaSP = @MaSP";

            SqlParameter[] p = {
        new SqlParameter("@MaSP", maSP)
    };

            return DAL.Instance.MyExecuteNonQuery(sql, CommandType.Text, ref error, p);
        }
    }
}