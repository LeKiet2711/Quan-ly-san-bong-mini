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
    public partial class frm_bill : Form
    {
        public frm_bill()
        {
            InitializeComponent();
        }
        public string gio_vao;
        public string gio_ra;
        public string tong;
        public string id;
        public string phuongthuc;
        public string tiengio;
        public string tiendv;

        private void frm_bill_Load(object sender, EventArgs e)
        {
            txt_giovao.Text = gio_vao;
            txt_giora.Text = gio_ra;
            txt_id.Text = id;
            txt_tong.Text = tong;
            txt_phuongthuc.Text = phuongthuc;
            txt_tiengio.Text = tiengio;
            txt_dichvu.Text = tiendv;
        }

        private void btn_in_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanh Toán Thành Công!");
            this.Close();
        }
    }
}
