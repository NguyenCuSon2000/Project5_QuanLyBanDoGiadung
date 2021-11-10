using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IDiaChiBussiness
    {
        List<TinhThanhPho> GetTinhThanhPho();
        List<QuanHuyen> GetQuanHuyen();
        List<QuanHuyen> GetQHbyMaTinh(string maTP);
        List<XaPhuong> GetXaPhuong();
        List<XaPhuong> GetXPbyMaQH(string maQH);
    }
}
