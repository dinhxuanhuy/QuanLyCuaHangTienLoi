using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace BusinessAccessLayer
{
    public class BALNhanVien
    {
        DAL dp = null;
        public BALNhanVien()
        {
            dp = DAL.Instance;
        }

        public DataTable LayNhanVien()
        {
            // Giả sử vNhanVien đã được cập nhật để trả về MaNV là NVARCHAR
            return dp.MyExecuteQuery("Select * from NhanVien", null);
        }

        public DataTable LayChucVu()
        {
            return dp.MyExecuteQuery("Select ChucVu from BangLuong", null);
        }

        // 1. HÀM THÊM (Bỏ tham số Mã NV - Trigger sẽ sinh)
        public bool ThemNhanVien(ref string error, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string sdt, string chucVu, DateTime ngayTuyenDung)
        {
            // Gọi SP không truyền @MaNV
            return dp.MyExecuteNonQuery("ThemNhanVien",
                CommandType.StoredProcedure,
                ref error,
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", ngaySinh.Date),
                new SqlParameter("@GioiTinh", gioiTinh),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@NgayTuyenDung", ngayTuyenDung.Date)
            );
        }

        // 2. HÀM CẬP NHẬT (MaNV là NVARCHAR)
        public bool CapNhatNhanVien(ref string error, string maNV, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string sdt, string chucVu, DateTime ngayTuyenDung)
        {
            return dp.MyExecuteNonQuery("CapNhatNhanVien",
                CommandType.StoredProcedure,
                ref error,
                new SqlParameter("@MaNV", maNV.Trim()), // ⬅️ TRUYỀN CHUỖI
                new SqlParameter("@HoTen", hoTen),
                new SqlParameter("@NgaySinh", ngaySinh.Date),
                new SqlParameter("@GioiTinh", gioiTinh),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@SDT", sdt),
                new SqlParameter("@ChucVu", chucVu),
                new SqlParameter("@NgayTuyenDung", ngayTuyenDung.Date)
            );
        }

        // 3. HÀM XÓA (MaNV là NVARCHAR)
        public bool XoaNhanVien(ref string error, string maNV)
        {
            return dp.MyExecuteNonQuery("XoaNhanVien",
                CommandType.StoredProcedure,
                ref error,
                new SqlParameter("@MaNV", maNV.Trim()) // ⬅️ TRUYỀN CHUỖI
            );
        }
    }
}