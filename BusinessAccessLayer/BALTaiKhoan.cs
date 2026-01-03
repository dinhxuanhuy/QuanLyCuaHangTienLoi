using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography; 
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public class BALTaiKhoan
    {
        DAL dp = null;
        public BALTaiKhoan()
        {
            dp = DAL.Instance;
        }
        public void AdminConn()
        {
            dp.AdminConn();
        }
        public void SetConn(string tk, string mk)
        {
            dp.SetConn(tk, mk);
        }
        public bool TestConn()
        {
            return dp.TestConnection();
        }
        // Hàm lấy MaNV còn trống (chưa có tài khoản)
        public DataTable LayDSMaNVChuaCoTK()
        {
            string query = "SELECT MaNV FROM NhanVien WHERE MaNV NOT IN (SELECT MaNV FROM TaiKhoan)";
            // Giả định MyExecuteQuery của DAL có sẵn. 
            // Nếu có tham số, ông phải truyền parameters vào, còn đây là SELECT đơn giản.
            return DAL.Instance.MyExecuteQuery(query, null);
        }

        // Hàm lấy danh sách Chức vụ/Vai trò từ bảng BangLuong
        public DataTable LayDSChucVu()
        {
            string query = "SELECT ChucVu FROM BangLuong";
            return DAL.Instance.MyExecuteQuery(query, null);
        }

        // Hàm ThemTaiKhoan phải nhận đủ 4 tham số như UI đã truyền
        public bool ThemTaiKhoan(string maNV, string tenDangNhap, string matKhau, string maVaiTro, ref string err)
        {
            // 1. Hash mật khẩu (security first, bro)
            string matKhauHash = HashPassword(matKhau);

            string sp = "sp_ThemTaiKhoan"; // Tên SP cố định

            // 2. Gọi Stored Procedure
            // MyExecuteNonQuery ĐÃ có CommandType và ref err nên gọi bình thường
            try
            {
                bool ketQua = dp.MyExecuteNonQuery(sp, CommandType.StoredProcedure, ref err,
                    new SqlParameter("@MaNV", maNV),
                    new SqlParameter("@TenDangNhap", tenDangNhap),
                    new SqlParameter("@MatKhauHash", matKhauHash),
                    new SqlParameter("@VaiTro", maVaiTro)
                );
                return ketQua;
            }
            catch (Exception ex)
            {
                // DAL của bạn dùng throw new Exception, nên ta bắt ở đây để gán vào ref err
                err = ex.Message;
                return false;
            }
        }

        // Hàm Hash Mật khẩu giả định
        private string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển chuỗi thành mảng byte
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                // Tính toán hash
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển mảng byte hash thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // "X2" là định dạng hex 2 chữ số
                }
                return sb.ToString(); // Trả về chuỗi MD5 in hoa (ví dụ: E10ADC...)
            }
        }
        public bool KiemTraDangNhap(string tenDN, string matKhauTho, out string maVaiTro, out string maNV, ref string err)
        {
            // Bước 1: Dùng quyền Admin để đọc dữ liệu (QUAN TRỌNG)
            dp.AdminConn();

            maVaiTro = "";
            maNV = ""; // Khởi tạo giá trị rỗng để trả về
            err = "";

            // Hash mật khẩu nhập vào
            string matKhauNhapHash = HashPassword(matKhauTho);

            // Lấy dữ liệu từ DB (bao gồm cả MaNV)
            DataTable dt;
            try
            {
                dt = LayDuLieuTaiKhoan(tenDN, ref err);
            }
            catch (Exception ex)
            {
                err = "Lỗi DAL: " + ex.Message;
                return false;
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                if (string.IsNullOrEmpty(err)) err = "Tên đăng nhập không tồn tại.";
                return false;
            }

            // Lấy dữ liệu từ dòng đầu tiên
            string matKhauDBHash = dt.Rows[0]["MatKhauHash"].ToString();
            string vaiTroDB = dt.Rows[0]["VaiTro"].ToString();
            string maNV_DB = dt.Rows[0]["MaNV"].ToString(); // LẤY MÃ NV TỪ DB

            // So sánh mật khẩu
            if (matKhauNhapHash == matKhauDBHash)
            {
                // Gán dữ liệu ra biến out
                maVaiTro = vaiTroDB;
                maNV = maNV_DB; // TRẢ MÃ NV RA NGOÀI

                // Bước 2: Sau khi lấy xong hết dữ liệu thì mới chuyển kết nối sang User (SetConn)
                dp.SetConn(tenDN, matKhauTho);
                return true;
            }
            else
            {
                err = "Mật khẩu không đúng.";
                return false;
            }
        }

        // Hàm hỗ trợ: Lấy dữ liệu tài khoản theo Tên Đăng Nhập
        private DataTable LayDuLieuTaiKhoan(string tenDangNhap, ref string err)
        {
            // THÊM ", MaNV" VÀO CÂU TRUY VẤN
            string query = "SELECT MatKhauHash, VaiTro, MaNV FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", tenDangNhap)
            };

            try
            {
                return dp.MyExecuteQuery(query, param);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }
    }
}