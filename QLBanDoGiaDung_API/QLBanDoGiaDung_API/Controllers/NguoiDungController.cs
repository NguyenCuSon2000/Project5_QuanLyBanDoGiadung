using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLBanDoGiaDung_API.Controllers
{
    [Authorize]
    
    [ApiController]
  [Route("api/[controller]")]
  public class NguoiDungController : ControllerBase
    {
        private INguoiDungBussiness _userBusiness;
        private string _path;
        public NguoiDungController(INguoiDungBussiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userBusiness.Authenticate(model.TaiKhoan, model.MatKhau);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(user);
        }
    [NonAction]
    public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
    [NonAction]
    public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [Route("user-all-paginate")]
        [HttpPost]
        public ResponseModel GetDataAllPaginate([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                long total = 0;
                var data = _userBusiness.GetDataAllPaginate(page, pageSize, out total);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
        [Route("delete-user/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(string id)
        {
            _userBusiness.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [Route("create-user")]
        [HttpPost]
        public NguoiDungModel  CreateUser([FromBody] NguoiDungModel  model)
        {
            
            model.MaNguoiDung = Guid.NewGuid().ToString();
             model.MatKhau = GetMD5(model.MatKhau);
            _userBusiness.Create(model);
            return model;
        }

        [Route("update-user")]
        [HttpPost]
        public NguoiDungModel  UpdateUser([FromBody] NguoiDungModel  model)
        {
           
            _userBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public NguoiDungModel  GetDatabyID(string id)
        {
            return _userBusiness.GetDatabyID(id);
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string hoTen = "";
                if (formData.Keys.Contains("hoTen") && !string.IsNullOrEmpty(Convert.ToString(formData["hoTen"]))) { hoTen = Convert.ToString(formData["hoTen"]); }
                string taiKhoan = "";
                if (formData.Keys.Contains("taiKhoan") && !string.IsNullOrEmpty(Convert.ToString(formData["taiKhoan"]))) { taiKhoan = Convert.ToString(formData["taiKhoan"]); }
                long total = 0;
                var data = _userBusiness.Search(page, pageSize, out total, hoTen, taiKhoan);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

    [NonAction]
        public static string GetMD5(string str)
        {
          MD5 md5 = new MD5CryptoServiceProvider();
          byte[] fromData = Encoding.UTF8.GetBytes(str);
          byte[] targetData = md5.ComputeHash(fromData);
          string byte2String = null;

          for (int i = 0; i < targetData.Length; i++)
          {
            byte2String += targetData[i].ToString("x2");

          }
          return byte2String;
        }
      }
}
