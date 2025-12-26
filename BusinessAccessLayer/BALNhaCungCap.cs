using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public class BALNhaCungCap
    {
        DAL dp = null;
        public BALNhaCungCap()
        {
            dp = DAL.Instance;
        }

        // Hàm hỗ trợ ExecuteNonQueryWithSqlErrorHandling cần được định nghĩa ở đây
        private bool ExecuteNonQueryWithSqlErrorHandling(string query, SqlParameter[] parameters, ref string err)
        {
            try
            {
                bool result = dp.MyExecuteNonQuery(query, CommandType.StoredProcedure, ref err, parameters);
                return result;
            }
            catch (SqlException ex)
            {
                err = "Lỗi Database: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                err = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
        }

        public DataTable LayNCC()
        {
            return dp.MyExecuteQuery("SELECT MaNCC, TenNCC, DiaChi, SDT FROM NhaCungCap", null);
        }

        // 1. HÀM THÊM (Không có Mã NCC)
        public bool ThemNCC(ref string error, string tenNCC, string diaChi, string sdt)
        {
            return ExecuteNonQueryWithSqlErrorHandling("ThemNCC",
                new SqlParameter[]
                {
                    new SqlParameter("@TenNCC", tenNCC.Trim()),
                    new SqlParameter("@DiaChi", diaChi.Trim()),
                    new SqlParameter("@SDT", sdt.Trim())
                },
                ref error);
        }

        // 2. HÀM CẬP NHẬT (Có Mã NCC là NVARCHAR)
        public bool CapNhatNCC(ref string error, string maNCC, string tenNCC, string diaChi, string sdt)
        {
            return ExecuteNonQueryWithSqlErrorHandling("CapNhatNCC",
                new SqlParameter[]
                {
                    new SqlParameter("@MaNCC", maNCC.Trim()),
                    new SqlParameter("@TenNCC", tenNCC.Trim()),
                    new SqlParameter("@DiaChi", diaChi.Trim()),
                    new SqlParameter("@SDT", sdt.Trim())
                },
                ref error);
        }

        // 3. HÀM XÓA (Có Mã NCC là NVARCHAR)
        public bool XoaNCC(ref string error, string maNCC)
        {
            return ExecuteNonQueryWithSqlErrorHandling("XoaNCC",
                new SqlParameter[]
                {
                    new SqlParameter("@MaNCC", maNCC.Trim())
                },
                ref error);
        }
    }
}