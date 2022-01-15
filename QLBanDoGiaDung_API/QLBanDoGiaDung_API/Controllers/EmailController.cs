using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using QLBanDoGiaDung_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanDoGiaDung_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class EmailController : ControllerBase
  {
    private readonly IMailService _mailService;
    public EmailController(IMailService mailService)
    {
      _mailService = mailService;
    }

    [HttpPost("Send")]
    public async Task<IActionResult> Send([FromBody] MailRequest request)
    {
      try
      {
        await _mailService.SendEmailAsync(request);
        return Ok();
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }
  }
}
