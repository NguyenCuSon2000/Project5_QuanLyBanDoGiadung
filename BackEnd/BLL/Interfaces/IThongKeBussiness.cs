using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IThongKeBussiness
    {
        List<ThongKeModel> GetSanPhamBanChay();
        string GetSoLuongSanPham();
        string GetSoLuongLoaiSP();
        string GetSoLuongHoaDon();
        string GetSoLuongNguoiDung();
    }

}
