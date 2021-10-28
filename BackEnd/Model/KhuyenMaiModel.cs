using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class KhuyenMaiModel
    {
        public string MaKH { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string HinhThuc { get; set; }
        public int GiaTriKH { get; set; }
        public string DonViTinh { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
    }
}
