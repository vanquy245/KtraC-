using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QlBanxe
{
    public class BanXe
    {
        private int id;
        private string tenKH;
        private string sdt;
        private int idmathang;
        private long giaban;
        

        public string Id { get => id; set => id = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string Ten { get => sdt; set => sdt = value; }
        public string Idmathang { get => idmathang; set => idmathang = value; }
        public string Giaban { get => giaban; set => giaban = value; }

        public Danhba(string id, string tenKH, string sdt, int sdt,long sdt,)
        {
            Id = id;
            TenKH = tenKH;
            Sdt = sdt;
            Idmathang = sdt;
            Giaban = sdt;
        }
        public Danhba(DataRow row)
        {
            Id = row["Id"].ToInt();
            TenKH = row["TenKH"].ToString();
            Sdt = row["Sdt"].ToString();
            Idmathang = row["idmathang"].ToString();
            Giaban = row["Giaban"].ToString();
        }
    }
}
