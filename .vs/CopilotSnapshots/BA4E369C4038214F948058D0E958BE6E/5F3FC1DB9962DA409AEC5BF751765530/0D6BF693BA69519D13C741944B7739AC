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
        public bool KiemTraDangNhap(string tenDN, string matKhauTho, out string maVaiTro, ref string err)
        {
            dp.AdminConn();
            maVaiTro = "";
            err = "";

            // 1. Hash mật khẩu thô mà người dùng nhập vào
            string matKhauNhapHash = HashPassword(matKhauTho);

            // 2. Gọi DAL để lấy Mật khẩu Hash (đã lưu) và Vai Trò từ DB
            DataTable dt;
            try
            {
                dt = LayDuLieuTaiKhoan(tenDN, ref err); // err sẽ được dùng để gán lỗi nếu có
            }
            catch (Exception ex)
            {
                // Bắt lỗi từ DAL (do DAL dùng throw)
                err = "Lỗi DAL: " + ex.Message;
                return false;
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                // err đã có thể được gán ở LayDuLieuTaiKhoan nếu có lỗi, nếu không thì báo lỗi không tìm thấy
                if (string.IsNullOrEmpty(err))
                {
                    err = "Tên đăng nhập không tồn tại.";
                }
                return false;
            }

            // 3. Lấy dữ liệu từ DataTable
            // Lưu ý: Cột trong DB là VaiTro, không phải MaVaiTro
            string matKhauDBHash = dt.Rows[0]["MatKhauHash"].ToString();
            string vaiTroDB = dt.Rows[0]["VaiTro"].ToString(); // Dùng "VaiTro" (theo cấu trúc DB của bạn)

            // 4. So sánh Mật khẩu Hash
            if (matKhauNhapHash == matKhauDBHash)
            {
                maVaiTro = vaiTroDB;
                dp.SetConn(tenDN, matKhauTho); // Cập nhật kết nối với thông tin đăng nhập đúng
                return true; // Thành công
            }
            else
            {
                err = "Mật khẩu không đúng.";
                return false; // Thất bại
            }
        }

        // Hàm hỗ trợ: Lấy dữ liệu tài khoản theo Tên Đăng Nhập
        private DataTable LayDuLieuTaiKhoan(string tenDangNhap, ref string err)
        {
            // Dùng lệnh SELECT trực tiếp thay vì EXEC Stored Procedure
            // Phải đảm bảo không có SQL Injection (đã dùng Parameterized Query)
            string query = "SELECT MatKhauHash, VaiTro FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", tenDangNhap)
            };

            try
            {
                // dp.MyExecuteQuery(query, param) sẽ chạy lệnh SELECT trên.
                return dp.MyExecuteQuery(query, param);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }
        // Trong BALTaiKhoan.cs

        public string TimMaNV(string tenDN, string matKhauTho, ref string err)
        {
            // 1. Hash mật khẩu (Giả định hàm HashPassword đã tồn tại trong class này)
            string matKhauNhapHash = HashPassword(matKhauTho);

            // 2. Lấy dữ liệu tài khoản từ DB (Cần MaNV và MatKhauHash để so sánh)
            // Query chỉ cần lấy MatKhauHash và MaNV
            string query = "SELECT MatKhauHash, MaNV FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

            SqlParameter[] param = new SqlParameter[]
            {
        new SqlParameter("@TenDangNhap", tenDN)
            };

            DataTable dt;
            try
            {
                // Gọi hàm thực thi truy vấn của DAL (Giả định dp là đối tượng DAL)
                dt = dp.MyExecuteQuery(query, param);
            }
            catch (Exception ex)
            {
                err = "Lỗi truy vấn: " + ex.Message;
                return null; // Trả về null nếu có lỗi kết nối/truy vấn
            }

            // 3. Kiểm tra kết quả
            if (dt == null || dt.Rows.Count == 0)
            {
                err = "Tên đăng nhập không tồn tại.";
                return null; // Không tìm thấy tài khoản
            }

            // 4. So sánh mật khẩu hash
            string matKhauDBHash = dt.Rows[0]["MatKhauHash"].ToString();

            if (matKhauNhapHash == matKhauDBHash)
            {
                // Mật khẩu đúng -> Trả về Mã Nhân Viên
                return dt.Rows[0]["MaNV"].ToString();
            }
            else
            {
                err = "Mật khẩu không đúng.";
                return null; // Sai mật khẩu
            }
        }
        // --- KẾT THÚC THÊM HÀM ĐĂNG NHẬP ---
    }
}