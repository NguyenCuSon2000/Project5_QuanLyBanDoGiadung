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
  public class LoaiSanPhamController : ControllerBase
  {
    private ILoaiSanPhamBussiness _loaispBusiness;
    private string _path;
    private string _path1;
    public LoaiSanPhamController(ILoaiSanPhamBussiness newBusiness, IConfiguration configuration)
    {
      _loaispBusiness = newBusiness;
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

    //[Route("delete-category/{id}")]
    //[HttpDelete]
    //public IActionResult DeleteCategory(string id)
    //{
    //  _loaispBusiness.Delete(id);
    //  return Ok();
    //}
    [Route("delete-category")]
    [HttpPost]
    public IActionResult DeleteProduct([FromBody] Dictionary<string, object> formData)
    {
      string MaLoai = "";
      if (formData.Keys.Contains("MaLoai") && !string.IsNullOrEmpty(Convert.ToString(formData["MaLoai"]))) { MaLoai = Convert.ToString(formData["MaLoai"]); }
      _loaispBusiness.Delete(MaLoai);
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
        string tenLoai = "";
        if (formData.Keys.Contains("tenLoai") && !string.IsNullOrEmpty(Convert.ToString(formData["tenLoai"]))) { tenLoai = Convert.ToString(formData["tenLoai"]); }
        long total = 0;
        var data = _loaispBusiness.Search(page, pageSize, out total, tenLoai);
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
