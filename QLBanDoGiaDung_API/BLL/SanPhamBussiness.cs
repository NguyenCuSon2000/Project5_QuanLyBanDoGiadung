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
    public class SanPhamBussiness : ISanPhamBussiness
    {
        private ISanPhamRepository _res;
        private string Secret;
        public SanPhamBussiness(ISanPhamRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<SanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<SanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            return _res.GetDataAllPaginate(pageIndex, pageSize, out total);
        }
        public SanPhamModel GetDatabyID(string masp)
        {
            return _res.GetDatabyID(masp);
        }

        public List<SanPhamModel> GetProductNew()
        {
            return _res.GetProductNew();
        }

        public bool Create(SanPhamModel model)
        {
            return _res.Create(model);
        }
        public bool Update(SanPhamModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string masp)
        {
            return _res.Delete(masp);
        }
        public List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string tenSanPham)
        {
            return _res.Search(pageIndex, pageSize, out total, tenSanPham);
        }
        public List<SanPhamModel> GetProductByCategory(int pageIndex, int pageSize, out long total, string maloai)
        {
            return _res.GetProductByCategory(pageIndex, pageSize, out total, maloai);
        }
        public List<SanPhamModel> GetProductByBrand(int pageIndex, int pageSize, out long total, string mahang, string tenhang)
        {
            return _res.GetProductByBrand(pageIndex, pageSize, out total, mahang, tenhang);
        }
    }
}
