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
    [Authorize]
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
        [HttpGet]
        public IEnumerable<ThongKeModel> GetSanPhamBanChay()
        {
            return _spBusiness.GetSanPhamBanChay();
        }

        [Route("get-soluong-sanpham")]
        [HttpGet]
        public string GetSoLuongSanPham()
        {
            return _spBusiness.GetSoLuongSanPham();
        }

        [Route("get-soluong-loaisanpham")]
        [HttpGet]
        public string GetSoLuongLoaiSP()
        {
            return _spBusiness.GetSoLuongLoaiSP();
        }

        [Route("get-soluong-hoadon")]
        [HttpGet]
        public string GetSoLuongHoaDon()
        {
            return _spBusiness.GetSoLuongHoaDon();
        }

        [Route("get-soluong-nguoidung")]
        [HttpGet]
        public string GetSoLuongNguoiDung()
        {
            return _spBusiness.GetSoLuongNguoiDung();
        }

    }
}
