using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IThongKeBussiness
    {
        List<ChiTietDonHangModel> GetSanPhamBanChay(int pageIndex, int pageSize, out long total, string tenSanPham);
        //string GetSoLuongSanPham();
        //string GetSoLuongLoaiSP();
        //string GetSoLuongHoaDon();
        //string GetSoLuongNguoiDung();
    }

}
