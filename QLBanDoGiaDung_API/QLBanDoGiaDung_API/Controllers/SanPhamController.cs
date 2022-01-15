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
  public class SanPhamController : ControllerBase
  {
    private ISanPhamBussiness _spBusiness;
    private string _path;
    private string _path1;
    public SanPhamController(ISanPhamBussiness newBusiness, IConfiguration configuration)
    {
      _spBusiness = newBusiness;
      _path = configuration["AppSettings:PATH"];
      _path1 = configuration["AppSettings:PATH1"];
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

    [Route("upload")]
    [HttpPost, DisableRequestSizeLimit]
    public async Task<IActionResult> Upload(IFormFile file)
    {
      try
      {
        if (file.Length > 0)
        {
          string filePath = $"{file.FileName}";
          var fullPath = CreatePathFile(filePath, _path);
          using (var fileStream = new FileStream(fullPath, FileMode.Create))
          {
            await file.CopyToAsync(fileStream);
          }
          var fullPath1 = CreatePathFile(filePath, _path1);
          using (var fileStream = new FileStream(fullPath1, FileMode.Create))
          {
            await file.CopyToAsync(fileStream);
          }
          return Ok(new { filePath });
        }
        else
        {
          return BadRequest();
        }
      }
      catch (Exception)
      {
        return StatusCode(500, "Không tìm thây");
      }
    }

    [NonAction]
    private string CreatePathFile(string RelativePathFileName, string path)
    {
      try
      {
        string serverRootPathFolder = path;
        string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
        string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
        if (!Directory.Exists(fullPathFolder))
          Directory.CreateDirectory(fullPathFolder);
        return fullPathFile;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }

    // GET: api/<SanPhamController>
    [Route("get-all")]
    [HttpGet]
    public IEnumerable<SanPhamModel> GetDataAll()
    {
      return _spBusiness.GetDataAll();
    }
    [Route("product-all-paginate")]
    [HttpPost]
    public ResponseModel GetDataAllPaginate([FromBody] Dictionary<string, object> formData)
    {
      var response = new ResponseModel();
      try
      {
        var page = int.Parse(formData["page"].ToString());
        var pageSize = int.Parse(formData["pageSize"].ToString());
        long total = 0;
        var data = _spBusiness.GetDataAllPaginate(page, pageSize, out total);
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
    [Route("get-product-by-category")]
    [HttpPost]
    public ResponseModel GetProductByCategory([FromBody] Dictionary<string, object> formData)
    {
      var response = new ResponseModel();
      try
      {
        var page = int.Parse(formData["page"].ToString());
        var pageSize = int.Parse(formData["pageSize"].ToString());
        string maloai = "";
        if (formData.Keys.Contains("maloai") && !string.IsNullOrEmpty(Convert.ToString(formData["maloai"])))
        {
          maloai = Convert.ToString(formData["maloai"]);
        }
        long total = 0;
        var data = _spBusiness.GetProductByCategory(page, pageSize, out total, maloai);
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

    [Route("get-product-by-brand")]
    [HttpPost]
    public ResponseModel GetProductByBrand([FromBody] Dictionary<string, object> formData)
    {
      var response = new ResponseModel();
      try
      {
        var page = int.Parse(formData["page"].ToString());
        var pageSize = int.Parse(formData["pageSize"].ToString());
        string mahang = "";
        if (formData.Keys.Contains("mahang") && !string.IsNullOrEmpty(Convert.ToString(formData["mahang"]))) { mahang = Convert.ToString(formData["mahang"]); }
        string tenhang = "";
        if (formData.Keys.Contains("tenhang") && !string.IsNullOrEmpty(Convert.ToString(formData["tenhang"]))) { tenhang = Convert.ToString(formData["tenhang"]); }
        long total = 0;
        var data = _spBusiness.GetProductByBrand(page, pageSize, out total, mahang, tenhang);
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

    [Route("get-product-new")]
    [HttpGet]
    public IEnumerable<SanPhamModel> GetProductNew()
    {
      return _spBusiness.GetProductNew();
    }


    [Route("get-by-id/{masp}")]
    [HttpGet]
    public SanPhamModel GetDatabyID(string masp)
    {
      return _spBusiness.GetDatabyID(masp);
    }

    [Route("create-product")]
    [HttpPost]
    public SanPhamModel CreateProduct([FromBody] SanPhamModel model)
    {
      model.MaSanPham = Guid.NewGuid().ToString();
      _spBusiness.Create(model);
      return model;
    }

    [Route("update-product")]
    [HttpPost]
    public SanPhamModel UpdateProduct([FromBody] SanPhamModel model)
    {
      _spBusiness.Update(model);
      return model;
    }

    [Route("delete-product")]
    [HttpPost]
    public IActionResult DeleteProduct([FromBody] Dictionary<string, object> formData)
    {
      string MaSanPham = "";
      if (formData.Keys.Contains("MaSanPham") && !string.IsNullOrEmpty(Convert.ToString(formData["MaSanPham"]))) { MaSanPham = Convert.ToString(formData["MaSanPham"]); }
      _spBusiness.Delete(MaSanPham);
      return Ok();
    }

    [Route("search")]
    [HttpPost]
    public ResponseModel search([FromBody] Dictionary<string, object> formData)
    {
      var response = new ResponseModel();
      try
      {
        var page = int.Parse(formData["page"].ToString());
        var pageSize = int.Parse(formData["pageSize"].ToString());
        string tenSanPham = "";
        if (formData.Keys.Contains("tenSanPham") && !string.IsNullOrEmpty(Convert.ToString(formData["tenSanPham"]))) { tenSanPham = Convert.ToString(formData["tenSanPham"]); }
        long total = 0;
        var data = _spBusiness.Search(page, pageSize, out total, tenSanPham);
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
