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
    public partial class frm_TrangThaiSan : Form
    {
        public frm_TrangThaiSan()
        {
            InitializeComponent();
        }
        private List<string> _sandangdong = new List<string>();
        public static List<string> idsan = new List<string>();
        Dbconnect db = new Dbconnect();
        public List<string> IDsan
        {
            get { return idsan; }
            set { idsan = value; }
        }

        private void frm_TrangThaiSan_Load(object sender, EventArgs e)
        {
            TrangThaiSan();

        }

        private void TrangThaiSan()
        {
            string id = "";
            string s = "";
            string check="";
            if (San1.ForeColor==Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S1'";
                check = db.getScalar(s).ToString().Substring(0,5);
                if (check == "OPEN ")
                {
                    San1.ForeColor= Color.Black;
                }
                else
                {
                    San1.ForeColor = Color.Red;
                }
            }
            if (San2.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S2'";
                string check2 = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San2.ForeColor = Color.Black;
                }
                else
                {
                    San2.ForeColor = Color.Red;
                }
            }
            if (San3.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S3'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San3.ForeColor = Color.Black;
                }
                else
                {
                    San3.ForeColor = Color.Red;
                }
            }
            if (San4.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S4'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San4.ForeColor = Color.Black;
                }
                else
                {
                    San4.ForeColor = Color.Red;
                }
            }
            if (San5.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S5'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San5.ForeColor = Color.Black;
                }
                else
                {
                    San5.ForeColor = Color.Red;
                }
            }
            if (San6.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S6'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San6.ForeColor = Color.Black;
                }
                else
                {
                    San6.ForeColor = Color.Red;
                }
            }
            if (San7.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S7'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San7.ForeColor = Color.Black;
                }
                else
                {
                    San7.ForeColor = Color.Red;
                }
            }
            if (San8.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S8'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San8.ForeColor = Color.Black;
                }
                else
                {
                    San8.ForeColor = Color.Red;
                }
            }
            if (San9.ForeColor == Color.Black)
            {
                s = "select TrangThai from SanBong Where IDSanBong = 'S9'";
                check = db.getScalar(s).ToString().Substring(0, 5);
                if (check == "OPEN ")
                {
                    San9.ForeColor = Color.Black;
                }
                else
                {
                    San9.ForeColor = Color.Red;
                }
            }

        }

        private void btn_dong_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn đóng sân ?  ", "Thông Báo", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                // Lấy danh sách tất cả các radiobutton
                List<RadioButton> radioButtons = this.Controls.OfType<RadioButton>().ToList();

                // Duyệt qua danh sách radiobutton
                foreach (RadioButton radioButton in radioButtons)
                {
                    if (radioButton.Checked)
                    {
                        string san = radioButton.Name.Substring(3);

                        // Nếu sân chưa có trong danh sách các sân đang đóng
                        if (!_sandangdong.Contains(san))
                        {

                            _sandangdong.Add(san);


                            radioButton.ForeColor = Color.Red;


                            Label labelSânĐangĐóng = this.Controls.Find("label5", true).FirstOrDefault() as Label;
                            labelSânĐangĐóng.Text = "Các sân đang đóng: " + string.Join(", ", _sandangdong);
                        }
                    }
                }
            }
            string id = "";
            if (San1.Checked)
            {
                id = "S1";
            }
            else if (San2.Checked)
            {
                id = "S2";
            }
            else if (San3.Checked)
            {
                id = "S3";
            }
            else if (San4.Checked)
            {
                id = "S4";
            }
            else if (San5.Checked)
            {
                id = "S5";
            }
            else if (San6.Checked)
            {
                id = "S6";
            }
            else if (San7.Checked)
            {
                id = "S7";
            }
            else if (San8.Checked)
            {
                id = "S8";
            }
            else if (San9.Checked)
            {
                id = "S9";
            }

            string s = "update SanBong SET TrangThai='CLOSE' WHERE IDSanBong='"+id+"'";
            int kq = db.getNonquery(s);
            if (kq > 0)
            {
                MessageBox.Show("Đúng");
            }
            else
            {
                MessageBox.Show("Sai");
            }


        }

        private void btn_mo_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn mở sân?  ", "Thông Báo", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                // Lấy danh sách tất cả các radiobutton
                List<RadioButton> radioButtons = this.Controls.OfType<RadioButton>().ToList();


                foreach (RadioButton radioButton in radioButtons)
                {
                    // Nếu radiobutton được chọn
                    if (radioButton.Checked)
                    {

                        string san = radioButton.Name.Substring(3);

                        // Nếu sân có trong danh sách các sân đang đóng
                        if (_sandangdong.Contains(san))
                        {

                            _sandangdong.Remove(san);

                            radioButton.ForeColor = Color.Black;

                            // Cập nhật văn bản của label
                            Label labelSânĐangĐóng = this.Controls.Find("label5", true).FirstOrDefault() as Label;
                            labelSânĐangĐóng.Text = (_sandangdong.Count > 0) ? "Các sân đang đóng: " + string.Join(", ", _sandangdong) : "Không có sân nào đang đóng";
                        }
                    }
                }
            }
            string id = "";
            if (San1.Checked)
            {
                id = "S1";
            }
            else if (San2.Checked)
            {
                id = "S2";
            }
            else if (San3.Checked)
            {
                id = "S3";
            }
            else if (San4.Checked)
            {
                id = "S4";
            }
            else if (San5.Checked)
            {
                id = "S5";
            }
            else if (San6.Checked)
            {
                id = "S6";
            }
            else if (San7.Checked)
            {
                id = "S7";
            }
            else if (San8.Checked)
            {
                id = "S8";
            }
            else if (San9.Checked)
            {
                id = "S9";
            }

            string s = "update SanBong SET TrangThai='OPEN' WHERE IDSanBong='" + id + "'";
            int kq = db.getNonquery(s);
            if (kq > 0)
            {
                MessageBox.Show("Đúng");
            }
            else
            {
                MessageBox.Show("Sai");
            }


        }

        private void frm_TrangThaiSan_Leave(object sender, EventArgs e)
        {
            frm_DatSan frm = new frm_DatSan();
            if (San1.ForeColor==Color.Red)
            {
                idsan.Add("Sân 1");
            }
            if (San2.ForeColor == Color.Red)
            {
                idsan.Add("Sân 2");
            }
            if (San3.ForeColor == Color.Red)
            {
                idsan.Add("Sân 3");
            }
            if (San4.ForeColor == Color.Red)
            {
                idsan.Add("Sân 4");
            }
            if (San5.ForeColor == Color.Red)
            {
                idsan.Add("Sân 5");
            }
            if (San6.ForeColor == Color.Red)
            {
                idsan.Add("Sân 6");
            }
            if (San7.ForeColor == Color.Red)
            {
                idsan.Add("Sân 7");
            }
            if (San8.ForeColor == Color.Red)
            {
                idsan.Add("Sân 8");
            }
            if (San9.ForeColor == Color.Red)
            {
                idsan.Add("Sân 9");
            }
        }
    }
}
