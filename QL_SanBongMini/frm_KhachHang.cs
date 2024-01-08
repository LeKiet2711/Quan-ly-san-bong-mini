using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QL_SanBongMini
{
    public partial class frm_KhachHang : Form
    {
        DataTable dataKH;
        Dbconnect db = new Dbconnect();
        public frm_KhachHang()
        {
            InitializeComponent();
        }

        
        private void frm_KhachHang_Load(object sender, EventArgs e)
        {
            //loaddata();
            load_dgv();
            dataGridView1.AutoResizeColumns();
            DataColumn[] key = new DataColumn[1];
            key[0] = dataKH.Columns[0];
            dataKH.PrimaryKey = key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtHovaten.Text == "" || txtDiachi.Text == "" || txtSdt.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                string makh = GetUniqueID("KhachHang");
                txtMakh.Text = makh;
                //string sql = "insert into KhachHang values('"+txtMakh.Text+"',N'"+txtHovaten.Text+"',N'"+txtDiachi.Text+"','"+txtSdt.Text+"','"+txtEmail.Text+"')";
                //int kq = db.getNonquery(sql);

                DataRow newrow = dataKH.NewRow();
                string sql = "select *from KhachHang";
                newrow["IDKhachHang"] = makh;
                newrow["HoVaTen"] = txtHovaten.Text;
                newrow["Diachi"] = txtDiachi.Text;
                newrow["Sdt"] = txtSdt.Text;
                newrow["Email"] = txtEmail.Text;
                dataKH.Rows.Add(newrow);
                int kq = db.updateDatable(dataKH, sql);

                if (kq > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
        }


        private string GetUniqueID(string table)
        {
            string sql = "select max(IDKhachHang) as maxID from " + table;
            SqlDataReader dr = db.getdataReader(sql);
            int maxID = 0;
            if (dr.Read())
            {
                string strMaxID = dr["maxID"].ToString();
                int.TryParse(strMaxID, out maxID);
            }
            dr.Close();
            // Trả về ID mới
            return (maxID + 1).ToString();
        }
        void load_dgv()
        {
            string sql = "select *from KhachHang";
            dataKH = db.getDataTable(sql);
            dataGridView1.DataSource = dataKH;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            txtMakh.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtHovaten.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtDiachi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtSdt.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtHovaten.Text == "" || txtDiachi.Text == "" || txtSdt.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng muốn xóa");
            }
            else
            {
                DataRow row = dataKH.Rows.Find(txtMakh.Text);
                if (row != null)
                {
                    row.Delete();
                }
                int kq = db.updateDatable(dataKH, "select *from KhachHang");
                if (kq > 0)
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql= "update KhachHang set Hovaten=N'" + txtHovaten.Text + "', Diachi= N'" + txtDiachi.Text + "',SDT='" + txtSdt.Text + "',Email='" + txtEmail.Text + "' where IDKhachHang='" + txtMakh.Text + "' ";
            int kq = db.getNonquery(sql);
            if (kq > 0)
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            load_dgv();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sdt = txtTimkiem.Text.Trim();
            string sql = "SELECT * FROM KhachHang WHERE SDT = '" + sdt + "'";
            DataTable dataKH = db.getDataTable(sql);
            if (dataKH.Rows.Count > 0)
            {
                // Có kết quả tìm kiếm
                dataGridView1.DataSource = dataKH;
                MessageBox.Show("Đã tìm thấy khách hàng có số điện thoại " + sdt);
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng có số điện thoại " + sdt);
            }
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            load_dgv();
            txtTimkiem.Clear();
            txtHovaten.Focus();
        }
    }
}
