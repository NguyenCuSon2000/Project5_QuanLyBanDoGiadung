using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface INhomSanPhamRepository
    {
        List<NhomSanPhamModel> GetDataAll();
        List<NhomSanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        NhomSanPhamModel GetDatabyID(string manhom);
        bool Create(NhomSanPhamModel model);
        bool Update(NhomSanPhamModel model);
        bool Delete(string nhom);
        List<NhomSanPhamModel> Search(int pageIndex, int pageSize, out long total, string tenNhom);
    }
}
