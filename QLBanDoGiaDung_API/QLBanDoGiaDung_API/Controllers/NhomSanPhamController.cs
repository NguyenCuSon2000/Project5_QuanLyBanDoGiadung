using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QLBanDoGiaDung_API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class NhomSanPhamController : ControllerBase
    {
        private INhomSanPhamBussiness _nhomspBusiness;
        private string _path;
        public NhomSanPhamController(INhomSanPhamBussiness newBusiness, IConfiguration configuration)
        {
            _nhomspBusiness = newBusiness;
            _path = configuration["AppSettings:PATH"];
        }

       
        // GET: api/<NhomSanPhamController>
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<NhomSanPhamModel> GetDataAll()
        {
            return _nhomspBusiness.GetDataAll();
        }
        [Route("group-all-paginate")]
        [HttpPost]
        public ResponseModel GetDataAllPaginate([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                long total = 0;
                var data = _nhomspBusiness.GetDataAllPaginate(page, pageSize, out total);
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
        [Route("create-category")]
        [HttpPost]
        public NhomSanPhamModel CreateNhom([FromBody] NhomSanPhamModel model)
        {
            model.MaNhom = Guid.NewGuid().ToString();
            _nhomspBusiness.Create(model);
            return model;
        }

        [Route("update-category")]
        [HttpPost]
        public NhomSanPhamModel UpdateNhom([FromBody] NhomSanPhamModel model)
        {
            _nhomspBusiness.Update(model);
            return model;
        }

       
        [Route("get-by-id/{maloaisp}")]
        [HttpGet]
        public NhomSanPhamModel GetDatabyID(string maloaisp)
        {
            return _nhomspBusiness.GetDatabyID(maloaisp);
        }

        [Route("delete-category/{id}")]
        [HttpDelete]
        public IActionResult DeleteCategory(string id)
        {
           _nhomspBusiness.Delete(id);
            return Ok();
        }

        [AllowAnonymous]
        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string manhom = "";
                if (formData.Keys.Contains("manhom") && !string.IsNullOrEmpty(Convert.ToString(formData["manhom"]))) { manhom = Convert.ToString(formData["manhom"]); }
                string tennhom = "";
                if (formData.Keys.Contains("tennhom") && !string.IsNullOrEmpty(Convert.ToString(formData["tennhom"]))) { tennhom = Convert.ToString(formData["tennhom"]); }
                long total = 0;
                var data = _nhomspBusiness.Search(page, pageSize, out total, manhom, tennhom);
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
        
    }
}
