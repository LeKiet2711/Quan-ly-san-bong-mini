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
    public partial class frm_DangNhap : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=KIETSO2AICUNGSO\SQLEXPRESS;Initial Catalog=QLSB;Integrated Security=True");
        string taikhoan;
        string matkhau;
        public string Taikhoan
        {
            get { return taikhoan; }
            set { taikhoan = value; }
        }
        public string Matkhau
        {
            get { return matkhau; }
            set { matkhau = value; }
        }
        public frm_DangNhap(string taikhoan, string matkhau)
        {
            this.taikhoan = taikhoan;
            this.matkhau = matkhau;
        }

        public frm_DangNhap()
        {
            InitializeComponent();
        }

        private void frm_DangNhap_Load(object sender, EventArgs e)
        {
            con.Open();
            txtDangNhap.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "Select Count(*) from TAIKHOAN where TK=@Tendangnhap and MK=@MK";
            string taikhoan = txtDangNhap.Text;
            this.Taikhoan = taikhoan;
            SqlCommand s = new SqlCommand(sql, con);
            s.Parameters.AddWithValue("@Tendangnhap", txtDangNhap.Text);
            s.Parameters.AddWithValue("@MK", txtMK.Text);
            int check = (int)(s.ExecuteScalar());
            if (check > 0)
            {
                MessageBox.Show("Thành công");
                Mainform frm = new Mainform();
                frm.Taikhoan = this.Taikhoan;
                frm.ShowDialog();
                this.Close();
            }
            else { 
                MessageBox.Show("Không thành công");
                txtDangNhap.Clear();
                txtMK.Clear();
                txtDangNhap.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMK.UseSystemPasswordChar = false;
            }
            else txtMK.UseSystemPasswordChar = true;
        }

        private void txtDangNhap_Enter(object sender, EventArgs e)
        {
            if (txtDangNhap.Text == "Tên Đăng Nhập")
            {
                txtDangNhap.Text = "";
                txtDangNhap.ForeColor = Color.Black;
            }
        }

        private void txtMK_Enter(object sender, EventArgs e)
        {
            if (txtMK.Text == "Mật Khẩu")
            {
                txtMK.Text = "";
                txtMK.ForeColor = Color.Black;
                txtMK.UseSystemPasswordChar = true;
            }
        }

        private void txtMK_Leave(object sender, EventArgs e)
        {
            if (txtMK.Text == "")
            {
                txtMK.Text = "Mật Khẩu";
                txtMK.ForeColor = Color.Red;
                txtMK.UseSystemPasswordChar = false;
                txtMK.Text = "Mật Khẩu";
            }
        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {
            if (txtMK.Text != null)
            {
                txtMK.UseSystemPasswordChar = true;
                txtMK.ForeColor = Color.Black;
            }
        }
    }
}
