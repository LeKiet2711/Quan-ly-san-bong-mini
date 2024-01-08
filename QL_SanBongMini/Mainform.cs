using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_SanBongMini
{
    public partial class Mainform : Form
    {
        public string Taikhoan { get; set; }
        public Mainform()
        {
            InitializeComponent();
        }
        public void LayTaiKhoan()
        {
            //frm_DangNhap frm = new frm_DangNhap();
            lblTen.Text = this.Taikhoan;
        }


        private Form currentChildForm;
        private void OpenChildForm(Form ChildForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panel_body.Controls.Add(ChildForm);
            panel_body.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            LayTaiKhoan();
        }

        private void btn_Khachhang(object sender, EventArgs e)
        {
            OpenChildForm(new frm_KhachHang());
        }

        private void btnDatsan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_DatSan());
        }

        private void tnThanhtoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_ThanhToan());
        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_Baocao());
        }
        private void btnTrangthaisan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_TrangThaiSan());
        }

        private void btnDangxuat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                this.Hide();
                frm_DangNhap f = new frm_DangNhap();
                f.ShowDialog();
            }
        }

    }
}
