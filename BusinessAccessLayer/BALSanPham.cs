using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessAccessLayer
{
    public class BALSanPham
    {
        DAL dp = null;

        public BALSanPham()
        {
            dp = DAL.Instance;
        }

        // --- HÀM HỖ TRỢ TRUY VẤN DỮ LIỆU ---

        public DataTable LaySanPham()
        {
            // CẬP NHẬT: Lấy từ VIEW để ẩn cột GiaNhap, chỉ hiện GiaBan
            return dp.MyExecuteQuery("SELECT * FROM SanPham", null);
        }

        public DataTable LayMaNV()
        {
            return dp.MyExecuteQuery("SELECT MaNV FROM NhanVien", null);
        }

        public DataTable LayMaNCC()
        {
            return dp.MyExecuteQuery("SELECT MaNCC FROM NhaCungCap", null);
        }

        // --- HÀM HỖ TRỢ XỬ LÝ LỖI (Giữ nguyên logic của bạn) ---
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

        // --- HÀM THAO TÁC (CRUD) ĐÃ ĐƯỢC CẬP NHẬT ---

        // 1. HÀM THÊM (Cập nhật: Thêm GiaNhap, đổi DonGia -> GiaBan)
        public bool ThemSanPham(ref string error,
            string tenSP, int soLuong,
            decimal giaNhap, decimal giaBan, // <--- CẬP NHẬT THAM SỐ GIÁ
            DateTime ngaySX, DateTime hanSD,
            string donVi, string tinhTrang,
            string nvQuanLy, string maNCC)
        {
            return ExecuteNonQueryWithSqlErrorHandling("ThemSanPham",
                new SqlParameter[]
                {
                    new SqlParameter("@TenSP", tenSP.Trim()),
                    new SqlParameter("@SoLuong", soLuong),
                    new SqlParameter("@GiaNhap", giaNhap), // Mới
                    new SqlParameter("@GiaBan", giaBan),   // Đổi tên từ DonGia
                    new SqlParameter("@NgaySX", ngaySX.Date),
                    new SqlParameter("@HanSD", hanSD.Date),
                    new SqlParameter("@DonVi", donVi.Trim()),
                    new SqlParameter("@TinhTrang", tinhTrang.Trim()),
                    new SqlParameter("@NVQuanLy", nvQuanLy.Trim()),
                    new SqlParameter("@MaNCC", maNCC.Trim())
                },
                ref error);
        }

        // 2. HÀM CẬP NHẬT (Cập nhật: Logic Nhập Thêm Hàng)
        public bool NhapHang(ref string err, string maSP, int soLuongNhap, string maNV)
        {
            return dp.MyExecuteNonQuery("sp_NhapHangVaGhiLichSu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaSP", maSP),
                new SqlParameter("@SoLuongNhap", soLuongNhap),
                new SqlParameter("@MaNV", maNV));
        }
    }
}