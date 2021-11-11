using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DonHangModel
    {
        public string MaDonHang { get; set; }
        public string HoTen { get; set; }
        public string DiaChiNhan { get; set; }
        public string SDTNhan { get; set; }
        public string Email { get; set; }
        public string TinhTrang { get; set; }
        public float TongTien { get; set; }
        public string NgayDat { get; set; }
        public string NgayGiao { get; set; }
        public List<ChiTietDonHangModel> listjson_chitiet { get; set; }
    }
}
