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
    public class NguoiDungBussiness : INguoiDungBussiness
    {
        private INguoiDungRepository _res;
        private string Secret;
        public NguoiDungBussiness(INguoiDungRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<NguoiDungModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total)
        {
            return _res.GetDataAllPaginate(pageIndex, pageSize, out total);
        }
        public bool Delete(string user_id)
        {
            return _res.Delete(user_id);
        }
        public NguoiDungModel Authenticate(string username, string password)
        {
            var user = _res.GetUser(username, password);
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.HoTen.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            return user;

        }

        public NguoiDungModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(NguoiDungModel model)
        {
            return _res.Create(model);
        }
        public bool Update(NguoiDungModel model)
        {
            return _res.Update(model);
        }
        public List<NguoiDungModel> Search(int pageIndex, int pageSize, out long total, string hoTen, string taiKhoan)
        {
            return _res.Search(pageIndex, pageSize, out total, hoTen, taiKhoan);
        }
    }
}
