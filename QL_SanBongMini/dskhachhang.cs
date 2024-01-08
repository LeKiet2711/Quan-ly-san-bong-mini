using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SanBongMini
{
    class dskhachhang
    {
        List<KhachHang> dskh;
        public dskhachhang()
        {
            dskh = new List<KhachHang>();
        }
        public void AddKH(KhachHang kh)
        {
            dskh.Add(kh);
        }
    }
}
