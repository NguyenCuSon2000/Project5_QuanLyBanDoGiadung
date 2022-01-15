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

namespace QLBanDoGiaDung_API.Controllers
{
    [ApiController]
  [Route("api/[controller]")]
  public class DiaChiController : ControllerBase
    {
        private IDiaChiBussiness _dcBusiness;
        
        private string _path;
        public DiaChiController(IDiaChiBussiness newBusiness, IConfiguration configuration)
        {
            _dcBusiness = newBusiness;
            
            _path = configuration["AppSettings:PATH"];
        }

        // GET: api/<DiaChiController>
        [Route("get-province-all")]
        [HttpGet]
        public IEnumerable<TinhThanhPho> GetTinhThanhPho()
        {
            return _dcBusiness.GetTinhThanhPho();
        }

        [Route("get-district-all")]
        [HttpGet]
        public IEnumerable<QuanHuyen> GetQuanHuyen()
        {
            return _dcBusiness.GetQuanHuyen();
        }

        [Route("get-qh-by-matinh/{maTP}")]
        [HttpGet]
        public IEnumerable<QuanHuyen> GetQHbyMaTinh(string maTP)
        {
            return _dcBusiness.GetQHbyMaTinh(maTP);
        }
       

        [Route("get-ward-all")]
        [HttpGet]
        public IEnumerable<XaPhuong> GetXaPhuong()
        {
            return _dcBusiness.GetXaPhuong();
        }

        [Route("get-xp-by-maqh/{maQH}")]
        [HttpGet]
        public IEnumerable<XaPhuong> GetXPbyMaQH(string maQH)
        {
            return _dcBusiness.GetXPbyMaQH(maQH);
        }
    }
}
