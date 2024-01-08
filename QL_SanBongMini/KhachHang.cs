using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_SanBongMini
{
    class KhachHang
    {
        public string loaisan;
        public string Loaisan
        {
            get { return loaisan; }
            set { loaisan = value; }
        }
        public string HoVaTen;
        public string ID;
        public string SĐT;
        public int slsuoi;
        public int slngot;
        public int slao;

        List<string> dichvu;
        public int batdau;

        public int Slsuoi
        {
            get { return slsuoi; }
            set { slsuoi = value; }
        }
        public int Slngot
        {
            get { return slngot; }
            set { slngot = value; }
        }
        public int Slao
        {
            get { return slao; }
            set { slao = value; }
        }

        public int Batdau
        {
            get { return batdau; }
            set { batdau = value; }
        }
        public int ketthuc;
        public int Ketthuc
        {
            get { return ketthuc; }
            set { ketthuc = value; }
        }
        public string hoten
        {
            get { return HoVaTen; }
            set { HoVaTen = value; }
        }
        public string id
        {
            get { return ID; }
            set { ID = value; }
        }
        public string sdt
        {
            get { return SĐT; }
            set { SĐT = value; }
        }
        public List<string> DV
        {
            get { return dichvu; }
            set { dichvu = value; }
        }
        public KhachHang()
        {
            HoVaTen = string.Empty;
            ID = string.Empty;
            SĐT = string.Empty;
            dichvu = new List<string>();
        }
        public KhachHang(string HoVaTen, string ID, string SĐT, List<string> dichvu)
        {
            this.HoVaTen = HoVaTen;
            this.ID = ID;
            this.SĐT = SĐT;
            this.dichvu = dichvu;
        }
        public int tiengio()
        {
            int s = 0;
            if (loaisan == "5")
            {
                s = s + 250000 * (Ketthuc - Batdau);
            }
            else
            {
                if (loaisan == "7")
                {
                    s = s + 500000 * (Ketthuc - Batdau);
                }
                else
                {
                    s = s + 1000000 * (Ketthuc - Batdau);
                }
            }
            return s;
        }
        public int tonggio()
        {
            int s = 0;
            s += Ketthuc - Batdau;
            return s;
        }
        public int tiendv()
        {
            int s = 0;
            foreach (string item in dichvu)
            {
                if (item == "Nước Suối (lốc)")
                {
                    s += 60000*slsuoi;
                }
                if (item == "Nước Ngọt (lốc)")
                {
                    s += 90000*slngot;
                }
                if (item == "Áo Pic (cái)")
                {
                    s += 50000*slao;
                }
            }
            return s;
        }
        public int tongtienthanhtoan()
        {
            int s = 0;
            s = tiengio() + tiendv();
            return s;
        }
    }
}
