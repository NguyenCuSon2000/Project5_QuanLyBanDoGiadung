using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanDoGiaDung_API.Interfaces
{
  public interface IMailService
  {
    Task SendEmailAsync(MailRequest mailRequest);
  }
}
