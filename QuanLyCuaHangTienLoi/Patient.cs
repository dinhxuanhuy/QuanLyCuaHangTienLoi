using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dental_sys
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        private void Patient_Load(object sender, EventArgs e)
        {
            dgvNhaCungCap.Rows.Add(9);
            dgvNhaCungCap.Rows[0].Cells[1].Value = Image.FromFile("photos\\1.png");
            dgvNhaCungCap.Rows[0].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[0].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[0].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[0].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[0].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[0].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[1].Cells[1].Value = Image.FromFile("photos\\5.png");
            dgvNhaCungCap.Rows[1].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[1].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[1].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[1].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[1].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[1].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[2].Cells[1].Value = Image.FromFile("photos\\3.png");
            dgvNhaCungCap.Rows[2].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[2].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[2].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[2].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[2].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[2].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[3].Cells[1].Value = Image.FromFile("photos\\4.png");
            dgvNhaCungCap.Rows[3].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[3].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[3].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[3].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[3].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[3].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[4].Cells[1].Value = Image.FromFile("photos\\5.png");
            dgvNhaCungCap.Rows[4].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[4].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[4].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[4].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[4].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[4].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[5].Cells[1].Value = Image.FromFile("photos\\6.png");
            dgvNhaCungCap.Rows[5].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[5].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[5].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[5].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[5].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[5].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[6].Cells[1].Value = Image.FromFile("photos\\5.png");
            dgvNhaCungCap.Rows[6].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[6].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[6].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[6].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[6].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[6].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[7].Cells[1].Value = Image.FromFile("photos\\1.png");
            dgvNhaCungCap.Rows[7].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[7].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[7].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[7].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[7].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[7].Cells[7].Value = "Jan 21,2020";

            dgvNhaCungCap.Rows[8].Cells[1].Value = Image.FromFile("photos\\1.png");
            dgvNhaCungCap.Rows[8].Cells[2].Value = "Dian Cooper";
            dgvNhaCungCap.Rows[8].Cells[3].Value = "(239)555-2020";
            dgvNhaCungCap.Rows[8].Cells[4].Value = "Cilacap";
            dgvNhaCungCap.Rows[8].Cells[5].Value = "Jan 21,2020 -13:30";
            dgvNhaCungCap.Rows[8].Cells[6].Value = "Jan 21,2020";
            dgvNhaCungCap.Rows[8].Cells[7].Value = "Jan 21,2020";
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
