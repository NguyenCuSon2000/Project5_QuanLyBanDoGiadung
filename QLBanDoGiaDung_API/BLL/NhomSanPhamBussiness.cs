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
    public class NhomSanPhamBussiness : INhomSanPhamBussiness
    {
        private INhomSanPhamRepository _res;
        private string Secret;
        public NhomSanPhamBussiness(INhomSanPhamRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<NhomSanPhamModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<NhomSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            return _res.GetDataAllPaginate(pageIndex, pageSize, out total);
        }
        public NhomSanPhamModel GetDatabyID(string maloaisp)
        {
            return _res.GetDatabyID(maloaisp);
        }
        public bool Create(NhomSanPhamModel model)
        {
            return _res.Create(model);
        }
        public bool Update(NhomSanPhamModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string maloaisp)
        {
            return _res.Delete(maloaisp);
        }
        public List<NhomSanPhamModel> Search(int pageIndex, int pageSize, out long total,  string tenNhom)
        {
            return _res.Search(pageIndex, pageSize, out total, tenNhom);
        }
       
    }
}
