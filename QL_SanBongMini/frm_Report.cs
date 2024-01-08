using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QL_SanBongMini
{
    public partial class frm_Report : Form
    {
        public frm_Report()
        {
            InitializeComponent();
        }

        private void frm_Report_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Dbconnect db = new Dbconnect();
            string sql = "select *from thanhtoan";
            db.getdataReader(sql);
            CrystalReportTT rp = new CrystalReportTT();
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txt_nhapngay == null)
            {
                MessageBox.Show("Bạn chưa nhập ngày");
            }
            else
            {
                DataTable dt = new DataTable();
                Dbconnect db = new Dbconnect();
                string sql = "select *from thanhtoan where DAY(ngaythanhtoan)='"+txt_nhapngay.Text+"'";
                db.getdataReader(sql);
                CrystalReportTT rp = new CrystalReportTT();
                rp.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rp;
            } 
        }
    }
}
