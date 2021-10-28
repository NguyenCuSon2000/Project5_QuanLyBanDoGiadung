using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IThongKeRepository
    {
        List<ThongKeModel> GetSanPhamBanChay();
        string GetSoLuongSanPham(); 
        string GetSoLuongLoaiSP(); 
        string GetSoLuongHoaDon(); 
        string GetSoLuongNguoiDung();
    }
}
