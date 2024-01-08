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
using static QL_SanBongMini.frm_ThanhToan;

namespace QL_SanBongMini
{
    public partial class frm_ThanhToan : Form
    {
        dskhachhang dskh = new dskhachhang();
        KhachHang kh = new KhachHang();
        Dbconnect db = new Dbconnect();
        public frm_ThanhToan()
        {
            InitializeComponent();
        }
        public void loadCombobox()
        {
            string sql = "select *from khachhang";
            DataTable dt = new DataTable();
            dt = db.getDataTable(sql);
            cbb_id.DataSource = dt;
            cbb_id.DisplayMember = "idkhachhang";
            cbb_id.ValueMember = "idkhachhang";
        }
        private string GetUniqueID(string table)
        {
            string sql = "select max(IDThanhToan) as maxID from " + table;
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

        private string GetUniqueIDCP(string table)
        {
            string sql = "select max(IDChiPhiKhac) as maxID from " + table;
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

        public void loadDataGridView1()
        {
            string sql = "select khachhang.idkhachhang,khachhang.hovaten,batdau,ketthuc, sanbong.idsanbong, loaisan,LichHen.IDLichHen from khachhang,sanbong,lichhen where khachhang.idkhachhang=lichhen.idkhachhang and lichhen.idsanbong= sanbong.idsanbong and khachhang.idkhachhang='"+cbb_id.SelectedValue.ToString()+"'";
            DataTable dt = new DataTable();
            dt = db.getDataTable(sql);
            dataGridView1.DataSource = dt;
        }

        private void frm_ThanhToan_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
            loadCombobox();
            loadDataGridView1();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txt_loaisan.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            kh.Loaisan = txt_loaisan.Text;

            txt_giovao.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            string vao = txt_giovao.Text.Substring(0, 2);
            kh.Batdau = Int32.Parse(vao);

            txt_giora.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            string ra = txt_giora.Text.Substring(0, 2);
            kh.Ketthuc = Int32.Parse(ra);
            txt_tongtien.Text = kh.tiengio().ToString();
            txt_tonggio.Text = kh.tonggio().ToString();

            txtLichHen.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
        }
        public delegate void GETDATA(string data);

        private void btn_thanhtoan_Click(object sender, EventArgs e)
        {
           

            
            string loaidv = "";
            string s2 = "";
            string chiphi = "";
            int dongia;
            if (cb_phuongthuc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán");
            }else if (txtLichHen.Text==string.Empty)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn muốn thanh toán");
            }else if (txt_tongtien.Text==string.Empty || txt_dv.Text==string.Empty)
            {
                MessageBox.Show("Vui lòng chọn tổng tiền sau đó thanh toán");
            }
            else
            {
                if (kt_KhoaChinh(txtLichHen.Text))
                {
                    frm_bill frm = new frm_bill();
                    frm.gio_vao = txt_giovao.Text;
                    frm.gio_ra = txt_giora.Text;
                    frm.id = cbb_id.Text;
                    frm.tiengio = txt_tongtien.Text;
                    frm.phuongthuc = cb_phuongthuc.Text;
                    frm.tong = txt_dv.Text;
                    frm.tiendv = txt_Dichvu.Text;
                    string maThanhToan = GetUniqueID("ThanhToan");
                    int Tongtien = Convert.ToInt32(txt_dv.Text);
                    int Tiendichvu = Convert.ToInt32(txt_Dichvu.Text);

                    ///Tạo ba check, ba chuỗi s2 để thêm từng dịch vụ vào bảng ChiPhiKhac (Không thêm 1 lần tất cả các dịch vụ vào)
                    if (ck_suoi.Checked)
                    {
                        //Cấp phát mã IDChiPhiKhac liên tục mỗi lần thêm tránh trường hợp tạo 1 ID nhưng insert nhiều sẽ văng source
                        chiphi = GetUniqueIDCP("ChiPhiKhac");
                        loaidv = "Nước";
                        dongia = kh.slsuoi * 60000;
                        s2 = "insert into ChiPhiKhac values('" + chiphi + "','" + txtLichHen.Text + "',N'" + loaidv + "','" + kh.slsuoi + "'," + 60000 + "," + dongia + ")";
                        int k = db.getNonquery(s2);
                       /* if (k > 0)
                        {
                            MessageBox.Show("OH YEAH");
                        }
                        else MessageBox.Show("TOANG");*/
                    }
                    if (ck_ngot.Checked)
                    {
                        chiphi = GetUniqueIDCP("ChiPhiKhac");
                        loaidv = "Nước ngọt";
                        dongia = kh.slngot * 60000;
                        s2 = "insert into ChiPhiKhac values('" + chiphi + "','" + txtLichHen.Text + "',N'" + loaidv + "','" + kh.slngot + "'," + 90000 + "," + dongia + ")";
                        int k = db.getNonquery(s2);
                    }
                    if (ck_ao.Checked)
                    {
                        chiphi = GetUniqueIDCP("ChiPhiKhac");
                        loaidv = "Áo";
                        dongia = kh.slao * 60000;
                        s2 = "insert into ChiPhiKhac values('" + chiphi + "','" + txtLichHen.Text + "',N'" + loaidv + "','" + kh.slao + "'," + 50000 + "," + dongia + ")";
                        int k = db.getNonquery(s2);
                    }

                    //////////////////

                    string ngay = DateTime.Now.Day.ToString();
                    string thang = DateTime.Now.Month.ToString();
                    string nam = DateTime.Now.Year.ToString();

                    string s1 = "select Ngay from LichHen,ThanhToan where LichHen.IDLichHen = ThanhToan.IDLichHen and ThanhToan.IDLichHen = '" + txtLichHen.Text + "'";
                    DateTime ktran = Convert.ToDateTime(db.getScalar(s1));

                    DateTime ngayktra = DateTime.Parse(ktran.ToString("yyyy-MM-dd"));

                    int namm = ngayktra.Year;
                    int thangg = ngayktra.Month;
                    int ngayy = ngayktra.Day;

                    
                    string ngaythangnam = string.Format("{0}-{1}-{2}", nam, thang, ngay);



                    string ngayThanhToan = thang + "/" + ngay + "/" + nam;
                    string s = "insert into ThanhToan values('" + maThanhToan + "','" + txtLichHen.Text + "',N'" + cb_phuongthuc.SelectedItem.ToString() + "','" + ngayThanhToan + "','" + Tongtien + "','" + Tiendichvu + "')";
                    int kq = db.getNonquery(s);
                    if (kq > 0)
                    {
                        MessageBox.Show("Thanh toán thành công");
                    }
                    else MessageBox.Show("Thanh toán không thành công");
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Thanh toán không thành công (Lịch hẹn đã thanh toán)");
                }
            }
        }

