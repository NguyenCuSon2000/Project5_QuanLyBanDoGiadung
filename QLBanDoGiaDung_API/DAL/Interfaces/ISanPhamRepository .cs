using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ISanPhamRepository
    {
        List<SanPhamModel> GetDataAll();
        List<SanPhamModel> GetDataAllPaginate(int pageIndex, int pageSize, out long total);
        List<SanPhamModel> GetProductNew();
        SanPhamModel GetDatabyID(string masp);
        bool Create(SanPhamModel model);
        bool Update(SanPhamModel model);
        bool Delete(string masp);
        List<SanPhamModel> Search(int pageIndex, int pageSize, out long total, string tenSanPham);
        List<SanPhamModel> GetProductByCategory(int pageIndex, int pageSize, out long total,string maloai);
        List<SanPhamModel> GetProductByBrand(int pageIndex, int pageSize, out long total,string mahang, string tenhang);
    }
}
