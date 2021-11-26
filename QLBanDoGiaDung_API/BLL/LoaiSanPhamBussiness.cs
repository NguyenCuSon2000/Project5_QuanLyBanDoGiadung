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
    public class LoaiSanPhamBussiness : ILoaiSanPhamBussiness
    {
        private ILoaiSanPhamRepository _res;
        private string Secret;
        public LoaiSanPhamBussiness(ILoaiSanPhamRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<LoaiSanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<LoaiSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            return _res.GetDataAllPaginate(pageIndex, pageSize, out total);
        }
        public LoaiSanPhamModel GetDatabyID(string maloaisp)
        {
            return _res.GetDatabyID(maloaisp);
        }
        public bool Create(LoaiSanPhamModel model)
        {
            return _res.Create(model);
        }
        public bool Update(LoaiSanPhamModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string maloaisp)
        {
            return _res.Delete(maloaisp);
        }
        public List<LoaiSanPhamModel> Search(int pageIndex, int pageSize, out long total,  string tenLoai)
        {
            return _res.Search(pageIndex, pageSize, out total, tenLoai);
        }
        public List<LoaiSanPhamModel> GetCategoryByGroup(int pageIndex, int pageSize, out long total, string manhom)
        {
            return _res.GetCategoryByGroup(pageIndex, pageSize, out total, manhom);
        }
    }
}
