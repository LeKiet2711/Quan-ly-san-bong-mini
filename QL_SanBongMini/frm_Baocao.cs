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
using System.Data;

namespace QL_SanBongMini
{
    public partial class frm_Baocao : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-7VVFGOUG\SQLEXPRESS;Initial Catalog=QLSB;Integrated Security=True");
        dskhachhang ds = new dskhachhang();
        DataTable dataBaocao;
        Dbconnect db = new Dbconnect();
        public frm_Baocao()
        {
            InitializeComponent();
        }

        private void frm_Baocao_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_TinhThang_Click(object sender, EventArgs e)
        {
            if (cbb_Thang.SelectedIndex == -1 || cbb_Namtheothang.SelectedIndex== -1)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ tháng và năm.");
            }
            else
            {
                load_dgv();
                int cbb1 = int.Parse(cbb_Thang.SelectedItem.ToString());
                int cbb2 = int.Parse(cbb_Namtheothang.SelectedItem.ToString());

                label5.Text = cbb_Thang.SelectedItem.ToString() + "/" + cbb_Namtheothang.SelectedItem.ToString();
                string s1 = "select COALESCE(sum(TongTien), 0) from ThanhToan Where month(NgayThanhToan)= " + cbb1 + " and year(NgayThanhToan)= " + cbb2+"";
                string s2 = "select COALESCE(sum(TongChiPhiKhac), 0) from ThanhToan Where month(NgayThanhToan)= " + cbb1 + " and year(NgayThanhToan)= " + cbb2 + "";
                //sử dụng COALESCE để chuyển NULL về 0, tránh trường hợp trả về NULL int kq sẽ sai (văng source) int!=NULL

                int kq = (int)db.getScalar(s1);
                int kq2 = (int)db.getScalar(s2);
                if (kq > 0)
                {
                    MessageBox.Show("Thành công");
                    txtTongtien.Text = kq.ToString();
                    txtTiendichvu.Text = kq2.ToString();
                }
                else
                {
                    MessageBox.Show("Tháng không có trận đấu nào");
                    txtTongtien.Clear();
                    txtTiendichvu.Clear();
                }
            }
            
        }

        void load_dgv()
        {
            string thangcb =cbb_Thang.SelectedItem.ToString();
            string namcb =cbb_Namtheothang.SelectedItem.ToString();
            string sql = "SELECT ThanhToan.IDLichHen,HovaTen,SDT,NgayThanhToan,TongChiPhiKhac,TongTien FROM LichHen, ThanhToan, KhachHang WHERE LichHen.IDLichHen = ThanhToan.IDLichHen AND LichHen.IDKhachHang = KhachHang.IDKhachHang AND MONTH(NgayThanhToan)= '"+thangcb+"' AND YEAR(NgayThanhToan)='"+namcb+"'";
            dataBaocao = db.getDataTable(sql);
            dataGridView1.DataSource = dataBaocao;
        }
        void load_dgv2()
        {
            string namcb = cbb_Namtheonam.SelectedItem.ToString();
            string sql = "SELECT ThanhToan.IDLichHen,HovaTen,SDT,NgayThanhToan,TongChiPhiKhac,TongTien FROM LichHen, ThanhToan, KhachHang WHERE LichHen.IDLichHen = ThanhToan.IDLichHen AND LichHen.IDKhachHang = KhachHang.IDKhachHang AND YEAR(NgayThanhToan)='" + namcb + "'";
            dataBaocao = db.getDataTable(sql);
            dataGridView1.DataSource = dataBaocao;
        }

        private void btn_TinhNam_Click(object sender, EventArgs e)
        {
            if (cbb_Namtheonam.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn năm.");
            }
            else
            {
                load_dgv2();
                label5.Text = "Năm " + cbb_Namtheonam.SelectedItem.ToString();
                int cbb3 = int.Parse(cbb_Namtheonam.SelectedItem.ToString());

                string s1 = "select COALESCE(sum(TongTien), 0) from ThanhToan Where year(NgayThanhToan)= " + cbb3 + "";
                string s2 = "select COALESCE(sum(TongChiPhiKhac), 0) from ThanhToan Where year(NgayThanhToan)= " + cbb3 + "";
                //sử dụng COALESCE để chuyển NULL về 0, tránh trường hợp trả về NULL int kq sẽ sai (văng source) int!=NULL

                int kq = (int)db.getScalar(s1);
                int kq2 = (int)db.getScalar(s2);
                if (kq > 0)
                {
                    MessageBox.Show("Thành công");
                    txtTongtien.Text = kq.ToString();
                    txtTiendichvu.Text = kq2.ToString();
                }
                else
                {
                    MessageBox.Show("Tháng không có trận đấu nào");
                    txtTongtien.Clear();
                    txtTiendichvu.Clear();
                }
            }
            
        }
    }
}
