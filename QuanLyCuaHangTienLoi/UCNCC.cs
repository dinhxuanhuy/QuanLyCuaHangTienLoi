using BusinessAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangTienLoi
{
    public partial class UCNCC : UserControl
    {
        BALNhaCungCap dbncc = null;
        public UCNCC()
        {
            InitializeComponent();
            dbncc = new BALNhaCungCap();
        }


        private void UCNCC_Load(object sender, EventArgs e)
        {
            LoadData();

        }
        void LoadData()
        {
            try
            {
                dgvNhaCungCap.ReadOnly = true;
                dgvNhaCungCap.AllowUserToAddRows = false;
                dgvNhaCungCap.AllowUserToDeleteRows = false;
                dgvNhaCungCap.MultiSelect = false;

                dgvNhaCungCap.DataSource = dbncc.LayNCC();

                ResetText();

                if (dgvNhaCungCap.Rows.Count > 0)
                {
                    dgvNhaCungCap_CellClick(dgvNhaCungCap, new DataGridViewCellEventArgs(0, 0));
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu Nhà Cung Cấp! Lỗi: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhaCungCap.RowCount) return;
            int r = e.RowIndex;

            try
            {
                // Lấy DataRow an toàn
                DataRowView rowView = dgvNhaCungCap.Rows[r].DataBoundItem as DataRowView;
                if (rowView == null) return;
                DataRow row = rowView.Row;

                Func<string, string> getRowValue = (colName) =>
                {
                    object val = row[colName];
                    return (val == null || val == DBNull.Value) ? "" : val.ToString();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu Nhà Cung Cấp.\nChi tiết: " + ex.Message, "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
