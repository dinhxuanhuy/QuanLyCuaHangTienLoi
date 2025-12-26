using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public class BALQuanLyCa
    {
        // Khai báo DAL Instance
        DAL dp = null;
        public BALQuanLyCa()
        {
            dp = DAL.Instance;
            dp.AdminConn(); // Giả định kết nối đã được thiết lập qua DAL.Instance
        }

        // Hàm hỗ trợ: Lấy danh sách NV cho ComboBox (MaNV là NVARCHAR)
        public DataTable LayDSNhanVien()
        {
            // Giả định sp_LayDSNhanVien trả về MaNV là NVARCHAR
            return dp.MyExecuteQuery("EXEC sp_LayDSNhanVien", null);
        }

        // Hàm đọc dữ liệu (MaCa và MaNV là NVARCHAR)
        public DataTable CaLamViec()
        {
            // Câu truy vấn cần JOIN để lấy HoTen (Giả định View/Query này là chuẩn)
            string query = @"
                SELECT C.MaCa, C.NgayThangNam, C.Buoi, C.MaNV, N.HoTen AS TenNhanVien
                FROM CaLamViec C
                INNER JOIN NhanVien N ON C.MaNV = N.MaNV
                ORDER BY C.NgayThangNam DESC, C.Buoi;
            ";
            return dp.MyExecuteQuery(query, null);
        }

        // Hàm xử lý chung lỗi SQL cho CRUD (Bắt lỗi FK, UNIQUE, v.v.)
        private bool ExecuteNonQueryWithSqlErrorHandling(string query, SqlParameter[] parameters, ref string err)
        {
            try
            {
                bool result = dp.MyExecuteNonQuery(query, CommandType.Text, ref err, parameters);
                return result;
            }
            catch (SqlException ex)
            {
                // Bắt các lỗi SQL phổ biến
                foreach (SqlError error in ex.Errors)
                {
                    switch (error.Number)
                    {
                        case 2627: // Lỗi UNIQUE KEY (Trùng Ngày/Buổi)
                            err = "Ca làm việc đã bị trùng! Ngày và Buổi này đã có nhân viên trực.";
                            return false;
                        case 547: // Lỗi Foreign Key (Mã NV không tồn tại)
                            err = "Mã Nhân Viên không tồn tại trong hệ thống hoặc không thể xóa do ràng buộc.";
                            return false;
                        default:
                            err = "Lỗi Database: " + error.Message;
                            return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                err = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
        }


        // 1. Hàm ThemCa: Thêm Ca Làm Việc (MaNV là STRING)
        public bool ThemCa(DateTime ngay, string buoi, string maNV, ref string err)
        {
            string query = @" 
                INSERT INTO CaLamViec (NgayThangNam, Buoi, MaNV) 
                VALUES (@NgayThangNam, @Buoi, @MaNV); 
            ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NgayThangNam", ngay.Date),
                new SqlParameter("@Buoi", buoi),
                new SqlParameter("@MaNV", maNV.Trim()) // ⬅️ TRUYỀN CHUỖI NVARCHAR
            };
            return ExecuteNonQueryWithSqlErrorHandling(query, parameters, ref err);
        }

        // 2. Hàm SuaCa: Sửa Ca Làm Việc (MaCa và MaNV đều là STRING)
        public bool SuaCa(string maCa, DateTime ngay, string buoi, string maNV, ref string err)
        {
            string query = @"
                UPDATE CaLamViec SET NgayThangNam = @NgayThangNam, Buoi = @Buoi, MaNV = @MaNV 
                WHERE MaCa = @MaCa;
            ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCa", maCa.Trim()),
                new SqlParameter("@NgayThangNam", ngay.Date),
                new SqlParameter("@Buoi", buoi),
                new SqlParameter("@MaNV", maNV.Trim()) // ⬅️ TRUYỀN CHUỖI NVARCHAR
            };
            return ExecuteNonQueryWithSqlErrorHandling(query, parameters, ref err);
        }

        // 3. Hàm XoaCa: Xóa Ca Làm Việc (MaCa là STRING)
        public bool XoaCa(string maCa, ref string err)
        {
            string query = @" DELETE FROM CaLamViec WHERE MaCa = @MaCa; ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaCa", maCa.Trim())
            };
            return ExecuteNonQueryWithSqlErrorHandling(query, parameters, ref err);
        }
    }
}