        private void btn_dv_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (txtLichHen.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn muốn thanh toán");
            }
            else
            {

                if (ck_suoi.Checked)
                {
                    if (numericUpDown1.Value == 0)
                    {
                        MessageBox.Show("Vui lòng chọn số lượng lốc nước suối");
                        check++;
                        //tạo check để dễ dàng tính tiền dịch vụ và tổng tiền
                    }
                    else
                    {
                        kh.DV.Add(ck_suoi.Text);
                        kh.slsuoi = Convert.ToInt32(numericUpDown1.Value);
                    }
                }

                if (ck_ngot.Checked)
                {
                    if (numericUpDown2.Value == 0)
                    {
                        MessageBox.Show("Vui lòng chọn số lượng lốc nước ngọt");
                        check++;
                    }
                    else
                    {
                        kh.DV.Add(ck_ngot.Text);
                        kh.slngot = Convert.ToInt32(numericUpDown2.Value);
                    }
                }
                if (ck_ao.Checked)
                {
                    if (numericUpDown3.Value == 0)
                    {
                        MessageBox.Show("Vui lòng chọn số lượng áo");
                        check++;
                    }
                    else
                    {
                        kh.DV.Add(ck_ao.Text);
                        kh.slao = Convert.ToInt32(numericUpDown3.Value);
                    }
                }

                if (check == 0)
                {
                    dskh.AddKH(kh);
                    txt_Dichvu.Text = kh.tiendv().ToString();
                    txt_dv.Text = kh.tongtienthanhtoan().ToString();
                    btn_dv.Enabled = false;
                    kh.DV.Clear();
                    //Clear ds dịch vụ tránh trường hợp thanh toán nhiều hóa đơn trong 1 phiên đăng nhập (khi thanh toán sẽ lấy tất cả dịch vụ trong list ra thanh toán)
                }
            }
        }

        private void txt_dv_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbb_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDataGridView1();
            txt_loaisan.Clear();
            txt_giovao.Clear();
            txt_giora.Clear();
            txt_Dichvu.Clear();
            txt_tongtien.Clear();
            txt_tonggio.Clear();
            ck_ao.Checked = false;
            ck_ngot.Checked = false;
            ck_suoi.Checked = false;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            
        }
        public bool kt_KhoaChinh(string str)
        {
            string sql = "select count(*) from THANHTOAN where IDLichHen ='" + str + "'";
            int kq = (int)db.getScalar(sql);
            if (kq >= 1)
                return false;// có rồi
            else
                return true;// chưa có
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = numericUpDown1.Value.ToString();
            MessageBox.Show(a);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txt_dv.Clear();
            txt_giovao.Clear();
            txt_tongtien.Clear();
            txt_loaisan.Clear();
            txt_giora.Clear();
            txtLichHen.Clear();
            txt_Dichvu.Clear();
            txt_tonggio.Clear();
            ck_suoi.Checked = false;
            ck_ngot.Checked = false;
            ck_ao.Checked = false;
            btn_dv.Enabled = true;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (ck_suoi.Checked==false)
            {
                MessageBox.Show("Vui lòng chọn sau đó chọn số lượng");
                numericUpDown1.Value = 0;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (ck_ngot.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn sau đó chọn số lượng");
                numericUpDown2.Value = 0;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (ck_ao.Checked == false)

            {
                MessageBox.Show("Vui lòng chọn sau đó chọn số lượng");
                numericUpDown3.Value = 0;
            }
        }
    }
}
