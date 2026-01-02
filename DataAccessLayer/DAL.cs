using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public class DAL
    {
        // Giữ đối tượng duy nhất của lớp DAL theo mẫu thiết kế Singleton.
        private static DAL instance = null;
        // Khai báo biến thành viên connectionString, có thể được truy cập ở bất kỳ lớp nào.
        public string connectionString = "";
        // Lưu trữ đối tượng đại diện cho kết nối vật lý đang hoạt động (hoặc ngắt kết nối) tới cơ sở dữ liệu SQL Server.
        SqlConnection conn = null;
        // Lưu trữ đối tượng đại diện cho lệnh truy vấn (SQL command) sẽ được thực thi trên cơ sở dữ liệu.
        SqlCommand comm = null;

        // Constructor - ngăn chặn việc tạo đối tượng (new DAL()) từ bên ngoài lớp DAL.
        private DAL()
        {
        }

        // Thuộc tính Singleton. Đảm bảo chỉ có DUY NHẤT một đối tượng DAL được khởi tạo và trả về.
        public static DAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL();
                }
                return instance;
            }
        }
        [KernelFunction]
        [Description(@"Thực thi câu lệnh truy vấn SQL (T-SQL) để lấy dữ liệu từ cơ sở dữ liệu Cửa Hàng Tiện Lợi.
        Dưới đây là cấu trúc cơ sở dữ liệu (Database Schema) để bạn tham khảo khi viết Query:

        --- 1. NHÓM QUẢN LÝ NHÂN SỰ & TÀI KHOẢN ---
        - NhanVien (Thông tin nhân viên): MaNV (Khóa chính), HoTen, NgaySinh, GioiTinh, DiaChi, SDT, ChucVu, NgayTuyenDung.
        - BangLuong (Lương theo chức vụ): ChucVu, Luong.
        - CaLamViec (Lịch làm việc): MaCa, NgayThangNam, Buoi (Sáng/Chiều/Tối), MaNV.
        - TaiKhoan (Đăng nhập hệ thống): MaTK, MaNV, TenDangNhap, MatKhauHash, VaiTro (Admin/NhanVien).

        --- 2. NHÓM HÀNG HÓA & KHO BÃI ---
        - SanPham (Danh sách hàng hóa): MaSP (Khóa chính), TenSP, SoLuong (Tồn kho), GiaNhap, GiaBan, NgaySX, HanSD, DonVi (Cái/Hộp...), TinhTrang, NVQuanLy, MaNCC.
        - NhaCungCap (Đối tác cung cấp): MaNCC, TenNCC, DiaChi, SDT.
        - LichSuNhapHang (Lịch sử nhập kho): MaLS, MaSP, TenSP, SoLuongNhap, DonGiaNhap, ThanhTien, NgayNhap, MaNV (Người nhập), MaNCC.

        --- 3. NHÓM BÁN HÀNG & DOANH THU ---
        - HDBanHang (Hóa đơn bán ra): MaHD (Khóa chính), NVBanHang (Mã NV bán), Ngay (Ngày bán), TongGiaTri (Tổng tiền hóa đơn).
        - ChiTietHDBanHang (Chi tiết từng món trong hóa đơn): MaHD, MaSP, SoLuong, DonGia, TongKhuyenMai, ThanhTien.
        - DoanhThuCuoiNgay (Tổng kết ngày): MaDT, Ngay, TongTien.
        - DoanhThuCuoiThang (Tổng kết tháng): MaDT, ThoiGianBatDau, ThoiGianKetThuc, ChiPhiNhapHang, DoanhThu, LoiNhuan.

        --- 4. NHÓM KHUYẾN MÃI ---
        - KhuyenMai (Các chương trình giảm giá): MaKM, LoaiKM, MucKM, DieuKien, ThoiGianBatDau, ThoiGianKetThuc.
        - SanPhamKhuyenMai (Sản phẩm nào áp dụng mã nào): MaSP, MaKM.
")]
        public async Task<string> ExecuteSqlQueryAsync([Description("The SQL query to execute.")] string sqlQuery)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Instance.connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            var results = new List<Dictionary<string, object>>();
                            while (await reader.ReadAsync())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[reader.GetName(i)] = reader.GetValue(i);
                                }
                                results.Add(row);
                            }
                            try
                            {
                                return JsonSerializer.Serialize(results, new JsonSerializerOptions
                                {
                                    WriteIndented = true
                                });
                            }
                            catch (Exception jsonEx)
                            {
                                return $"Error serializing results to JSON: {jsonEx.Message}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error executing SQL query: {ex.Message}";
            }
        }

        // Thiết lập Connection String bằng SQL Server Authentication (User ID và Password).
        public void SetConn(string ID, string Pass)
        {
            connectionString = "Data Source=localhost;Initial Catalog=QuanLyCuaHangTienLoi;User " +
                "ID= " + ID + ";Password= " + Pass + ";" +
                "Encrypt=True;TrustServerCertificate=True";
            conn = new SqlConnection(connectionString);
            comm = conn.CreateCommand();
        }

        // Thiết lập Connection String sử dụng Windows Authentication (Integrated Security).
        public void AdminConn()
        {
            //connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangTienLoi;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            connectionString = "Data Source=localhost;Initial Catalog=QuanLyCuaHangTienLoi;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            conn = new SqlConnection(connectionString);
            comm = conn.CreateCommand();
        }

        // Thực thi truy vấn SELECT (Trả về dữ liệu) bằng SqlDataAdapter và Parameterized Query.
        public DataTable MyExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    foreach (SqlError error in ex.Errors)
                    {
                        if (error.Number == 547)
                        {
                            throw new Exception("Lỗi ràng buộc dữ liệu: " + error.Message);
                        }
                        else if (error.Number == 2627)
                        {
                            throw new Exception("Lỗi trùng khóa chính: " + error.Message);
                        }
                        else if (error.Number == 229 || error.Number == 916 || error.Number == 4060)
                        {
                            throw new Exception("Không có quyền truy cập: " + error.Message);
                        }
                        else
                        {
                            throw new Exception("Lỗi truy vấn dữ liệu: " + error.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi truy vấn dữ liệu: " + ex.Message);
                }
            }
            return dt;
        }

        // Thực thi các lệnh INSERT/UPDATE/DELETE (Non-Query) hoặc Stored Procedure với tham số.
        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string err, params SqlParameter[] param)
        {
            bool f = false;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.Parameters.Clear();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            foreach (SqlParameter p in param)
                comm.Parameters.Add(p);
            try
            {
                comm.ExecuteNonQuery();
                f = true;
            }
            catch (SqlException ex)
            {
                foreach (SqlError error in ex.Errors)
                {
                    if (error.Number == 547)
                    {
                        throw new Exception("Lỗi ràng buộc dữ liệu: " + error.Message);
                    }
                    else if (error.Number == 2627)
                    {
                        throw new Exception("Lỗi trùng khóa chính: " + error.Message);
                    }
                    else if (error.Number == 229 || error.Number == 916 || error.Number == 4060)
                    {
                        throw new Exception("Không có quyền truy cập: " + error.Message);
                    }
                    else
                    {
                        throw new Exception("Lỗi truy vấn dữ liệu: " + error.Message);
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return f;
        }

        // Hàm thực thi trả về 1 giá trị duy nhất (Dùng để lấy Mã HD vừa tạo)
        public object MyExecuteScalar(string strSQL, CommandType ct, ref string err, params SqlParameter[] param)
        {
            object result = null;
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                conn.Open();

                comm.Parameters.Clear();
                comm.CommandText = strSQL;
                comm.CommandType = ct;

                if (param != null)
                {
                    foreach (SqlParameter p in param)
                        comm.Parameters.Add(p);
                }

                // ExecuteScalar trả về ô đầu tiên của dòng đầu tiên
                result = comm.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                err = ex.Message;
                throw; // Ném lỗi ra để BAL hoặc Form xử lý
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct, SqlParameter[] param, ref string err)
        {
            DataSet ds = new DataSet();
            try
            {
                // Mở kết nối nếu chưa mở
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();

                // Cấu hình lệnh
                comm.Parameters.Clear();
                comm.CommandText = strSQL;
                comm.CommandType = ct; // Quan trọng: Cho phép chọn StoredProcedure

                // Thêm tham số nếu có
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                        comm.Parameters.Add(p);
                }

                // Dùng Adapter để đổ dữ liệu vào DataSet
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                err = ex.Message;
            }
            finally
            {
                // Luôn đóng kết nối sau khi xong
                conn.Close();
            }
            return ds;
        }
        // Kiểm tra kết nối.
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Ngắt kết nối.
        public void CloseConnection()
        {
            conn.Close();
        }
    }
}
