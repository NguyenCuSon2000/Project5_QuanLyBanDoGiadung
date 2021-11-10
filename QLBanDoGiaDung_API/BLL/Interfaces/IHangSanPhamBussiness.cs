using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IHangSanPhamBussiness
    {
        List<HangSanPhamModel> GetDataAll();
        List<HangSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        HangSanPhamModel GetDatabyID(string mahang);
        bool Create(HangSanPhamModel model);
        bool Update(HangSanPhamModel model);
        bool Delete(string maloaisp);
        List<HangSanPhamModel> Search(int pageIndex, int pageSize, out long total, string tenhang);
    }
}
