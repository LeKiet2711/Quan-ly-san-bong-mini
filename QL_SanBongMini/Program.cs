using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_SanBongMini
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_DangNhap());
            //Application.Run(new frm_ThanhToan());
            //Application.Run(new frm_DatSan());
            //Application.Run(new frm_KhachHang());
            //Application.Run(new Mainform());
        }
    }
}
