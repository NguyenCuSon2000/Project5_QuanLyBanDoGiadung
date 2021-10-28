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

namespace QuanLyBanDoGiaDung_API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSanPhamController : ControllerBase
    {
        private ILoaiSanPhamBussiness _loaispBusiness;
        private string _path;
        public LoaiSanPhamController(ILoaiSanPhamBussiness newBusiness, IConfiguration configuration)
        {
            _loaispBusiness = newBusiness;
            _path = configuration["AppSettings:PATH"];
        }

       
        // GET: api/<LoaiSanPhamController>
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<LoaiSanPhamModel> GetDataAll()
        {
            return _loaispBusiness.GetDataAll();
        }
        [Route("category-all-paginate")]
        [HttpPost]
        public ResponseModel GetDataAllPaginate([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                long total = 0;
                var data = _loaispBusiness.GetDataAllPaginate(page, pageSize, out total);
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
        public LoaiSanPhamModel CreateLoaiSP([FromBody] LoaiSanPhamModel model)
        {
            model.MaLoai = Guid.NewGuid().ToString();
            _loaispBusiness.Create(model);
            return model;
        }

        [Route("update-category")]
        [HttpPost]
        public LoaiSanPhamModel UpdateLoai([FromBody] LoaiSanPhamModel model)
        {
            _loaispBusiness.Update(model);
            return model;
        }

       
        [Route("get-by-id/{maloaisp}")]
        [HttpGet]
        public LoaiSanPhamModel GetDatabyID(string maloaisp)
        {
            return _loaispBusiness.GetDatabyID(maloaisp);
        }

        [Route("delete-category/{id}")]
        [HttpDelete]
        public IActionResult DeleteCategory(string id)
        {
           _loaispBusiness.Delete(id);
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
                string maloaisp = "";
                if (formData.Keys.Contains("maloaisp") && !string.IsNullOrEmpty(Convert.ToString(formData["maloaisp"]))) { maloaisp = Convert.ToString(formData["maloaisp"]); }
                string tenloai = "";
                if (formData.Keys.Contains("tenloai") && !string.IsNullOrEmpty(Convert.ToString(formData["tenloai"]))) { tenloai = Convert.ToString(formData["tenloai"]); }
                long total = 0;
                var data = _loaispBusiness.Search(page, pageSize, out total, maloaisp, tenloai);
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
        [AllowAnonymous]
        [Route("get-category-by-group")]
        [HttpPost]
        public ResponseModel GetCategoryByGroup([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string manhom = "";
                if (formData.Keys.Contains("manhom") && !string.IsNullOrEmpty(Convert.ToString(formData["manhom"])))
                {
                    manhom = Convert.ToString(formData["manhom"]);
                }
              
                long total = 0;
                var data = _loaispBusiness.GetCategoryByGroup(page, pageSize, out total, manhom);
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
