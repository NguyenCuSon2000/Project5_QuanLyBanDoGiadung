using DAL;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using Helper;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace BLL
{
    public class ThongKeBussiness : IThongKeBussiness
    {
        private IThongKeRepository _res;
        private string Secret;
        public ThongKeBussiness(IThongKeRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<ChiTietDonHangModel> GetSanPhamBanChay(int pageIndex, int pageSize,out long total, string tenSanPham)
        {
            return _res.GetSanPhamBanChay(pageIndex, pageSize,out total, tenSanPham);
        }

    public string GetSoLuongSanPham()
    {
      return _res.GetSoLuongSanPham();
    }
    public string GetSoLuongLoaiSP()
    {
      return _res.GetSoLuongLoaiSP();
    }
    public string GetSoLuongNhomSP()
    {
      return _res.GetSoLuongNhomSP();
    }
    public string GetSoLuongHangSP()
    {
      return _res.GetSoLuongHangSP();
    }
    public string GetSoLuongDonHang()
    {
      return _res.GetSoLuongDonHang();
    }
    public string GetSoLuongNguoiDung()
    {
      return _res.GetSoLuongNguoiDung();
    }
    public string GetSoLuongTinTuc()
    {
      return _res.GetSoLuongTinTuc();
    }
  }
}
