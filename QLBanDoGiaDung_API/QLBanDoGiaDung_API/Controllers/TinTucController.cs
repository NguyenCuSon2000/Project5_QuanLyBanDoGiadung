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
  public class TinTucController : ControllerBase
  {
    private ITinTucBussiness _spBusiness;
    private string _path;
    private string _path1;
    public TinTucController(ITinTucBussiness newBusiness, IConfiguration configuration)
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
    [Route("get-by-id/{matintuc}")]
    [HttpGet]
    public TinTucModel GetDatabyID(string matintuc)
    {
      return _spBusiness.GetDatabyID(matintuc);
    }

    [Route("create-blog")]
    [HttpPost]
    public TinTucModel Createblog([FromBody] TinTucModel model)
    {
      model.MaTinTuc = Guid.NewGuid().ToString();
      _spBusiness.Create(model);
      return model;
    }

    [Route("update-blog")]
    [HttpPost]
    public TinTucModel Updateblog([FromBody] TinTucModel model)
    {
      _spBusiness.Update(model);
      return model;
    }

    [Route("delete-blog")]
    [HttpPost]
    public IActionResult Deleteblog([FromBody] Dictionary<string, object> formData)
    {
      string MaTinTuc = "";
      if (formData.Keys.Contains("MaTinTuc") && !string.IsNullOrEmpty(Convert.ToString(formData["MaTinTuc"]))) { MaTinTuc = Convert.ToString(formData["MaTinTuc"]); }
      _spBusiness.Delete(MaTinTuc);
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
        string tieuDe = "";
        if (formData.Keys.Contains("tieuDe") && !string.IsNullOrEmpty(Convert.ToString(formData["tieuDe"]))) { tieuDe = Convert.ToString(formData["tieuDe"]); }
        long total = 0;
        var data = _spBusiness.Search(page, pageSize, out total, tieuDe);
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
