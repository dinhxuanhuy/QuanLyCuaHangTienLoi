using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public class BALKhuyenMai
    {
        DAL dp = null;
        public BALKhuyenMai() { dp = DAL.Instance; }

        public DataTable LayKhuyenMai()
        {
            return dp.MyExecuteQuery("SELECT * FROM KhuyenMai", null);
        }

        // 1. THÊM (5 tham số, không có MaKM)
        public bool ThemKhuyenMai(ref string error, string loaiKM, decimal mucKM, int dieuKien, DateTime batDau, DateTime ketThuc)
        {
            // Gọi SP ThemKhuyenMai (đã sửa trong SQL để bỏ @MaKM)
            // Bạn cần viết hàm ExecuteNonQueryWithSqlErrorHandling hoặc dùng dp.MyExecuteNonQuery trực tiếp
            return dp.MyExecuteNonQuery("ThemKhuyenMai", CommandType.StoredProcedure, ref error,
                new SqlParameter("@LoaiKM", loaiKM),
                new SqlParameter("@MucKM", mucKM),
                new SqlParameter("@DieuKien", dieuKien),
                new SqlParameter("@ThoiGianBatDau", batDau.Date),
                new SqlParameter("@ThoiGianKetThuc", ketThuc.Date)
            );
        }

        // 2. CẬP NHẬT (6 tham số, có MaKM)
        public bool CapNhatKhuyenMai(ref string error, string maKM, string loaiKM, decimal mucKM, int dieuKien, DateTime batDau, DateTime ketThuc)
        {
            return dp.MyExecuteNonQuery("CapNhatKhuyenMai", CommandType.StoredProcedure, ref error,
                new SqlParameter("@MaKM", maKM),
                new SqlParameter("@LoaiKM", loaiKM),
                new SqlParameter("@MucKM", mucKM),
                new SqlParameter("@DieuKien", dieuKien),
                new SqlParameter("@ThoiGianBatDau", batDau.Date),
                new SqlParameter("@ThoiGianKetThuc", ketThuc.Date)
            );
        }

        // 3. XÓA
        public bool XoaKhuyenMai(ref string error, string maKM)
        {
            return dp.MyExecuteNonQuery("XoaKhuyenMai", CommandType.StoredProcedure, ref error,
                new SqlParameter("@MaKM", maKM)
            );
        }
    }
}