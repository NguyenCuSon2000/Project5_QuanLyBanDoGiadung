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
    public class DiaChiBussiness : IDiaChiBussiness
    {
        private IDiaChiRepository _res;
        private string Secret;
        public DiaChiBussiness(IDiaChiRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<TinhThanhPho> GetTinhThanhPho()
        {
            return _res.GetTinhThanhPho();
        }
        public List<QuanHuyen> GetQuanHuyen()
        {
            return _res.GetQuanHuyen();
        }
        public List<QuanHuyen> GetQHbyMaTinh(string matp)
        {
            return _res.GetQHbyMaTinh(matp);
        }
        public List<XaPhuong> GetXaPhuong()
        {
            return _res.GetXaPhuong();
        }
        public List<XaPhuong> GetXPbyMaQH(string maqh)
        {
            return _res.GetXPbyMaQH(maqh);
        }
    }
}
