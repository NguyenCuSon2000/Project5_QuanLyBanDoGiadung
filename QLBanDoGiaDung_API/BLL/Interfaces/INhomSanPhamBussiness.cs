using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface INhomSanPhamBussiness
    {
        List<NhomSanPhamModel> GetDataAll();
        List<NhomSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        NhomSanPhamModel GetDatabyID(string maloaisp);
        bool Create(NhomSanPhamModel model);
        bool Update(NhomSanPhamModel model);
        bool Delete(string maloaisp);
        List<NhomSanPhamModel> Search(int pageIndex, int pageSize, out long total,string tenNhom);
    }
}
