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
    public partial class frm_DatSan : Form
    {
        DataTable dataDatsan;
        Dbconnect db = new Dbconnect();
        public List<string> idsan { get; set; }
        public frm_DatSan()
        {
            InitializeComponent();
        }
        private void loaddata()
        {
            string sql = "Select KhachHang.IDKhachHang,LichHen.IDLichHen,HoVaTen,SDT,email,SanBong.IDSanBong,LoaiSan,Ngay,BatDau,KetThuc from KhachHang,LichHen,SanBong Where KhachHang.IDKhachHang = LichHen.IDKhachHang AND LichHen.IDSanBong = SanBong.IDSanBong";
            dataDatsan = db.getDataTable(sql);
            dataGridView1.DataSource = dataDatsan;
        }
        private void loadcbbKH()
        {
            string sql = "Select *from KhachHang";
            DataTable kh = db.getDataTable(sql);
            cbb_IDKhachhang.DataSource = kh;
            cbb_IDKhachhang.DisplayMember = "HoVaTen";
            cbb_IDKhachhang.ValueMember = "IDKhachHang";

        }
        private void frm_DatSan_Load(object sender, EventArgs e)
        {
            loaddata();
            loadcbbKH();
            dataGridView1.AutoResizeColumns();

            DataColumn[] key = new DataColumn[1];
            key[0] = dataDatsan.Columns[1];
            dataDatsan.PrimaryKey = key;
            foreach (string item in frm_TrangThaiSan.idsan)
            {
                if (rad1.Text==item)
                {
                    label9.Text +="," + item;
                    rad1.Enabled = false;
                }
                if (rad2.Text == item)
                {
                    label9.Text += "," + item;
                    rad2.Enabled = false;
                }
                if (rad3.Text == item)
                {
                    label9.Text += "," + item;
                    rad3.Enabled = false;
                }
                if (rad4.Text == item)
                {
                    label9.Text += "," + item;
                    rad4.Enabled = false;
                }
                if (rad5.Text == item)
                {
                    label9.Text += "," + item;
                    rad5.Enabled = false;
                }
                if (rad6.Text == item)
                {
                    label9.Text += "," + item;
                    rad6.Enabled = false;
                }
                if (rad7.Text == item)
                {
                    label9.Text += "," + item;
                    rad7.Enabled = false;
                }
                if (rad8.Text == item)
                {
                    label9.Text += "," + item;
                    rad8.Enabled = false;
                }
                if (rad9.Text == item)
                {
                    label9.Text += "," + item;
                    rad9.Enabled = false;
                }

            }

        }

        private int CheckTime()
        {
            MaskedTextBox start = new MaskedTextBox();
            MaskedTextBox end = new MaskedTextBox();
            start = maskedTextBox1;
            end = maskedTextBox2;

            int compareResult = maskedTextBox1.Text.CompareTo(maskedTextBox2.Text);

            return compareResult;

        }
        private int checkdate()
        {
            DateTime now = DateTime.Now;
            int checkngay = dateTimePicker3.Value.CompareTo(now);
            return checkngay;
        }

        private int CheckTrangThaiDatSan(string idsanbong, DateTime ngaydat, string batdau, string ketthuc)
        {
            db.Open();
            string sql = "select * from LichHen where IDSanBong = @idsanbong and Ngay = @ngaydat and BatDau between @batdau and @ketthuc";
            SqlCommand cmd = new SqlCommand(sql, db.conn);     
            cmd.Parameters.AddWithValue("@idsanbong", idsanbong);

            cmd.Parameters.AddWithValue("@ngaydat", ngaydat);
            cmd.Parameters.AddWithValue("@batdau", batdau);
            cmd.Parameters.AddWithValue("@ketthuc", ketthuc);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["IDKhachHang"].ToString() == cbb_IDKhachhang.SelectedValue.ToString())
                {
                    db.Close();
                    return 0; // không trùng
                }
                else
                {
                    db.Close();
                    return 1; // trùng
                }
            }
            else
            {
                db.Close();
                return 0; // không trùng
            }
            
        }


        private void btnDatsan_Click(object sender, EventArgs e)
        {
            string id = "";


            if (rad1.Checked)
            {
                id = "S1";
            }
            else if (rad2.Checked)
            {
                id = "S2";
            }
            else if (rad3.Checked)
            {
                id = "S3";
            }
            else if (rad4.Checked)
            {
                id = "S4";
            }
            else if (rad5.Checked)
            {
                id = "S5";
            }
            else if (rad6.Checked)
            {
                id = "S6";
            }
            else if (rad7.Checked)
            {
                id = "S7";
            }
            else if (rad8.Checked)
            {
                id = "S8";
            }
            else if (rad9.Checked)
            {
                id = "S9";
            }
            


            DateTime NgayDa = dateTimePicker3.Value;
            string ngay = NgayDa.Day.ToString();
            string thang = NgayDa.Month.ToString();
            string nam = NgayDa.Year.ToString();
            string ng = thang + "/" + ngay + "/" + nam;

            //string checkTTS = "select * from LichHen where IDSanBong = '" + id + "' and Ngay = '" + nam+"/"+thang+"/"+ngay + "' and BatDau between '"+maskedTextBox1.Text+"' and '"+maskedTextBox2.Text+"'";
            int check = CheckTrangThaiDatSan(id, dateTimePicker3.Value, maskedTextBox1.Text, maskedTextBox2.Text);

            DataRow newrow = dataDatsan.NewRow();
            string sql = "select *from LichHen";
            int rowCount = dataDatsan.Rows.Count + 1; //Đếm số hàng và cộng thêm 1
            string IDLichHen = "H" + rowCount.ToString().PadLeft(2, '0'); // H010, H011, ...
            newrow["IDLichHen"] = IDLichHen;
            newrow["IDKhachHang"] = cbb_IDKhachhang.SelectedValue.ToString();
            newrow["IDSanBong"] = id;

            if (check > 0)
            {
                MessageBox.Show("Sân bóng này đã được đặt trong thời gian này");
            }
            else if (checkdate() < -1)
            {
                MessageBox.Show("Vui lòng chọn lại ngày lớn hơn hoặc bằng ngày hiện tại");
            }
            else if (CheckTime() > 0)
            {
                MessageBox.Show("Gio bắt đầu phải nhỏ hơn giờ kết thúc");
            }else if (rad1.Checked == false && rad1.Checked == false && rad2.Checked == false && rad3.Checked == false && rad4.Checked == false && rad5.Checked == false && rad6.Checked == false && rad7.Checked == false && rad8.Checked == false && rad9.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn sân");
            }else if (maskedTextBox1.Text.Substring(0, 1) == " " || maskedTextBox2.Text.Substring(0, 1) == " ")
            {
                MessageBox.Show("Vui lòng không để trống thời gian");
            }
            else
            {
                
                newrow["Ngay"] = ng;
                //12:00:00.000
                string a = maskedTextBox1.Text.Substring(0, 2);
                string b = maskedTextBox2.Text.Substring(0, 2);
                int sogioda = Int32.Parse(b) - Int32.Parse(a);
                if (sogioda < 1)
                {
                    MessageBox.Show("số giờ đá phải tối thiểu là 1h");
                }
                else
                {
                    newrow["BatDau"] = maskedTextBox1.Text + ":00.000";
                    newrow["KetThuc"] = maskedTextBox2.Text + ":00.000";
                    dataDatsan.Rows.Add(newrow);
                }
                int kq = db.updateDatable(dataDatsan, sql);
                if (kq > 0)
                {
                    MessageBox.Show("Thêm thành công!");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
                
            loaddata();
        }

        private void cbb_Loaisan_SelectedIndexChanged(object sender, EventArgs e)
        {
            rad3.Checked = false;
            rad6.Checked = false;
            rad7.Checked = false;
            rad8.Checked = false;
            rad9.Checked = false;
            rad1.Checked = false;
            rad2.Checked = false;
            rad4.Checked = false;
            rad5.Checked = false;

            if (cbb_Loaisan.SelectedItem.ToString() == "5")
            {
                rad3.Enabled = false;
                rad4.Enabled = false;
                rad6.Enabled = false;
                rad7.Enabled = false;
                rad8.Enabled = false;
                rad9.Enabled = false;

                rad1.Enabled = true;
                rad2.Enabled = true;
                rad5.Enabled = true;
            }
            else if (cbb_Loaisan.SelectedItem.ToString() == "7")
            {
                rad1.Enabled = false;
                rad2.Enabled = false;
                rad4.Enabled = false;
                rad5.Enabled = false;
                rad9.Enabled = false;

                rad3.Enabled = true;
                rad6.Enabled = true;
                rad7.Enabled = true;
                rad8.Enabled = true;
            }
            else if (cbb_Loaisan.SelectedItem.ToString() == "11")
            {
                rad1.Enabled = false;
                rad2.Enabled = false;
                rad3.Enabled = false;
                rad5.Enabled = false;
                rad6.Enabled = false;
                rad7.Enabled = false;
                rad8.Enabled = false;

                rad4.Enabled = true;
                rad9.Enabled = true;
            }

            foreach (string item in frm_TrangThaiSan.idsan)
            {
                if (rad1.Text == item)
                {
                    rad1.Enabled = false;
                }
                if (rad2.Text == item)
                {
                    rad2.Enabled = false;
                }
                if (rad3.Text == item)
                {
                    rad3.Enabled = false;
                }
                if (rad4.Text == item)
                {
                    label9.Text += "," + item;
                    rad4.Enabled = false;
                }
                if (rad5.Text == item)
                {
                    rad5.Enabled = false;
                }
                if (rad6.Text == item)
                {
                    rad6.Enabled = false;
                }
                if (rad7.Text == item)
                {
                    rad7.Enabled = false;
                }
                if (rad8.Text == item)
                {
                    rad8.Enabled = false;
                }
                if (rad9.Text == item)
                {
                    rad9.Enabled = false;
                }

            }


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtLichhen.Text=="")
            {
                MessageBox.Show("Vui lòng chọn lịch muốn xóa");
            }
            else
            {
                DataRow row = dataDatsan.Rows.Find(txtLichhen.Text);
                if (row != null)
                {
                    row.Delete();
                }
                int kq = db.updateDatable(dataDatsan, "select *from LichHen");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            cbb_IDKhachhang.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            cbb_Loaisan.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            DateTime date = Convert.ToDateTime(dataGridView1.Rows[i].Cells[7].Value.ToString());
            dateTimePicker3.Value = date;
            maskedTextBox1.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
            maskedTextBox2.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();
            txtLichhen.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

        }
    }
}
