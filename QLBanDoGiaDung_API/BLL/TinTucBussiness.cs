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
    public class TinTucBussiness : ITinTucBussiness
    {
        private ITinTucRepository _res;
        private string Secret;
        public TinTucBussiness(ITinTucRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
     
     
        public TinTucModel GetDatabyID(string matintuc)
        {
            return _res.GetDatabyID(matintuc);
        }

        public bool Create(TinTucModel model)
        {
            return _res.Create(model);
        }
        public bool Update(TinTucModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(string matintuc)
        {
            return _res.Delete(matintuc);
        }
        public List<TinTucModel> Search(int pageIndex, int pageSize, out long total, string tieuDe)
        {
            return _res.Search(pageIndex, pageSize, out total, tieuDe);
        }
   
    }
}
