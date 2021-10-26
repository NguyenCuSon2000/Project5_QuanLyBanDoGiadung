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
    public class HangSanPhamBussiness : IHangSanPhamBussiness
    {
        private IHangSanPhamRepository _res;
        private string Secret;
        public HangSanPhamBussiness(IHangSanPhamRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<HangSanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<HangSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            return _res.GetDataAllPaginate(pageIndex, pageSize, out total);
        }
        public HangSanPhamModel GetDatabyID(string maloaisp)
        {
            return _res.GetDatabyID(maloaisp);
        }
        public bool Create(HangSanPhamModel model)
        {
            return _res.Create(model);
        }
        public bool Update(HangSanPhamModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string maloaisp)
        {
            return _res.Delete(maloaisp);
        }
        public List<HangSanPhamModel> Search(int pageIndex, int pageSize, out long total, string tenhang)
        {
            return _res.Search(pageIndex, pageSize, out total, tenhang);
        }
       
    }
}
