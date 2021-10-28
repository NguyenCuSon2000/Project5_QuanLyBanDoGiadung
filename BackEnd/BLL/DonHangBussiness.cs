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
    public class DonHangBussiness : IDonHangBussiness
    {
        private IDonHangRepository _res;
        private string Secret;
        public DonHangBussiness(IDonHangRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<DonHangModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<DonHangModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            return _res.GetDataAllPaginate(pageIndex, pageSize, out total);
        }
        public bool Create(DonHangModel model)
        {
            return _res.Create(model);
        }

        public bool Update(DonHangModel model)
        {
            return _res.Update(model);
        }

        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public DonHangModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<DonHangModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi, string email)
        {
            return _res.Search(pageIndex, pageSize, out total, hoten, diachi, email);
        }

    }
}
