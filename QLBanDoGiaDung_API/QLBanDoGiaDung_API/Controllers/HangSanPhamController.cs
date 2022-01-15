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
   
   
    [ApiController]
  [Route("api/[controller]")]
  public class HangSanPhamController : ControllerBase
    {
        private IHangSanPhamBussiness _hangspBusiness;
        private string _path;
        public HangSanPhamController(IHangSanPhamBussiness newBusiness, IConfiguration configuration)
        {
            _hangspBusiness = newBusiness;
            _path = configuration["AppSettings:PATH"];
        }
     
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<HangSanPhamModel> GetDataAll()
        {
            return _hangspBusiness.GetDataAll();
        }

        [Route("brand-all-paginate")]
        [HttpPost]
        public ResponseModel GetDataAllPaginate([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                long total = 0;
                var data = _hangspBusiness.GetDataAllPaginate(page, pageSize, out total);
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
        [Route("create-brand")]
        [HttpPost]
        public HangSanPhamModel CreateHangSP([FromBody] HangSanPhamModel model)
        {
            model.MaHang = Guid.NewGuid().ToString();
            _hangspBusiness.Create(model);
            return model;
        }

        [Route("update-brand")]
        [HttpPost]
        public HangSanPhamModel UpdateHang([FromBody] HangSanPhamModel model)
        {
            _hangspBusiness.Update(model);
            return model;
        }

       
        [Route("get-by-id/{mahang}")]
        [HttpGet]
        public HangSanPhamModel GetDatabyID(string mahang)
        {
            return _hangspBusiness.GetDatabyID(mahang);
        }

        [Route("delete-brand/{id}")]
        [HttpDelete]
        public IActionResult DeleteCategory(string id)
        {
           _hangspBusiness.Delete(id);
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
                string tenhang = "";
                if (formData.Keys.Contains("tenhang") && !string.IsNullOrEmpty(Convert.ToString(formData["tenhang"]))) { tenhang = Convert.ToString(formData["tenhang"]); }
                long total = 0;
                var data = _hangspBusiness.Search(page, pageSize, out total, tenhang);
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
