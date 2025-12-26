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
    public class BALThongKe
    {
        DAL dp = null;
        public BALThongKe()
        {
            dp = DAL.Instance;
            dp.AdminConn();
        }
        public DataTable DoanhThuTheoNgay()
        {
            return dp.MyExecuteQuery("SELECT * FROM DoanhThuCuoiNgay", null);
        }
        public DataTable DoanhThuTheoThang()
        {
            return dp.MyExecuteQuery("SELECT * FROM DoanhThuCuoiThang", null);
        }
    }
}
