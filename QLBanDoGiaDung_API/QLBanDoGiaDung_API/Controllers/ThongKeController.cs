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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private IThongKeBussiness _spBusiness;
        private string _path;
        public ThongKeController(IThongKeBussiness newBusiness, IConfiguration configuration)
        {
            _spBusiness = newBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        // GET: api/<ThongKeController>
       
        [Route("get-sanpham-banchay")]
        [HttpPost]
        public ResponseModel GetSanPhamBanChay([FromBody] Dictionary<string, object> formData)
        {
          var response = new ResponseModel();
          try
          {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());
            string tenSanPham = "";
            if (formData.Keys.Contains("tenSanPham") && !string.IsNullOrEmpty(Convert.ToString(formData["tenSanPham"]))) { tenSanPham = Convert.ToString(formData["tenSanPham"]); }
            long total = 0;
            var data = _spBusiness.GetSanPhamBanChay(page, pageSize, out total, tenSanPham);
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
    //[Route("get-soluong-sanpham")]
    //[HttpGet]
    //public string GetSoLuongSanPham()
    //{
    //    return _spBusiness.GetSoLuongSanPham();
    //}

    //[Route("get-soluong-loaisanpham")]
    //[HttpGet]
    //public string GetSoLuongLoaiSP()
    //{
    //    return _spBusiness.GetSoLuongLoaiSP();
    //}

    //[Route("get-soluong-hoadon")]
    //[HttpGet]
    //public string GetSoLuongHoaDon()
    //{
    //    return _spBusiness.GetSoLuongHoaDon();
    //}

    //[Route("get-soluong-nguoidung")]
    //[HttpGet]
    //public string GetSoLuongNguoiDung()
    //{
    //    return _spBusiness.GetSoLuongNguoiDung();
    //}

  }
}
