using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QLBanDoGiaDung_API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class DonHangController : ControllerBase
    {
        private IDonHangBussiness _HoaDonBussiness;
        public DonHangController(IDonHangBussiness HoaDonBussiness)
        {
            _HoaDonBussiness = HoaDonBussiness;
        }

        // GET: api/<HoaDonController>
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<DonHangModel> GetDataAll()
        {
            return _HoaDonBussiness.GetDataAll();
        }
        [Route("order-all-paginate")]
        [HttpPost]
        public ResponseModel GetDataAllPaginate([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                long total = 0;
                var data = _HoaDonBussiness.GetDataAllPaginate(page, pageSize, out total);
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
        [Route("create-hoadon")]
        [HttpPost]
        public DonHangModel CreateItem([FromBody] DonHangModel model)
        {
            model.MaDonHang = Guid.NewGuid().ToString();
            if (model.listjson_chitiet != null)
            {
                foreach (var item in model.listjson_chitiet)
                {
                    item.MaDonHang = model.MaDonHang;
                    item.ma_chi_tiet = Guid.NewGuid().ToString();
                    
                }

            }
            _HoaDonBussiness.Create(model);
            return model;
        }

        [Route("update-hoadon")]
        [HttpPost]
        public DonHangModel UpdateHoaDon([FromBody] DonHangModel model)
        {
            if (model.listjson_chitiet != null)
            {
                foreach (var item in model.listjson_chitiet)
                    if (item.status == 1)
                    {
                        item.ma_chi_tiet = Guid.NewGuid().ToString();
                    }
            }
            _HoaDonBussiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public DonHangModel GetDatabyID(string id)
        {
            return _HoaDonBussiness.GetDatabyID(id);
        }

        [Route("delete-hoadon/{id}")]
        [HttpDelete]
        public IActionResult DeleteHoaDon(string id)
        {
            _HoaDonBussiness.Delete(id);
            return Ok();
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
                string hoten = "";
                if (formData.Keys.Contains("hoten") && !string.IsNullOrEmpty(Convert.ToString(formData["hoten"]))) { hoten = Convert.ToString(formData["hoten"]); }
                string diachi = "";
                if (formData.Keys.Contains("diachi") && !string.IsNullOrEmpty(Convert.ToString(formData["diachi"]))) { diachi = Convert.ToString(formData["diachi"]); }
                string email = "";
                if (formData.Keys.Contains("email") && !string.IsNullOrEmpty(Convert.ToString(formData["email"]))) { email = Convert.ToString(formData["email"]); }
                long total = 0;
                var data = _HoaDonBussiness.Search(page, pageSize, out total, hoten, diachi, email);
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
