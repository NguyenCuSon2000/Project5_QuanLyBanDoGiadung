using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IThongKeRepository
    {
        List<ChiTietDonHangModel> GetSanPhamBanChay(int pageIndex, int pageSize, out long total, string tenSanPham);
        //string GetSoLuongSanPham(); 
        //string GetSoLuongLoaiSP(); 
        //string GetSoLuongHoaDon(); 
        //string GetSoLuongNguoiDung();
    }
}
